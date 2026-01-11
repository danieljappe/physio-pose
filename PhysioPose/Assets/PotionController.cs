using FMOD.Studio; // Added namespace for EventInstance
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionController : MonoBehaviour
{

    //switchingCauldronColor
    public Material SwitchingMat;

    public Material WaterMat;

    private MeshRenderer rend;

    private Color InitialColor;

    public Color ColorSelected;

    private Color TmpColor;

    public bool IsCauldronLocked;
    public bool PotionFilled;


    //Cauldron
    public List<Color> ColorList;

    public List<GameObject> IngredientsSupposed;

    public List<GameObject> ActualIngredients;

    public bool AreIngredientsPutRight;


    //stirring
    private Vector3 PositionFrameBefore;
    public float DistanceToStirr;
    private float DistanceStirred;

    private bool CanStirr;

    public float Percentage;

    private Vector3 refe;

    public bool IsStirred => Percentage >= 1;
    public bool StirredBegin => Percentage > 0;

    public FinalDesk desk;


    // FMOD event reference for stirring sound
    [SerializeField]
    private string stirringSound = "Stirring";

    private bool isStirring = false;


    //main start
    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        IsCauldronLocked = false;

        InitialColor = new Color(1, 1, 1)
        {
            a = 40f / 255f
        };
        WaterMat.color = InitialColor;


        ColorList = new List<Color>()
        {
            new Color (0x9C / 255F,0x3F / 255F, 0x40 / 255F, 0x00 / 255F),
            new Color (0x52 / 255F, 0x7D / 255F, 0x9F/ 255F, 0/ 255F),
            new Color (0x65 / 255F, 0x82/ 255F, 0x2F/ 255F, 0/ 255F),
            new Color (0x6F/ 255F, 0x61/ 255F, 0x9A/ 255F, 0/ 255F),
            new Color (0x57/ 255F, 0x5B/ 255F, 0x6F/ 255F, 0/ 255F)
        };



        refe = new Vector3(666, 666, 666);
        PositionFrameBefore = refe;

        PotionFilled = false;
        CanStirr = false;

    }

    //Main update
    void Update()
    {
        UpdatingStirringPercentage();
        ChangingMixColorProcess();
            

        /*
        // Start or stop the stirring sound based on stirring activity
        if (StirredBegin && !isStirring)
        {
            Debug.Log("sound played");// Play the stirring sound       
            isStirring = true;
            AudioManager.Instance.PlaySound("Stirring");
        }

        if (isStirring && IsStirred)
        {
            isStirring = false;
            AudioManager.Instance.StopSound("Stirring"); // Stop the stirring sound
            Debug.Log("sound stopped");
        } */
    }


    //main OnTriggerEnter
    private void OnTriggerEnter(Collider other) //Add ingredients to the cauldron list
    {
        if (other.tag == "Ingredient")
        {
            ActualIngredients.Add(other.gameObject);
        }


        //filling Potions
        if (!PotionFilled && other.tag == "Flask" && IsStirred)
        {
            PotionFilled = true;
            MeshRenderer mesh = other.gameObject.GetComponent<MeshRenderer>();
            Material[] materials = mesh.materials;

            if (materials[0].name.Contains("Glass"))
            {
                materials[0] = SwitchingMat;
            }
            else
            {
                materials[1] = SwitchingMat;
            }

            mesh.materials = materials;
            EmptyTheCauldron();
        }

        if (other.tag == "Stick")
        {
            //Debug.Log("sound played");// Play the stirring sound       
            isStirring = true;
            AudioManager.Instance.PlaySound("Stirring");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ingredient"))
        {
            ActualIngredients.Remove(other.gameObject);
        }

        // Stop stirring sound when the stick is removed from the cauldron
        if (other.CompareTag("Stick") && isStirring)
        {
            isStirring = false;
            AudioManager.Instance.StopSound("Stirring");
            //Debug.Log("sound stopped");
        }
    }



        #region SwitchingCauldronColor

        public void ChangingMixColorProcess()
    {
        if (ActualIngredients.Count != 0)
            CanStirr = true;

        if (StirredBegin && !IsCauldronLocked)
        {
            LockingCauldron();
            IsCauldronLocked = true;
        }

        if (Percentage > 0f && Percentage <= 1f)
        {
            rend.material = SwitchingMat;
            TmpColor.a = Mathf.Lerp(0, 1, Percentage);
            TmpColor.r = Mathf.Lerp(0, ColorSelected.r, Percentage);
            TmpColor.g = Mathf.Lerp(0, ColorSelected.g, Percentage);
            TmpColor.b = Mathf.Lerp(0, ColorSelected.b, Percentage);
            SwitchingMat.color = TmpColor;
        }
    }


    public void EmptyTheCauldron()
    {
        rend.material = WaterMat;
    }
    #endregion




    #region Cauldron

    public void LockingCauldron()
    {
        //Debug.Log("Locking the cauldron");
        foreach (GameObject GO in ActualIngredients)
        {
            if (GO.name.Contains("Tail"))
            {
                ColorSelected = ColorList[GO.name[11] - 49];
                break;
            }
        }

        AreIngredientsPutRight = CheckingPotionIngredients();
        //Debug.Log(CheckingPotionIngredients());
        desk.SavingObjectPositionsScript.ResetAllPositions();

    }

    public void UnlockingAndClearCauldron()
    {
        //Debug.Log("Unlocking the cauldron");
        ResetStirringProcess();
        CanStirr = false;
        IsCauldronLocked = false;
        ActualIngredients.Clear();
        DistanceStirred = 0f;
    }

    private bool CheckingPotionIngredients()
    {
        if (ActualIngredients.Count == 0 || ActualIngredients.Count != IngredientsSupposed.Count)
        {
            //Debug.Log("Exit 1");
            return false;
        }

        Dictionary<string, int> occurences = new Dictionary<string, int>();

        foreach (var GO in IngredientsSupposed)
        {
            if (occurences.ContainsKey(GO.name))
                occurences[GO.name]++;

            else
                occurences.Add(GO.name, 1);
        }

        foreach (var GO in ActualIngredients)
        {
            if (!occurences.ContainsKey(GO.name))
            {
                //Debug.Log("Exit 2");
                return false;
            }

            else
                occurences[GO.name]--;
        }

        foreach (var (GO, n) in occurences)
        {
            if (n != 0)
                return false;
        }

        return true;

   
    }


    #endregion

    #region Stirring

    // Update is called once per frame

    private void UpdatingStirringPercentage()
    {
        if (DistanceToStirr > Mathf.Epsilon)
            Percentage = DistanceStirred / DistanceToStirr;
    }


    private void Process(Vector3 CurrentPos)
    {
        DistanceStirred += Vector3.Distance(PositionFrameBefore, CurrentPos);
        PositionFrameBefore = CurrentPos;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Stick" && CanStirr)
        {
            if (PositionFrameBefore != refe)
                Process(other.transform.position);

            else
                PositionFrameBefore = other.transform.position;
        }
    }

    public void ResetStirringProcess()
    {
        //Debug.Log("Stirring reset");
        DistanceStirred = 0f;
    }




    #endregion
}

