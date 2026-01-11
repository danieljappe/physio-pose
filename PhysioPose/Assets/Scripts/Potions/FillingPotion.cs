using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FillingPotion : MonoBehaviour
{
    // Start is called before the first frame update

    //public MeshRenderer mesh;

    //private Material MatToChange;

    public Stirring Stir;

    private SwitchingCauldronColor SwitchingCauldronColorScript;

    public Material MatToSwitchWith;
    
    
    void Start()
    {
        SwitchingCauldronColorScript = GetComponent<SwitchingCauldronColor>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Flask" && Stir.IsStirred)
        {
            MeshRenderer mesh = other.gameObject.GetComponent<MeshRenderer>();
            Material[] materials = mesh.materials;
            materials[0] = MatToSwitchWith;
            mesh.materials = materials;
            SwitchingCauldronColorScript.EmptyTheCauldron();

          
        }

        
    }
}
