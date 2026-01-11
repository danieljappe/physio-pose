using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField] int cost = 75;
    public bool UnlockItem (Ingredient ingredient, Vector3 position)
    {
        Bank bank = GetComponent<Bank>();

        if (bank == null)
        {
            return false;
        }

        if (bank.currentBalance >= cost)
        {
            Instantiate(ingredient, position, Quaternion.identity);
            bank.Withdraw(cost);
            return true;
        }
        return false;
    }
    }
