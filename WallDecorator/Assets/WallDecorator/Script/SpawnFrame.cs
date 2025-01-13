using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Hands;
using UnityEngine.XR.Hands.Gestures;
using UnityEngine.XR.Management;
using Oculus.Interaction.Input;

public class SpawnFrame : MonoBehaviour
{
    [SerializeField] private GameObject _framePrefab;
    [SerializeField] private float _handDistanceThreshold = 0.1f;
    
    private XRHandSubsystem _subsystem;
    private XRHand leftHand;
    private XRHand rightHand;
    private bool _isLeftPinching = false;
    private bool _isRightPinching = false;
    // Start is called before the first frame update
    void Start()
    {
         _subsystem= XRGeneralSettings.Instance.Manager.activeLoader.GetLoadedSubsystem<XRHandSubsystem>();
        leftHand = _subsystem.leftHand;
        rightHand = _subsystem.rightHand;
    }

    // Update is called once per frame
    void Update()
    {
        CheckGestures();
    }

    private void CheckGestures()
    {
        _isLeftPinching = CheckPinch(rightHand);
        _isRightPinching = CheckPinch(leftHand);
    }

    private bool CheckPinch(XRHand hand)
    {
        return false;
    }
}
