using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDownMovement : MonoBehaviour
{
    //To move the position of the object directly in the scene
    public float x;

    public float y;

    public float z;

    //Change the amplitude of the movement
    public float amp;
    // Start is called before the first frame update
    void Start()
    {
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(x, Mathf.Sin(Time.time) * amp + y, z);
    }
}
