using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Lock : MonoBehaviour
{
    public GameObject[] lockedObjects;
    public GameObject[] objectsInside;

    public void ToggleLockedObjects(bool value) {
        if(lockedObjects == null) return;
        for(int i = 0; i < lockedObjects.Length; i++) {
            lockedObjects[i].GetComponent<XRGrabInteractable>().enabled = value;
        }
    }

    public void ToggleObjectsInside(bool value) {
        if(objectsInside == null) return;
        foreach(GameObject objectInside in objectsInside) {
            objectInside.GetComponent<XRGrabInteractable>().enabled = value;
        }
    }

    public GameObject[] GetLockedObjects() {
        return lockedObjects;
    }

    public GameObject[] GetObjectsInside() {
        return objectsInside;
    }
}

