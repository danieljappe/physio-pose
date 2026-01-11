using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script reset the position of the object that touch the ground if it's a launcher or an ingredient
public class ResetPosition : MonoBehaviour
{
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
        if (other.CompareTag("Launcher") || other.CompareTag("Ingredient"))
        {
            var reset = other.GetComponent<PositionSaver>();
            reset.resetPos();
        }
    }
}
