using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{

    public GameObject regularLabel;
    public GameObject letterLabel;

    private bool wasPickedUp;

    void Start() {
        if(regularLabel != null) regularLabel.SetActive(true);
        if(letterLabel != null) letterLabel.SetActive(false);
        
        wasPickedUp = false;
    }

    public void SwitchLabels() {
        regularLabel.SetActive(false);
        letterLabel.SetActive(true);
    }

    public void PickedUp() {
        wasPickedUp = true;
    }
    
    public bool WasPickedUp() {
        return wasPickedUp;
    }
}
