using UnityEngine;
using UnityEngine.AI;
using FMODUnity;

public class CustomerAI : MonoBehaviour
{
    private Animator animator;
    public QueueManager queueManager; // Reference to the QueueManager
    private NavMeshAgent agent;
    public Transform playerPosition;
    public float turnSpeed = 3f; // Speed at which the object turns
    public bool isFirstInQueue;
    public Transform destination;
    private bool hasReachedDestination = false;

    [SerializeField]
    private EventReference FemaleTalking;
    [SerializeField]
    private EventReference MaleTalking;

    public enum Gender { Male, Female }
    public Gender customerGender;

    void Start()
    {
        GameObject gameObject = this.gameObject;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        queueManager.EnqueueCustomer(gameObject);
    }

    void Update()
    {
        animator.SetFloat("WalkSpeed", agent.velocity.magnitude);
        // Update animation based on movement
        if (agent.velocity.magnitude < 0.1f)
        {
            // Calculate direction from the object to the player
            Vector3 direction = playerPosition.position - transform.position;

            // Calculate the target rotation
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Smoothly interpolate between the current rotation and the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }

        if (isFirstInQueue && !hasReachedDestination)
        {
            CheckIfReachedDestination();
        }
    }

    public void CheckIfReachedDestination()
    {
        if (destination != null && !agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    OnReachedDestination();
                }
            }
        }
    }
    private void OnReachedDestination()
    {
        //Debug.Log(name + " has reached the destination.");
        hasReachedDestination = true;

        // If customer is first in queue, find CustomerOrdersGenerator and enable it
        Transform CustomerOrdersGenerator = gameObject.transform.Find("CustomerOrdersGenerator");
        GameObject child = CustomerOrdersGenerator.gameObject;

        animator.SetTrigger("Talk");

        // Play appropriate voice based on gender
        if (customerGender == Gender.Female)
        {
            //Debug.Log("Playing Female Talking Sound: " + FemaleTalking.Path);
            AudioManager.Instance.PlaySound("Female-Talking");
        }
        else if (customerGender == Gender.Male)
        {
            //Debug.Log("Playing Male Talking Sound: " + MaleTalking.Path);
            AudioManager.Instance.PlaySound("Male-Talking");
        }

        child.GetComponent<PrintingOrder>().GenerateOrderPrintItAndSentItToTheCauldron();
    }
}