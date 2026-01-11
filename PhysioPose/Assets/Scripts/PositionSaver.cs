using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script just save the position of the ingredient in case we have to place it again to it's original position
public class PositionSaver : MonoBehaviour
{
    private Transform _objectTransform;
    private Vector3 _originalPosition;
    // Start is called before the first frame update
    void Start()
    {
        _objectTransform = GetComponent<Transform>();
        _originalPosition = _objectTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void resetPos()
    {
        _objectTransform.position = _originalPosition;
    }
}
