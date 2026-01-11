using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public List<GameObject> customerPrefabs = new List<GameObject>();
    public QueueManager queueManager;
    public int activeCustomerCount = 0;
    private bool isSpawning = false;
    public float spawnInterval = 2f;
    
    // list of spawn points
    public Transform[] spawnPoints;

    void Update(){
        if(!isSpawning){
            StartCoroutine(SpawnCustomers());
        }
    }

    // coroutine to spawn customers in intervals
    IEnumerator SpawnCustomers()
    {
        while (customerPrefabs.Count > 0 && activeCustomerCount < 3)
        {
            isSpawning = true;
            yield return new WaitForSeconds(spawnInterval);

            int randomIndex = Random.Range(0, customerPrefabs.Count);
            GameObject customerToSpawn = customerPrefabs[randomIndex];

            // pick a random spawn point from the spawnpoint array
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // spawn character
            GameObject newCustomer = Instantiate(customerToSpawn, spawnPoint.position, spawnPoint.rotation);
            newCustomer.GetComponent<CustomerAI>().queueManager = queueManager;
            // increment active customer count
            activeCustomerCount++;
            //Debug.Log("Active Customers = " + activeCustomerCount);

            // remove the spawned customer from the list
            customerPrefabs.RemoveAt(randomIndex);
        }
        isSpawning = false;
    }
}
