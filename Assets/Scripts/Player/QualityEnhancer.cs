using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class QualityEnhancer : MonoBehaviour
{
    void Start()
    {
        XRSettings.eyeTextureResolutionScale = 10f;
    }
}
