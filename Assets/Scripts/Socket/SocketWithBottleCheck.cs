using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketWithBottleCheck : XRSocketInteractor
{
    public GameObject targetGameObject;
    public bool hasCorrectBottle = false;
    public AudioClip bottlePlaceSound;

    private AudioSource audioSource;
    private Vector3 attachStartPosition;
    private GameObject selectedGameObject;

    void Start() {
        Initialize();
    }

    public void Initialize() {
        audioSource = GetComponent<AudioSource>();
        attachStartPosition = attachTransform.position;
    }

    public void SetTargetGameObject(GameObject go) {
        targetGameObject = go;
    }

    public void SetSelectedGameObject(GameObject go) {
        selectedGameObject = go;
    }

    public GameObject SelectedGameObject() {
        return selectedGameObject;
    }

    public bool HasBottlePlaced() {
        return selectedGameObject != null;
    }

    public bool HasCorrectBottle() {
        return hasCorrectBottle;
    }

    public void OnHoverEnter() {
        attachTransform.position = attachStartPosition + new Vector3(0, selectedGameObject.GetComponent<XRGrabInteractable>().attachTransform.localPosition.y * 0.6f, 0);
    }

    public void OnSelectEnter() {
        if(audioSource != null) audioSource.PlayOneShot(bottlePlaceSound);
        if(selectedGameObject == targetGameObject) {
            hasCorrectBottle = true;
        }
    }

    public void OnSelectExit() {
        hasCorrectBottle = false;
        selectedGameObject = null;
    }

    public void OnHoverExit() {
        selectedGameObject = null;
    }

    public override bool CanHover(XRBaseInteractable interactable) {
        bool canHover = base.CanHover(interactable) && MatchTag(interactable) && (selectedGameObject == null || selectedGameObject == interactable.gameObject);
        
        if(canHover) selectedGameObject = interactable.gameObject;

        return canHover;
    }

    public override bool CanSelect(XRBaseInteractable interactable) {
        bool canSelect = base.CanHover(interactable) && MatchTag(interactable) && (selectedGameObject == null || selectedGameObject == interactable.gameObject);

        if(canSelect) selectedGameObject = interactable.gameObject;

        return canSelect;
    }

    private bool MatchTag(XRBaseInteractable interactable) {
        return interactable.gameObject.tag == "Bottle";
    }
}
