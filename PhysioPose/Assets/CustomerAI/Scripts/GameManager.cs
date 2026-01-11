using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public QueueManager queueManager;
    public CustomerSpawner customerSpawner;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("pressed Z");
            queueManager.DequeueAndMoveCustomer();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Active customer = " + customerSpawner.activeCustomerCount);
        }
    }
}
