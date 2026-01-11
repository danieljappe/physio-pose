using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chrono : MonoBehaviour
{
    // Start is called before the first frame update

    public Image image;
    public float TimeMax;
    private float CurrentTime = -1f;

    public AudioSource ClockSound;

    public bool IsCLockRunning => CurrentTime > 0f;
    
    void Start()
    {
        //CurrentTime = TimeMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentTime >= 0f)
            CurrentTime -= Time.deltaTime;
        image.fillAmount = CurrentTime / TimeMax;

        if (CurrentTime <= 0f)
        {
            //gameObject.SetActive(false);
            ClockSound.Stop();
        }

        else if (CurrentTime <= TimeMax/2 && !ClockSound.isPlaying)
            ClockSound.Play();
        
        //if (Input.GetKeyDown(KeyCode.A))
            //RunClockAgain();
    }

    public void RunClockAgain()
    {
        CurrentTime = TimeMax;
        //gameObject.SetActive(true);
    } 

}
