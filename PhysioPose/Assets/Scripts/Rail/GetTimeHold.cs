using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GetTimeHold : MonoBehaviour
{
    //public GameObject MainObject;
    //private InfoObject Info; 
    
    //public GameObject Rail;
    public float RadiusSize;
    
    public bool enCollision = false;
    public float tempsCollision = 0f;
    private float LastTimeCollided;

    public RailScript RailScript_;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        //Info = MainObject.GetComponent<InfoObject>();
        
    }
    
    void Update()
    {
        if (enCollision)
        {
            tempsCollision += Time.deltaTime;
            //Debug.Log("Temps de collision : " + tempsCollision.ToString("F2") + " secondes");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hand")
        {
            enCollision = true;
            //Debug.Log($"temps pris : {tempsCollision}");
            //Debug.Log("Collision commencée avec : " + other.name);
        }
        
        
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Hand")
        {
            enCollision = false;
            //Debug.Log("Collision terminée avec : " + other.name);
        }
    }
}
