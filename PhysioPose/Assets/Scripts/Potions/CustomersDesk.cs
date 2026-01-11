using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomersDesk : MonoBehaviour
{
    public GameObject FlaskNeeded;

    public Cauldron cauld;

    public Stirring StirrScript;

    public int OrdersSuccessfullyDone;
    
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
        if (other.tag == "Flask")
        {
            if (cauld.AreIngredientsPutRight && other.name == FlaskNeeded.name)
            {
                Debug.Log("The order was perfect. PERFECT");
                OrdersSuccessfullyDone++;
            }
            
            else
                Debug.Log("Nah bro,u too bad. You should seriously consider changing your job");
            
            StirrScript.ResetStirringProcess();
        }
    }

    public void SetFlask(GameObject gameObject)
    {
        FlaskNeeded = gameObject;
    }
}
