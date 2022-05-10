using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;

public class UIController : MonoBehaviour
{
    [SerializeField]
    GameObject[] UIControllers;

    [SerializeField]
    GameObject[] BaseControllers;

    [SerializeField]
    GameObject rig;

    [SerializeField]
    InputActionReference inputActionReferenceUISwitcher;

    bool isUICanvasActive = false;

    [SerializeField]
    GameObject UIGameObject;

    [SerializeField]
    Transform cameraOffset;


  
    private void OnEnable()
    {
        inputActionReferenceUISwitcher.action.performed += ActivateUIMode;
    }
    
    private void OnDisable()
    {
        inputActionReferenceUISwitcher.action.performed -= ActivateUIMode;

    }

    private void Start()
    {
        //Deactivating UI Gameobject by default
        if (UIGameObject !=null)
        {
            UIGameObject.SetActive(false);

        }

    }

    /// <summary>
    /// This method is called when the player presses UI Switcher Button which is the input action defined in Default Input Actions.
    /// When it is called, UI interaction mode is switched on and off according to the previous state of the UI Canvas.
    /// </summary>
    /// <param name="obj"></param>
    private void ActivateUIMode(InputAction.CallbackContext obj)
    {
        ToggleUI();
    }

    public void ToggleUI() {
        if (!isUICanvasActive)
        {
            isUICanvasActive = true;

            foreach(GameObject BaseController in BaseControllers) {
                BaseController.GetComponent<XRDirectInteractor>().enabled = false;
            }

            rig.GetComponent<ActionBasedSnapTurnProvider>().enabled = false;
            rig.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;

            UIGameObject.transform.position = new Vector3(UIGameObject.transform.position.x, cameraOffset.position.y, UIGameObject.transform.position.z);
            UIGameObject.SetActive(true);
        }
        else
        {
            isUICanvasActive = false;

            foreach(GameObject BaseController in BaseControllers) {
                BaseController.GetComponent<XRDirectInteractor>().enabled = true;
            }

            rig.GetComponent<ActionBasedSnapTurnProvider>().enabled = true;
            rig.GetComponent<ActionBasedContinuousMoveProvider>().enabled = true;

            UIGameObject.SetActive(false);
        }
    }
}
