using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class QueueManager : MonoBehaviour
{
    public Transform[] queuePoints; // Assign these in the Inspector
    public Transform exitWaypoint; // Assign the target waypoint in the Inspector
    public Queue<GameObject> customerQueue = new Queue<GameObject>();

    public void EnqueueCustomer(GameObject customer)
    {
        customerQueue.Enqueue(customer);
        //Debug.Log("Customer " + customer.name + " queued up at position " + customerQueue.Count);
        AssignQueuePoints();
        //Debug.Log("Enqueuing customer, Queue size = " + customerQueue.Count);
    }

    public void DequeueAndMoveCustomer()
    {
        if (customerQueue.Count > 0)
        {
            //Debug.Log("Customer Dequeuing");
            GameObject customer = customerQueue.Dequeue();
            customer.GetComponent<CustomerAI>().isFirstInQueue = false;
            AssignQueuePoints();
            SendToWaypoint(customer);
            GameObject orderListCanvas = GameObject.FindWithTag("Order");
            Destroy(orderListCanvas);
        } else {
            //Debug.Log("Queue is empty");
        }
    }

    private void SendToWaypoint(GameObject customer)
    {
        //Debug.Log("Sending customer to exitWaypoint");
        NavMeshAgent agent = customer.GetComponent<NavMeshAgent>();
        agent.SetDestination(exitWaypoint.position);
    }

    private void AssignQueuePoints()
    {
        int i = 0;
        foreach (GameObject customer in customerQueue)
        {
            if (i < queuePoints.Length)
            {
                //Debug.Log("Enqueuing customer, Queue size = " + customerQueue.Count);
                NavMeshAgent agent = customer.GetComponent<NavMeshAgent>();
                agent.SetDestination(queuePoints[i].position);
                customer.GetComponent<CustomerAI>().destination = queuePoints[i]; // Set the destination

                if (i == 0)
                {
                    customer.GetComponent<CustomerAI>().isFirstInQueue = true;
                    //Debug.Log(customer.name + " is number 1 in queue");
                }
            }
            i++;
        }
    }
}
