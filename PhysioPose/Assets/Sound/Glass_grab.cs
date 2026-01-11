using UnityEngine;
using FMODUnity;

public class Glass_grab : MonoBehaviour
{
    // FMOD event references for the grab and drop sounds
    
    public EventReference Glass;
   
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
            RuntimeManager.PlayOneShot(Glass, transform.position);
            isGrabbed = true;
        }
    }

    public void DropItem()
    {
        if (isGrabbed)
        {
            // Play the drop sound
            RuntimeManager.PlayOneShot(Glass, transform.position);
            isGrabbed = false;
        }
    }
}