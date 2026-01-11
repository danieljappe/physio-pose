using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    public List<Color> ColorList;

    private SwitchingCauldronColor SwitchingCauldronColor;
    
    public List<GameObject> IngredientsSupposed;

    public List<GameObject> ActualIngredients;

    public bool AreIngredientsPutRight; 

    // Start is called before the first frame update
    void Start()
    {
        //Detector = GetComponent<BoxCollider>();
        SwitchingCauldronColor = GetComponent<SwitchingCauldronColor>();
        
        ColorList = new List<Color>()
        {
            new Color (0x9C / 255F,0x3F / 255F, 0x40 / 255F, 0x00 / 255F),
            new Color (0x52 / 255F, 0x7D / 255F, 0x9F/ 255F, 0/ 255F),
            new Color (0x65 / 255F, 0x82/ 255F, 0x2F/ 255F, 0/ 255F),
            new Color (0x6F/ 255F, 0x61/ 255F, 0x9A/ 255F, 0/ 255F),
            new Color (0x57/ 255F, 0x5B/ 255F, 0x6F/ 255F, 0/ 255F)
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ingredient")
        {
            ActualIngredients.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ingredient")
        {
            ActualIngredients.Remove(other.gameObject);
            
        }
    }

    public void LockingCauldron()
    {
        //Debug.Log("Locking the cauldron");
        foreach (GameObject GO in ActualIngredients)
        {
            if (GO.name.Contains("Tail"))
            {
                SwitchingCauldronColor.ColorSelected = ColorList[GO.name[11] - 49];
                Debug.Log("Color Changed");
                break;
            }
        }

        AreIngredientsPutRight = ListsMotherMeGustaLaGasolinaaaaaaaaaaaaaaaaaa();
        Debug.Log(ListsMotherMeGustaLaGasolinaaaaaaaaaaaaaaaaaa());
        
        foreach (var ingr in ActualIngredients)
            Destroy(ingr);
        //Detector.enabled = false;
    }

    public void UnlockingAndClearCauldron()
    {
        //Debug.Log("Unlocking the cauldron");
        ActualIngredients.Clear();
    }

    private bool ListsMotherMeGustaLaGasolinaaaaaaaaaaaaaaaaaa()
    {
        if (ActualIngredients.Count == 0 || ActualIngredients.Count != IngredientsSupposed.Count)
        {
            Debug.Log("Exit 1");
            return false;
        }

        Dictionary<string, int> occurences = new Dictionary<string, int>();

        foreach (var GO in IngredientsSupposed)
        {
            if (occurences.ContainsKey(GO.name))
                occurences[GO.name]++;

            else
                occurences.Add(GO.name, 1);
        }

        foreach (var GO in ActualIngredients)
        {
            if (!occurences.ContainsKey(GO.name))
            {
                Debug.Log("Exit 2");
                return false;
            }

            else
                occurences[GO.name]--;
        }

        foreach (var (GO, n) in occurences)
        {
            if (n != 0)
                return false;
        }

        return true;
    }



}
