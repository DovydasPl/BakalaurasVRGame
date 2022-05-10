using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketWithKeyCheck : XRSocketInteractor
{
    public GameObject targetKey;
    private bool isKeyInserted;

    void Start() {
        Initialize();
    }

    public void Initialize() {
        isKeyInserted = false;
    }

    public void OnSelectEnter() {
        isKeyInserted = true;
    }    

    public void SetTargetKey(GameObject go) {
        targetKey = go;
    }

    public void OnHoverEnter() {
        targetKey.GetComponentInChildren<MeshCollider>().isTrigger = true;
    }

    public void OnHoverExit() {
        targetKey.GetComponentInChildren<MeshCollider>().isTrigger = false;
    }

    public bool IsKeyInserted() {
        return isKeyInserted;
    }
    
    public override bool CanHover(XRBaseInteractable interactable) {
        return base.CanHover(interactable) && MatchTag(interactable);
    }

    public override bool CanSelect(XRBaseInteractable interactable) {
        return base.CanSelect(interactable) && MatchTag(interactable);
    }

    private bool MatchTag(XRBaseInteractable interactable) {
        return interactable.gameObject == targetKey;
    }

}
