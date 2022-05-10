using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public SceneManager sceneManager;

    ////////////////////////////////////////

    public Bottle[] bottles;
    public SocketWithBottleCheck[] bottleSockets;

    private bool areAllBottlesFound;
    private bool isAtleastOneBottlePlacedInSocket;
    private bool areAllBottlesCorrectlyPlaced;

    ///////////////////////////////////////

    public GameObject cardSocket;
    public CardMachine cardMachine;
    public GameObject cardPrefab;
    public Transform cardSpawnPosition;

    private Card card;
    private bool isCardSpawned;
    private bool isCardFound;
    private bool isCardInserted;
    private bool isRegisterOpened;

    ///////////////////////////////////////
    
    public Safe safe;

    private bool isSafeUnlocked;

    ///////////////////////////////////////

    public Door door;

    private bool isDoorUnlocked;

    //////////////////////////////////////

    public AudioClip successClip;
    public PlayerDisabler player;
    public bool isGameFinished;

    private AudioSource audioSource;
    private GameTimer gameTimer;
    private HintSystem hintSystem;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameTimer = GetComponent<GameTimer>();
        hintSystem = GetComponent<HintSystem>();
        
        areAllBottlesFound = false;
        isAtleastOneBottlePlacedInSocket = false;
        areAllBottlesCorrectlyPlaced = false;
        isCardSpawned = false;
        isCardFound = false;
        isCardInserted = false;
        isRegisterOpened = false;
        isDoorUnlocked = false;
        isGameFinished = false;

        ConfigureHints();
    }

   

    void Update()
    {
        if(isGameFinished) return;

        if(gameTimer.IsTimeUp()) {
            StartCoroutine(LoseGame());
            return;
        }

        if(door.IsOpened()) {
            StartCoroutine(WinGame());
            return;
        }

        if(!areAllBottlesFound) {
            CheckIfAllBottlesFound();
            return;
        }

        if(!isAtleastOneBottlePlacedInSocket) {
            CheckIfAtleastOneBottlePlacedInSocket();
            return;
        }

        if(!areAllBottlesCorrectlyPlaced) {
            CheckBottlePlacement();   
            return;
        }

        if(!isCardFound) {
            CheckIfCardFound();
            return;
        }

        if(!isCardInserted) {
            CheckIfCardInserted();
            return;
        }

        if(!isSafeUnlocked) {
            CheckIfSafeOpened();
            return;
        }

        if(!isDoorUnlocked) {
            CheckIfDoorUnlocked();
            return;
        }
    }

    //TODO
    private IEnumerator WinGame() {
        isGameFinished = true;
        StartCoroutine(player.Disable());
        yield return new WaitForSeconds(2f);
        sceneManager.RedirectToScene(2);
    }

    //TODO
    private IEnumerator LoseGame() {
        isGameFinished = true;
        StartCoroutine(player.Disable());
        yield return new WaitForSeconds(2f);
        sceneManager.RedirectToScene(3);
    }

    private void CheckIfDoorUnlocked() {
        isDoorUnlocked = door.IsKeyInserted();

        if(isDoorUnlocked) {
            door.Unlock();
        }
    }

    private void CheckIfSafeOpened() {
        isSafeUnlocked = safe.IsUnlocked();

        if(isSafeUnlocked) {
            safe.OpenDoor();
            hintSystem.SetHintIndex(6);
        }
    }

    private void CheckIfCardInserted() {
        isCardInserted = cardMachine.IsCardInserted();

        if(!isCardInserted) return;

        cardMachine.CardInserted();
        isRegisterOpened = true;
        hintSystem.SetHintIndex(5);
    }

    private void CheckIfCardFound() {
        isCardFound = card.WasPickedUp();

        if(isCardFound) hintSystem.SetHintIndex(4);
    }

    private void CheckBottlePlacement() {
        bool temp = true;

        foreach(SocketWithBottleCheck bottleSocket in bottleSockets) {
            temp = temp && bottleSocket.HasCorrectBottle();
        }

        areAllBottlesCorrectlyPlaced = temp;

        if(areAllBottlesCorrectlyPlaced) {

            hintSystem.SetHintIndex(3);

            audioSource.PlayOneShot(successClip);
            
            SpawnCard();
            
            foreach(Bottle bottle in bottles) {
                bottle.SwitchLabels();
            }
        }
    }

    private void CheckIfAtleastOneBottlePlacedInSocket() {
        bool temp = false;

        foreach(SocketWithBottleCheck bottleSocket in bottleSockets) {
            temp = temp || bottleSocket.HasBottlePlaced();
        }

        isAtleastOneBottlePlacedInSocket = temp;

        if(isAtleastOneBottlePlacedInSocket) hintSystem.SetHintIndex(2);
    }

    private void CheckIfAllBottlesFound() {
        bool temp = true;

        foreach(Bottle bottle in bottles) {
            temp = temp && bottle.WasPickedUp();
        }

        areAllBottlesFound = temp;

        if(areAllBottlesFound) hintSystem.SetHintIndex(1);

    }

    private void SpawnCard() {
        isCardSpawned = true;
        card = ((GameObject) Instantiate(cardPrefab, cardSpawnPosition)).GetComponent<Card>();
    }

    private void ConfigureHints() {
        List<string[]> hints = new List<string[]>();

        //Find the bottles
        hints.Add(new string[] {
            "I should put the order together", 
            "Perhaps I should check the menu for the order",
            "Look for the drinks underlined in the menu",
            "Wine and one beer bottle is on a display, liquor bottle is on the table, last beer bottle is on the bar shelf"
        });

        //Bring the bottles to the coasters
        hints.Add(new string[] {
            "I should bring the drinks to these guys", 
            "There are probably some empty coasters on the table for the drinks",
            "I must take the bottles underlined in the menu and put them on the 4 coasters on the bar"
        });

        //Put the bottles in the correct order
        hints.Add(new string[] {
            "I wonder who ordered which drink", 
            "If only there was something I could check to know in what order I should put the bottles on the coasters that are on the table",
            "Perhaps I should check the menu",
            "I must put the bottles on the 4 coasters on the table in an order as is shown in the menu (from left to right)"
        });

        //Find the card
        hints.Add(new string[] {
            "That's weird, something changed about the bottles", 
            "What is this scribble on the bottle's label?", 
            "It spells CARD, perhaps I should try looking for one",
            "I should try looking for a credit card at places I've already looked",
            "I must find a credit card on the table at the furthest corner of the bar"
        });

        //Put the card in the card reader
        hints.Add(new string[] {
            "I wonder what cards are used for?", 
            "The card is most likely used for payments", 
            "I believe these guys haven't payed for the drinks yet",
            "Maybe I should help myself and pay for the drinks with this card?",
            "I must take the card to the card reader near the cash register and put it in"
        });

        //Figure out calculations for the safe
        hints.Add(new string[] {
            "I should check out the symbols on the card that was in the cash register", 
            "I probably have to switch the glass symbols with numbers to get a safe combination", 
            "I think I've seen these symbols on a wall as well as in a menu",
            "I should try using prices of the underlined bottles in the menu as the values for the symbols",
            "I should probably solve the equation on a wall to get the fifth unknown number",
            "Hmm, I think there was a discount for something, I should look for the discount stand",
            "The wine is 50% off, I think I should half the wine's price and try to use that as the value for the symbol puzzle",
            "Calculate the fifth symbol on the wall using prices of the underlined bottles in the menu (half the wine's price). Then enter the symbols to the safe in the order shown on the card from the cash register"
        });
        
        //Unlock the door
        hints.Add(new string[] {
            "I wonder what the key unlocks", 
            "I think there is only one place with a key lock", 
            "I believe I've seen a key lock on the door",
            "I should try to unlock the door with the key",
            "I must unlock the door at the corner of the bar with this key"
        });

        hintSystem.ConfigureHints(hints);
    }

    public bool AreAllBottlesFound() {
        return areAllBottlesFound;
    }

    public bool IsAtleastOneBottlePlacedInSocket() {
        return isAtleastOneBottlePlacedInSocket;
    }

    public bool AreAllBottlesCorrectlyPlaced() {
        return areAllBottlesCorrectlyPlaced;
    }

    public bool IsCardSpawned() {
        return isCardSpawned;
    }

    public bool IsCardInserted() {
        return isCardInserted;
    }

    private bool IsRegisterOpened() {
        return isRegisterOpened;
    }

    public bool IsSafeUnlocked() {
        return isSafeUnlocked;
    }


    public bool IsDoorUnlocked() {
        return isDoorUnlocked;
    }

}
