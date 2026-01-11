using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    public CustomerSpawner customerSpawner;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            //Debug.Log("Customer " + other.gameObject.name + " has reached the exit.");
            customerSpawner.activeCustomerCount--;
            //Debug.Log(customerSpawner.activeCustomerCount);
            Destroy(other.gameObject);
        }
    }
}