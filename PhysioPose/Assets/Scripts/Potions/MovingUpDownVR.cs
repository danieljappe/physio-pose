using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MovingUpDownVR : MonoBehaviour
{
    private Vector3 vector = new (0, 0.01f, 0);

    // Start is called before the first frame update
    void Start()
    {
        //this.transform.position = new Vector3(-0.05400000140070915f, 2.184000015258789f, -4.076000213623047f);
        //this.transform.rotation = new quaternion(0, 0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            this.transform.position += vector;
        
        if(Input.GetKey(KeyCode.DownArrow))
            this.transform.position -= vector;
    }
}
