using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] int goldReward = 25;

    Bank bank;

    void Start()
    {
        bank = FindObjectOfType<Bank>();    
    }

    public void RewardGold()
    {
        if(bank == null) { return; }
        bank.Deposit(goldReward);
    }
}
