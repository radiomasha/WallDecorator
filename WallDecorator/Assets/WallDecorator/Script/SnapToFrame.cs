using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class SnapToFrame : MonoBehaviour
{
    [SerializeField] private float _distance = 0.03f;
    private Vector3 hitPoint;
    private Vector3 hitNormal;
    private HandGrabInteractable _handGrabInteractable;
    private ImageFrame _imageFrame;
    // Start is called before the first frame update
    void Start()
    {
        _handGrabInteractable = GetComponentInChildren<HandGrabInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_handGrabInteractable == null)
        {
           // UIDebugger.Log("HandGrabInteractable is null");
        }

        if (_handGrabInteractable.State == InteractableState.Select)
        {
            CheckTheSurfaceToSnap();
        }

        if (CheckTheSurfaceToSnap())
        {
            SnapToSurface(hitNormal);
        }
    }
    
    private bool CheckTheSurfaceToSnap()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 0.5f))
        {
            //var snapInteractable = hit.collider.TryGetComponent<SnapInteractable>(out _);
            // UIDebugger.Log("collider"+hit.collider.gameObject.name);
            _imageFrame = hit.collider.GetComponent<ImageFrame>();
            if (_imageFrame != null)
            {
                //UIDebugger.Log("Snap interactable is " + hit.collider.name);
                hitPoint = hit.point;
                hitNormal = hit.normal;
            }
           
            if (hit.distance < _distance)
            {
                    //UIDebugger.Log("it's close");
                return true;

            }
                
        }

        return false;
    }

    private void SnapToSurface(Vector3 hitNormal)
    {
        
        //UIDebugger.Log("SnapToSurface");
        transform.position = new Vector3(_imageFrame.transform.position.x, _imageFrame.transform.position.y, hitPoint.z-0.0075f);
        
        transform.rotation = Quaternion.LookRotation(-hitNormal);
       
    }
}
