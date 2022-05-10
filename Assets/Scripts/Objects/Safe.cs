using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Safe : Lock
{
    public string lockCombination;
    public float resetThreshold;
    public AudioClip click;
    public AudioClip fail;
    public AudioClip success;
    
    private Rigidbody safeDoor;
    [SerializeField] private float resetTimer;
    [SerializeField] private string currentCombination;
    [SerializeField] private bool isUnlocked;
    private AudioSource audioSource;

    void Start() {
        Initialize();
    }

    public void Initialize() {
        ToggleObjectsInside(false);
        audioSource = GetComponent<AudioSource>();
        safeDoor = GetComponent<Rigidbody>();
        ToggleLockedObjects(false);
        resetTimer = 0f;
    }

    public void SetLockCombination(string value) {
        lockCombination = value;
    }

    public void SetResetThreshold(float value) {
        resetThreshold = value;
    }

    public string GetCurrentCombination() {
        return currentCombination;
    }

    void Update() {
        if(isUnlocked) return;

        if(currentCombination.Length == 0) return;

        resetTimer += Time.deltaTime;

        if(resetTimer >= resetThreshold) {
            IncorrectCombination();
        }
    }

    public bool IsUnlocked() {
        return isUnlocked;
    }

    public void ButtonPressed(string value) {
        if(isUnlocked) return;

        if(audioSource != null) audioSource.PlayOneShot(click);

        currentCombination += value;
        resetTimer = 0f;


        if(currentCombination.Length >= lockCombination.Length) {
            CheckTheCombination();
        }
       
    }

    public void OpenDoor() {
        StartCoroutine(OpeningDoor());
    }

    IEnumerator OpeningDoor() {
        safeDoor.isKinematic = false;
        audioSource.PlayOneShot(success);
        yield return new WaitForSeconds(1f);
        safeDoor.AddForce(-3f, 0, 0, ForceMode.Impulse);
        yield return new WaitForSeconds(0.1f);
        safeDoor.isKinematic = true;
        ToggleLockedObjects(true);  
        ToggleObjectsInside(true);
    }    

    private void CheckTheCombination() {
        if(currentCombination == lockCombination){
            isUnlocked = true;
            return;
        }

        IncorrectCombination();
    }

    private void IncorrectCombination() {
        currentCombination = "";
        resetTimer = 0f;
        if(audioSource != null) audioSource.PlayOneShot(fail);
    }
}
