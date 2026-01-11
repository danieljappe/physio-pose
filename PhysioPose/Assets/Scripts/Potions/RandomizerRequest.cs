using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RandomizerRequest : MonoBehaviour
{
    // Start is called before the first frame update
    public int NumberIngredientsWanted;
    
    public List<GameObject> Tails;
    
    public List<GameObject> Eyes;
    public List<GameObject> Beetle;
    public List<GameObject> HerbPowder;
    public List<GameObject> Flasks;

    public GameObject FrogLegs;
    public int FrogLegsProba;
    
    public GameObject HerbPouch;
    public int HerbPouchProba;

    private System.Random rand;

    private List<List<GameObject>> ListGroupIngredient;



    void Start()
    {
        rand = new System.Random();
        ListGroupIngredient = new List<List<GameObject>>() { Eyes, Beetle, HerbPowder};
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject GetRandomItem(List<GameObject> list) => list[rand.Next(list.Count)];

    public (List<GameObject>, GameObject) GetOrder()
    {
        bool GetSpecialItem = false;
        
        if (NumberIngredientsWanted == 0 || NumberIngredientsWanted >= 5)
        {
            Debug.LogError("CAN NOT RANDOMIZE, NumberIngredientsWanted wrong");
            return (null, null);
        }
        
        List<int> IndexAlreadySelected = new List<int>();
        int LocalNumberWanted = NumberIngredientsWanted - 1;
        
        GameObject tail = GetRandomItem(Tails);
        GameObject flask = GetRandomItem(Flasks);
        List<GameObject> FinalIngredients = new List<GameObject>() {tail};

        if (LocalNumberWanted >= 1 && rand.Next(FrogLegsProba) == 0)
        {
            FinalIngredients.Add(FrogLegs);
            LocalNumberWanted -= 1;
            GetSpecialItem = true;
        }

        if (LocalNumberWanted >= 1 && !GetSpecialItem && rand.Next(HerbPouchProba) == 0)
        {
            FinalIngredients.Add(HerbPouch);
            LocalNumberWanted -= 1;
        }
        
        for (int i = 0; i < LocalNumberWanted; i++)
        {
            int index;

            do
            {
                index = rand.Next(ListGroupIngredient.Count);
            } while (IndexAlreadySelected.Contains(index));
            
            IndexAlreadySelected.Add(index);
            
            FinalIngredients.Add(GetRandomItem(ListGroupIngredient[index]));
        }

        return (FinalIngredients, flask);
    }

    
    
    
}
