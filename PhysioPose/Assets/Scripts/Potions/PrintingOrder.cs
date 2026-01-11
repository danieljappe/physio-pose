using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PrintingOrder : MonoBehaviour
{
    public List<Canvas> Canvas;
    
    public RandomizerRequest RandRequ;

    public float ScaleFactor;

    public Canvas CurrentCanva;

    public PotionController cauldron;

    public FinalDesk Desk;

    public Transform PositionToSpawn;

    [SerializeField]
    private string newOrderSound = "new order";

    // Start is called before the first frame update
    void Start()
    {   
        //PositionToSpawn.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentCanva != null)
        {
            CurrentCanva.transform.localScale = new Vector3(ScaleFactor, ScaleFactor, ScaleFactor);
            CurrentCanva.transform.position = PositionToSpawn.position;
            CurrentCanva.transform.rotation = PositionToSpawn.rotation;
        }
    }
    
    
    public void DisplayOrder(List<GameObject> IngredientsList, GameObject flask)
    {
        Canvas canva = Canvas[IngredientsList.Count - 1];
        //Instantiate(canva, RandRequ.PositionToSpawn);
        Image [] images = canva.GetComponentsInChildren<Image>();
        //Debug.Log($" image length {images.Length}");
        
        //Exemple =  Resources.Load<Sprite>($"{IngredientsList[0].name}.png");

        for (int i = 0; i < IngredientsList.Count; i++)
        {
            //Debug.Log($"{IngredientsList[i].name}");
            Object ingr = Resources.Load(IngredientsList[i].name);
            images[i*2].sprite = TextureToSprite((Texture2D)ingr);
        }

        Object ingr2 = Resources.Load(flask.name);
        images[images.Length - 1].sprite = TextureToSprite((Texture2D) ingr2);

        CurrentCanva = Instantiate(canva, PositionToSpawn.position, Quaternion.identity);
        
    }
    
    private Sprite TextureToSprite(Texture2D texture)
    {
        // Create a new Sprite from the texture

        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        return sprite;
    }

    public void GenerateOrderPrintItAndSentItToTheCauldron()
    {
        // Find Cauldron in the scene by tag
        GameObject cauldronGO = GameObject.FindWithTag("CauldronIngredients");
        if (cauldronGO != null) {
            cauldron = cauldronGO.GetComponent<PotionController>();
            if (cauldron == null) {
                Debug.Log("Cauldron component not found on CauldronGO");
            }
        } else {
            Debug.Log("Cauldron gameObject is null");
        }

        // Find the desk by tag
        GameObject deskGO = GameObject.FindWithTag("OrderDesk");
        if (deskGO != null) {
            Desk = deskGO.GetComponent<FinalDesk>();
            if (Desk == null) {
                Debug.Log("CustomersDesk component not found on DeskGO");
            }
        } else {
            Debug.Log("Desk gameObject is null");
        }

        List<GameObject> list;
        GameObject flask;

        (list, flask) = RandRequ.GetOrder();
        Desk.SetFlask(flask);
        
        //calculate money for this order
        int money = 0;
        list.ForEach( x => money += Desk.SavingObjectPositionsScript.MoneyDic[x.name]);
        money += Desk.SavingObjectPositionsScript.MoneyDic[flask.name];
        Desk.MoneySupposedForPotion = money; 

        cauldron.IngredientsSupposed = list.ToList();


        DisplayOrder(cauldron.IngredientsSupposed, Desk.FlaskNeeded);



        // Play the order sound effect
        AudioManager.Instance.PlaySound(newOrderSound);
    }
}
    
 
