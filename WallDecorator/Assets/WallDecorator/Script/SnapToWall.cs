using System;
using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class SnapToWall : MonoBehaviour
{
    [SerializeField] private float _distance = 0.3f;
    [SerializeField] private MRUK _MRUK;
    [SerializeField] private Material _surfaceMaterial;
    private Vector3 hitPoint;
    private Vector3 hitNormal;
    private HandGrabInteractable _handGrabInteractable;

    private bool scenehasBeenLoaded;
    private MRUKRoom _currentRoom;

    private List<GameObject> _anchorsCreated = new();
    private bool SceneAndRoomInfoAvailable => _currentRoom != null && scenehasBeenLoaded;

    private void OnEnable()
    {
        _MRUK.RoomCreatedEvent.AddListener(BindRoomInfo);
    }

    private void OnDisable()
    {
        _MRUK.RoomCreatedEvent.RemoveListener(BindRoomInfo);
    }

    public void EnableScene()
    {
        scenehasBeenLoaded = true;
        UIDebugger.Log("EnableScene");
    }

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
            UIDebugger.Log("HandGrabInteractable is null");
        }

        if (_handGrabInteractable.State == InteractableState.Select)
        {
            //Ray ray = new Ray(transform.position, transform.forward);
            // Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
            //if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            // {
            CheckTheSurfaceToSnap();
            // }
            //else
            // {
            //     UIDebugger.Log("nothing");
            //   }

            //CheckTheSurfaceToSnap();
        }

        if (CheckTheSurfaceToSnap())
        {
            SnapToSurface(hitPoint);
        }
    }

    private void BindRoomInfo(MRUKRoom room)
    {
        _currentRoom = room;
    }

    private bool CheckTheSurfaceToSnap()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            // UIDebugger.Log("collider"+hit.collider.gameObject.name);
            if (hit.collider.gameObject.name == "WALL_FACE_EffectMesh")
            {
                hitPoint = hit.point;
                hitNormal = hit.normal;
                hit.collider.gameObject.GetComponent<Renderer>().material = _surfaceMaterial;
                //UIDebugger.Log(hitPoint.ToString() + hitNormal.ToString());
                //UIDebugger.Log("collider found" + hit.collider.gameObject.name);
                if (hit.distance < _distance)
                {
                    //UIDebugger.Log("it's close");
                    return true;

                }

            }
        }

        return false;
    }

    private void SnapToSurface(Vector3 hitPoint)
    {
        //UIDebugger.Log("SnapToSurface");
        transform.position = hitPoint;
        float angle = Vector3.Angle(transform.up, Vector3.up);
        if (angle < 45)
        {
            transform.rotation = Quaternion.LookRotation(-hitNormal);
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(-hitNormal);
            transform.Rotate(0, 0, 90); 
        }
    }
    
}
