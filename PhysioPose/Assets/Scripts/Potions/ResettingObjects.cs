using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResettingObjects : MonoBehaviour
{
    public SavingObjectPositions SavingObjectPositionsScript;

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
        if (other.tag == "Flask" || other.tag == "Ingredient")
        {
            Debug.Log("Object touched the floor and has been replaced");
            (other.transform.position, other.transform.rotation) = SavingObjectPositionsScript.PosDico[other.gameObject];
        }
    }
}
