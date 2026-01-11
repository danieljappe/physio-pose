using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scorer : MonoBehaviour
{
    public TMP_Text text;

    private OrbCollecter _collecter;
    // Start is called before the first frame update
    void Start()
    {
       _collecter = GetComponent<OrbCollecter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        text.SetText($"Score: {_collecter.getScore()}");
    }
}
