using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RailScript : MonoBehaviour
{
    // Start is called before the first frame update

    public InfoObject.difficulty Difficulty;
    
    public Transform from;
    public Transform to;

    public GameObject ObjectToMove;
    public GameObject EndDetector;

    public float speed;
    private float CurrentDistance;

    private float MaxTimeHold;

    public InfoObject Info;

    public TextMeshProUGUI text;


    void Start()
    {
        text.enabled = false;
        MaxTimeHold = Vector3.Distance(ObjectToMove.transform.position, to.position)/speed - EndDetector.transform.localScale.z;
        ObjectToMove.transform.LookAt(to.position);
    }
    

    void Update()
    {
        if ((CurrentDistance = Vector3.Distance(EndDetector.transform.position, to.position)) > 0.1f)
        {
            ObjectToMove.transform.position += to.position.normalized * (speed * Time.deltaTime);
        }
    }

    public void DestroyObject(float TimeObjectHold, InfoObject.difficulty diff)
    {
        float percentage = TimeObjectHold / MaxTimeHold;
        int score = (int) (percentage * InfoObject.points[(int)diff]);
        Info.Score += score;
        
        Debug.Log($"{percentage * InfoObject.points[(int) diff]} somme ");
        Debug.Log($"the percentage {percentage} has been added");
        
        Info.LunchCo(score);
        Destroy(gameObject);
    }

    //debug

}
