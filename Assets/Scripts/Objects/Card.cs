using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    private bool wasPickedUp;

    // Start is called before the first frame update
    void Start()
    {
        wasPickedUp = false;    
    }

    public void PickedUp() {
        wasPickedUp = true;
    }

    public bool WasPickedUp() {
        return wasPickedUp;
    }
}
