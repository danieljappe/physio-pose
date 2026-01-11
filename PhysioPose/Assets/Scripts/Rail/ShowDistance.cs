using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ShowDistance : MonoBehaviour
{
    public GameObject EndDetector;
    public Transform To;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnDrawGizmos()
    {
    #if UNITY_EDITOR
        if (EndDetector != null && To != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(EndDetector.transform.position, To.position);
        }
        #endif
    }
}
