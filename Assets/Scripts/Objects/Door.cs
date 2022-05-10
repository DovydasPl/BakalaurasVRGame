using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Lock
{
    public SocketWithKeyCheck keySocket;
    public float winOpenThreshold;
    public AudioClip unlockClip;
    
    private bool isOpened;
    private Animator animator;
    private AudioSource audioSource;

    void Start() {
        Initialize();
    }

    public void Initialize() {
        audioSource = GetComponent<AudioSource>();
        ToggleLockedObjects(false);
        isOpened = false;
        animator = GetComponent<Animator>();
    }

    public Animator GetAnimator() {
        return animator;
    }

    void Update() {
        if(this.gameObject.transform.rotation.y >= winOpenThreshold) isOpened = true;
    }

    public bool IsOpened() {
        return isOpened;
    }

    public bool IsKeyInserted() {
        return keySocket.IsKeyInserted();
    }

    public void Unlock() {
        if(audioSource != null) audioSource.PlayOneShot(unlockClip);

        if(animator != null) animator.SetBool("isKeyInserted", IsKeyInserted());

        ToggleLockedObjects(true);
    }
}
