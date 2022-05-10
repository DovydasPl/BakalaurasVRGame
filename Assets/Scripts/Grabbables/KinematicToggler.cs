using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicToggler : MonoBehaviour
{
    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void ToggleKinematic(bool value) {
        rigidbody.isKinematic = value;
    }
}
