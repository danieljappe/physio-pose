using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Stirring : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector3 PositionFrameBefore;
    public float DistanceToStirr;
    private float DistanceStirred;

    public float Percentage;

    private Vector3 refe;

    public bool IsStirred => Percentage >= 1;
    public bool StirredBegin => Percentage > 0;


    void Start()
    {
        refe = new Vector3(666, 666, 666);
        PositionFrameBefore = refe;
    }

    // Update is called once per frame
    void Update()
    {
        if (DistanceToStirr > Mathf.Epsilon)
            Percentage = DistanceStirred / DistanceToStirr;
    }

    private void Process(Vector3 CurrentPos)
    {
        DistanceStirred += Vector3.Distance(PositionFrameBefore, CurrentPos);
        //Debug.Log(Vector3.Distance(PositionFrameBefore, CurrentPos));
        PositionFrameBefore = CurrentPos;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Stick")
        {
            //Debug.Log("KLJA>GIUGFJUAGFYSDFLKuh");
            if (PositionFrameBefore != refe)
                Process(other.transform.position);

            else
                PositionFrameBefore = other.transform.position;
        }
    }

    public void ResetStirringProcess()
    {
        Debug.Log("Stirring reset");
        DistanceStirred = 0f;
    }
}
