using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerDisabler : MonoBehaviour
{
    public Image fadeImage;
    
    private bool isDisableDone;

    void Start() {
        isDisableDone = true;
    }

    public IEnumerator Disable() {
        isDisableDone = false;
        
        GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
        GetComponent<ActionBasedSnapTurnProvider>().enabled = false;
        GameObject.Find("RightHand Base Controller").GetComponent<ActionBasedController>().enabled = false;
        GameObject.Find("LeftHand Base Controller").GetComponent<ActionBasedController>().enabled = false;
        GameObject.Find("RightHand Ray Controller").GetComponent<ActionBasedController>().enabled = false;
        GameObject.Find("LeftHand Ray Controller").GetComponent<ActionBasedController>().enabled = false;

        while(fadeImage.color.a < 1f) {
            Color col = fadeImage.color;
            col.a += Time.deltaTime;
            fadeImage.color = col;
            yield return new WaitForSeconds(0.01f);
        }   

        yield return new WaitForSeconds(0f);
        isDisableDone = true;
    }
   
    public bool IsDisableDone() {
        return isDisableDone;
    }
}
