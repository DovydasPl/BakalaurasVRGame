using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public Text timer;
    public float maxTime;
    public AudioClip timerBeep;
    public AudioClip timeEndBeep;

    private AudioSource audioSource;
    private bool isTimeUp;
    private int secondsTimeStamp;
    private int minutesTimeStamp;
    [SerializeField] private float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isTimeUp = false;
        secondsTimeStamp = 0;
        minutesTimeStamp = 0;
        currentTime = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTimeUp) return;

        currentTime -= Time.deltaTime;
       
        PlaySound();
       
        if(currentTime <= 0) {
            isTimeUp = true;
            return;
        }

        timer.text = CurrentTime();
    }

    public bool IsTimeUp() {
        return isTimeUp;
    }

    private void PlaySound() {
        int minutes = (int) Mathf.Floor(currentTime / 60);
        int seconds = (int) Mathf.Floor(currentTime % 60);

        if(minutesTimeStamp == minutes && secondsTimeStamp == seconds) return;

        minutesTimeStamp = minutes;
        secondsTimeStamp = seconds;

        if(minutes == 0 && seconds == 0) {
            //Play lose sound
            audioSource.PlayOneShot(timeEndBeep);
            return;
        }

        if(minutes == 0 && seconds <= 10) {
            //Play sound
            audioSource.PlayOneShot(timerBeep);
            Debug.Log("last 10 seconds");
            return;
        }

        if(minutes == 0 && seconds == 30) {
            //Play sound
            audioSource.PlayOneShot(timerBeep);

            Debug.Log("last 30 seconds");
            return;
        }

        if(minutes <=10 && seconds == 0) {
            //Play sound
            audioSource.PlayOneShot(timerBeep);

            Debug.Log("last 10 minutes");
            
            return;
        }

        if(minutes % 10 == 0 && seconds == 0) {
            audioSource.PlayOneShot(timerBeep);
            //Play sound
            Debug.Log("last 50 minutes minutes");
            return;
        }
    }

    private string CurrentTime() {
        string seconds = Mathf.Floor(currentTime % 60).ToString("00");
        string minutes = Mathf.Floor(currentTime / 60).ToString("00");

        return minutes + ":" + seconds;
    }
}
