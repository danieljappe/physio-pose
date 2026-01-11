using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField]
    private EventReference squishySound;
    [SerializeField]
    private EventReference glassSound;
    [SerializeField]
    private EventReference pouchSound;
    [SerializeField]
    private EventReference newOrderSound;
    [SerializeField]
    private EventReference successSound;
    [SerializeField]
    private EventReference errorSound;
    [SerializeField]
    private EventReference StirringSound;
    [SerializeField]
    private EventReference MaleTalking;
    [SerializeField]
    private EventReference MaleThanks;
    [SerializeField]
    private EventReference FemaleTalking;
    [SerializeField]
    private EventReference FemaleThanks;

    private EventInstance currentStirringSound; // To store the current stirring sound instance


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(string name)
    {
        EventReference soundEvent = GetSoundEvent(name);

        if (soundEvent.IsNull)
        {
            Debug.LogWarning("SoundEvent for " + name + " is not assigned!");
            return;
        }

        if (name == "Stirring")
        {
            if (currentStirringSound.isValid())
            {
                Debug.LogWarning("Stirring sound is already playing.");
                return;
            }

            currentStirringSound = RuntimeManager.CreateInstance(soundEvent);
            currentStirringSound.start();
        }
        else
        {
            RuntimeManager.PlayOneShot(soundEvent);
        }
    }

    public void StopSound(string name)
    {
        /*
        if (name == "Stirring" && currentStirringSound.isValid())
        {
            currentStirringSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            currentStirringSound.release();
            currentStirringSound.clearHandle();
        } */

        if (name == "Stirring" && currentStirringSound.isValid())
        {
            FMOD.RESULT result;

            // Stop the sound with fade out
            result = currentStirringSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            if (result != FMOD.RESULT.OK)
            {
                Debug.LogError("Failed to stop sound: " + FMOD.Error.String(result));
                return;
            }
            else
            {
                //Debug.Log("Sound stopped successfully.");
            }

            // Release the sound
            result = currentStirringSound.release();
            if (result != FMOD.RESULT.OK)
            {
                Debug.LogError("Failed to release sound: " + FMOD.Error.String(result));
                return;
            }
            else
            {
                //Debug.Log("Sound released successfully.");
            }

            // Clear the handle
            currentStirringSound.clearHandle();
            //Debug.Log("Sound handle cleared.");
        }
        else
        {
            if (!currentStirringSound.isValid())
            {
                Debug.LogError("Sound is not valid.");
            }
            if (name != "Stirring")
            {
                Debug.LogError("Incorrect sound name.");
            }
        }
    }


    public string voiceLineEvent;

    protected FMOD.Studio.EventInstance voiceLineInstance;

    public virtual void PlayVoiceLine()
    {
        voiceLineInstance = FMODUnity.RuntimeManager.CreateInstance(voiceLineEvent);
        voiceLineInstance.start();
        voiceLineInstance.release(); // Release instance once it has played
    }

    private EventReference GetSoundEvent(string name)
    {
        switch (name)
        {
            case "squishy":
                return squishySound;
            case "glass":
                return glassSound;
            case "pouch":
                return pouchSound;
            case "new order":
                return newOrderSound;
            case "Success":
                return successSound;
            case "Error":
                return errorSound;
            case "Stirring":
                return StirringSound;
            case "Male-Talking":
                return MaleTalking;
            case "Male-Thanks":
                return MaleThanks;
            case "Female-Talking":
                return FemaleTalking;
            case "Female-Thanks":
                return FemaleThanks;
            default:
                Debug.LogWarning("Sound: " + name + " not found!");
                return new EventReference(); // Return a default event reference if sound is not found
        }
    }
}
