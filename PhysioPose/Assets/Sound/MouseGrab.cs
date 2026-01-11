using UnityEngine;
using FMODUnity;

public class MouseGrab : MonoBehaviour
{
    private Camera mainCamera;
    private GameObject grabbedObject;
    private Vector3 offset;
    private float zCoord;
    public string Squishy = "event:/path/to/Squishy";
    public string Glass = "event:/path/to/Glass";

    [SerializeField] private LayerMask grabbableLayer;
    [SerializeField] private EventReference grabSoundEvent;
    [SerializeField] private EventReference dropSoundEvent;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryGrabObject();
        }

        if (grabbedObject != null)
        {
            MoveObject();
        }

        if (Input.GetMouseButtonUp(0))
        {
            ReleaseObject();
        }
    }

    void TryGrabObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, grabbableLayer))
        {
            if (hit.collider != null)
            {
                grabbedObject = hit.collider.gameObject;
                zCoord = mainCamera.WorldToScreenPoint(grabbedObject.transform.position).z;
                offset = grabbedObject.transform.position - GetMouseWorldPos();
                RuntimeManager.PlayOneShot(grabSoundEvent, transform.position);
            }
        }
        if (grabbedObject != true)
        {
            RuntimeManager.PlayOneShot(Squishy, transform.position);
            grabbedObject = null;
        }
    }

    void MoveObject()
    {
        grabbedObject.transform.position = GetMouseWorldPos() + offset;
    }

    void ReleaseObject()
    {
        
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;

        return mainCamera.ScreenToWorldPoint(mousePoint);
    }
}