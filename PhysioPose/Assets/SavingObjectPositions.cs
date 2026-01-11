using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class SavingObjectPositions : MonoBehaviour
{
    public List<GameObject> ParentIngredients;
    public Dictionary<GameObject, (Vector3, Quaternion)> PosDico;
    public Dictionary<string, int> MoneyDic;

    public List<int> TailsPrices;
    public List<int> EyesPrices;

    public List<int> HerbPouchPrices;

    public List<int> HerbPowderPrices;

    public List<int> FrogLegsPrices;

    public List<int> BeetlesPrices;

    public List<int> FlasksPrices;



    // Start is called before the first frame update
    void Start()
    {
        PosDico = new Dictionary<GameObject, (Vector3, Quaternion)>();
        MoneyDic = new Dictionary<string, int>();
        InitDico();
        InitMoneyDico();
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("alkjhvliauhrefukhuaelrfaeghrf");
            ResetAllPositions();
        }
        */
    }

    public void InitDico()
    {
        foreach (var GO in ParentIngredients)
        {
            for (int i = 0; i < GO.transform.childCount; i++)
            {
                GameObject gameObject = GO.transform.GetChild(i).gameObject;
                PosDico.Add(gameObject, (gameObject.transform.position, gameObject.transform.rotation));
                //Debug.Log($"the object {gameObject.name} has been saved at the location {gameObject.transform.position}");
            }
        }
    }

    public void ResetAllPositions()
    {
        foreach (var GO in ParentIngredients)
        {
            for (int i = 0; i < GO.transform.childCount; i++)
            {
                GameObject gameObject = GO.transform.GetChild(i).gameObject;
                (gameObject.transform.position, gameObject.transform.rotation)  = PosDico[gameObject];
            }
        }
    }

    public void InitMoneyDico()
    {
        List<List<int>> BigList = new List<List<int>> (){TailsPrices, FlasksPrices, HerbPowderPrices, BeetlesPrices, EyesPrices, FrogLegsPrices, HerbPouchPrices};

        int index = 0;
        foreach (var GO in ParentIngredients)
        {
            
            if (GO.transform.childCount != BigList[index].Count)
                Debug.LogError("The number of objects and the number of prices given need to be the same");

            for (int i = 0; i < GO.transform.childCount; i++)
            {
                GameObject gameObject = GO.transform.GetChild(i).gameObject;
                MoneyDic.Add(gameObject.name, BigList[index][i]);
                Debug.Log($"the object {gameObject.name} has been saved at the price {BigList[index][i]}");
            }
            index++;
        }
    }

}
