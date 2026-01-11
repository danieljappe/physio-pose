using UnityEngine;
using FMODUnity;

public class Squishy_grab : MonoBehaviour
{
    // FMOD event references for the grab and drop sounds
    
    public EventReference Squishy;
   
    private bool isGrabbed = false;

    void Update()
    {
        // This is a placeholder for your grab/drop detection logic
        // Replace with your actual input or interaction handling

        if (Input.GetKeyDown(KeyCode.G)) // Example key for grabbing
        {
            GrabItem();
        }
        
        if (Input.GetKeyDown(KeyCode.D)) // Example key for dropping
        {
            DropItem();
        }
    }

    public void GrabItem()
    {
        if (!isGrabbed)
        {
            // Play the grab sound
            RuntimeManager.PlayOneShot(Squishy, transform.position);
            isGrabbed = true;
        }
    }

    public void DropItem()
    {
        if (isGrabbed)
        {
            // Play the drop sound
            RuntimeManager.PlayOneShot(Squishy, transform.position);
            isGrabbed = false;
        }
    }
}