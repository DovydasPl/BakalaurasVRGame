using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketWithCardCheck : XRSocketInteractor
{
    public string targetTag;
    private bool isCardInserted;

    void Start() {
        Initialize();
    }

    public void Initialize() {
        isCardInserted = false;
    }

    public void OnSelectEnter() {
        isCardInserted = true;
    }    

    public bool IsCardInserted() {
        return isCardInserted;
    }
    
    public override bool CanHover(XRBaseInteractable interactable) {
        return base.CanHover(interactable) && MatchTag(interactable);
    }

    public override bool CanSelect(XRBaseInteractable interactable) {
        return base.CanSelect(interactable) && MatchTag(interactable);
    }

    private bool MatchTag(XRBaseInteractable interactable) {
        return interactable.gameObject.tag == targetTag;
    }

}
