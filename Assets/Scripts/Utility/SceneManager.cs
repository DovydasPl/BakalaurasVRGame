using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public void RedirectToScene(int index) {
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }
}
