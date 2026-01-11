using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager: MonoBehaviour
{
    public List<GameObject> FruitList;
    private List<GameObject> CurrentEnabledFruits = new List<GameObject>();
    private GameObject SelectedFruit;
    
    public List<Color> ColorList;

    private System.Random rand;

    public Chrono chrono;
    void Start()
    {
        FruitList = gameObject.GetComponentsInChildren<Transform>()
            .Where(t => t != gameObject.transform)
            .Select(t => t.gameObject)
            .ToList();
        
        DisableAll();
        
        rand = new System.Random();
    }

    private void DisableAll()
    {
        if (SelectedFruit is not null)
            SelectedFruit.tag = "Untagged";
            
        FruitList.ForEach(t => t.SetActive(false));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            AppearFruitsWithOneColourSelected(0, 3);
        if(Input.GetKeyDown(KeyCode.B))
            RandomizeColours();
    }

    private void RandomizeColours()
    {
        foreach (var fruit in CurrentEnabledFruits)
        {
            fruit.GetComponent<MeshRenderer>().material.color = ColorList[rand.Next(ColorList.Count)];
        }
    }

    private void AppearingFruits(int n)
    {
        DisableAll();
        CurrentEnabledFruits.Clear();
        
        List<int> list = new List<int>();
        int indexChoosen;
        for (int i = 0; i < n; i++)
        {
            while ( list.Contains(indexChoosen = rand.Next(FruitList.Count)))
            {}
            list.Add(indexChoosen);
        }
        
        foreach (var index in list)
        {
            CurrentEnabledFruits.Add(FruitList[index]);
            FruitList[index].SetActive(true);
        }
    }

    public void AppearFruitsWithOneColourSelected(int index, int n) //choose your color by putting the index of the color you want from the Color List
    {
        if (chrono.IsCLockRunning)
            return;
        
        AppearingFruits(n);
        Color color;
        foreach (var fruit in CurrentEnabledFruits)
        {
            do
            {
                color = ColorList[rand.Next(ColorList.Count)];
            } while (color == ColorList[index]);

            fruit.GetComponent<MeshRenderer>().material.color = color;
        }

        int rand_index = rand.Next(CurrentEnabledFruits.Count);
        CurrentEnabledFruits[rand_index].GetComponent<MeshRenderer>().material.color = ColorList[index];
        CurrentEnabledFruits[rand_index].tag = "Selected";
        SelectedFruit = CurrentEnabledFruits[rand_index];
        
        chrono.RunClockAgain();
    }
}
