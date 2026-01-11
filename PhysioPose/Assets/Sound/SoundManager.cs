using UnityEngine;

public class SoundManager : MonoBehaviour
{
    void Sounds()
    {
        // Play the squishy sound
        AudioManager.Instance.PlaySound("Squishy");

        // Play the glass sound
        AudioManager.Instance.PlaySound("Glass");

        // Play the pouch sound
        AudioManager.Instance.PlaySound("Pouch");

        // Play the new order sound
        AudioManager.Instance.PlaySound("newOrderSound");

        // Play stirring sound
        AudioManager.Instance.PlaySound("Stirring");

        // Play the success sound
        AudioManager.Instance.PlaySound("Success");

        // Play the error sound
        AudioManager.Instance.PlaySound("error");

        //Male talking
        AudioManager.Instance.PlaySound("Male-Talking");

        //Male thanks
        AudioManager.Instance.PlaySound("Male-Thanks");

        //Female talking
        AudioManager.Instance.PlaySound("Female-Talking");

        //Female thanks
        AudioManager.Instance.PlaySound("Female-Thanks");
    }

}