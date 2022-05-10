using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueSoundController : MonoBehaviour
{
    public AudioClip clueSound;

    private bool havePlayed;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        havePlayed = false;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClueSound() {
        if(havePlayed) return;
        
        audioSource.PlayOneShot(clueSound);
        havePlayed = true;
    }
}
