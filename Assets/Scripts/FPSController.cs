using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSController : MonoBehaviour
{

    public int FPSCount;
    public int frameCount;
    public Text debugText;
    // Start is called before the first frame update
    void Start()
    {
        FPSCount = 0;
        frameCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float current = 0;
        current = (int)(1f / Time.unscaledDeltaTime);
        FPSCount += (int)current;
        frameCount++;
        debugText.text = "Current FPS: " + current.ToString();
        debugText.text += "\nAverage FPS: " + ((float)FPSCount/frameCount).ToString();
    }
}
