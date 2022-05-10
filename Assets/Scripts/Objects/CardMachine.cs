using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMachine : Lock
{
    public Material green;
    public Material red;
    public Rigidbody drawer;
    public SocketWithCardCheck cardSocket;
    public AudioClip drawerOpen;
    public AudioClip success;

    private AudioSource audioSource;

    private MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public void Initialize() {
        audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = red;
        ToggleLockedObjects(false);
        ToggleObjectsInside(false);
    }
    

    public void SwitchMaterials() {
        meshRenderer.material = green;
    }

    public void CardInserted() {
        StartCoroutine(OpeningRegister());
    }

    IEnumerator OpeningRegister() {
        yield return new WaitForSeconds(1f);

        if(audioSource != null) audioSource.PlayOneShot(success);

        SwitchMaterials();

        yield return new WaitForSeconds(1f);

        if(audioSource != null) audioSource.PlayOneShot(drawerOpen);

        drawer.isKinematic = false;
        drawer.AddForce(-40f, 0, 0, ForceMode.Impulse);

        yield return new WaitForSeconds(0.1f);

        ToggleLockedObjects(true);
        ToggleObjectsInside(true);

        drawer.isKinematic = true;

    }

    public bool IsCardInserted() {
        return cardSocket.IsCardInserted();
    }

}
