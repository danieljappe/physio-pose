using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class FinalDesk : MonoBehaviour
{
    public GameObject FlaskNeeded;
    public TMP_Text orderResponseText;

    public Material Glass;
    public QueueManager queueManager;

    public PotionController MainScript;

    public int OrdersSuccessfullyDone;

    public SavingObjectPositions SavingObjectPositionsScript;

    public int MoneyEarned;

    public int MoneySupposedForPotion;

    public TextMeshProUGUI text;

    [SerializeField]
    private string successSound = "Success";
    [SerializeField]
    private string errorSound = "Error";

    // Start is called before the first frame update
    void Start()
    {
        SavingObjectPositionsScript = GetComponent<SavingObjectPositions>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    

    private void OnTriggerEnter(Collider other)
{
    if (other.tag == "Flask" && MainScript.IsStirred && MainScript.PotionFilled)
    {
        if (MainScript.AreIngredientsPutRight && other.name == FlaskNeeded.name)
        {
            // Correct response
            orderResponseText.text = GetRandomCorrectResponse();

            // Play the success sound
            AudioManager.Instance.PlaySound(successSound);

            OrdersSuccessfullyDone++;
            UpdatingMoney();
        }
        else
        {
            // Incorrect response
            orderResponseText.text = GetRandomIncorrectResponse();

            // Play the error sound
            AudioManager.Instance.PlaySound(errorSound);
        }

        MainScript.UnlockingAndClearCauldron();
        ResetingFlaskMat(other.gameObject);
        queueManager.DequeueAndMoveCustomer();
        SavingObjectPositionsScript.ResetAllPositions();
    }
}

// Function to get a random correct response string
private string GetRandomCorrectResponse()
{
    string[] correctResponses = {
        "The potion is perfect. Thank you!",
        "Everything looks great! Order complete.",
        "Well done! The potion is just right."
    };

    return correctResponses[UnityEngine.Random.Range(0, correctResponses.Length)];
}

// Function to get a random incorrect response string
private string GetRandomIncorrectResponse()
{
    string[] incorrectResponses = {
        "This is not what I asked for!",
        "Oops! That's not quite right.",
        "Almost there, but not quite!"
    };

    return incorrectResponses[UnityEngine.Random.Range(0, incorrectResponses.Length)];
}




    public void SetFlask(GameObject gameObject)
    {
        FlaskNeeded = gameObject;
    }

    private void ResetingFlaskMat(GameObject gameObject)
    {
        if (MainScript.PotionFilled)
        {
            MainScript.PotionFilled = false;
            MeshRenderer mesh = gameObject.gameObject.GetComponent<MeshRenderer>();
            Material[] materials = mesh.materials;

            if (materials[0].name.Contains("Switch"))
                materials[0] = Glass;
            else
                materials[1] = Glass;

            mesh.materials = materials;
        }
    }

    private void UpdatingMoney()
    {
        MoneyEarned += MoneySupposedForPotion;
        text.text = "Money Earned " + Convert.ToString(MoneyEarned);
    }
}