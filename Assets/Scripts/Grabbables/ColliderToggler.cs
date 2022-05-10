using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderToggler : MonoBehaviour
{
    private BoxCollider collider;

    void Start() {
        collider = GetComponent<BoxCollider>();
    }

    public void Toggle(bool value) {
        collider.isTrigger = value;
    }
}
