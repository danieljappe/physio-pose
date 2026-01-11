using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbCollecter : MonoBehaviour
{
    private int _score;
    public int getScore()
    {
        return _score;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Launcher"))
        {
            var launch = other.GetComponent<SceneLoader>();
            launch.LoadScene();
        }
    }
}
