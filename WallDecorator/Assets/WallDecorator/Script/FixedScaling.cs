using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class FixedScaling : MonoBehaviour
{
    [SerializeField] private TMP_Text _sizeText;
    private Vector3[] _allowedSizes = new Vector3[]
    {
        new Vector3(0.148f, 0.21f, 0.01f), //A5
        new Vector3(0.21f, 0.297f, 0.01f), // A4
        new Vector3(0.297f, 0.42f, 0.01f), // A3
        new Vector3(0.42f, 0.594f, 0.01f), // A2
        new Vector3(0.594f, 0.841f, 0.01f) // A1
    };
    
    private string[] _sizeLabels = new string[]
    {
        "A5",
        "A4",
        "A3",
        "A2",
        "A1"
    };
    private Transform _objectTransform;
    private AudioSource _audioSource; 
    // Start is called before the first frame update
    void Start()
    {
        _objectTransform = transform;
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentScale = _objectTransform.localScale;
        Vector3 closestSize = GetClosestSize(currentScale);
        _objectTransform.localScale = closestSize;
        
    }

    private Vector3 GetClosestSize(Vector3 currentScale)
    {
        Vector3 closest = _allowedSizes[0];
        float minDistance = Vector3.Distance(currentScale, closest);

        foreach (var size in _allowedSizes)
        {
            float distance = Vector3.Distance(currentScale, size);
            if (distance < minDistance)
            {
                closest = size;
                minDistance = distance;
            }
        }
        UpdateText(closest);
        PlaySound();
        return closest;
    }

    private void UpdateText(Vector3 closestSize)
    {
        int index = System.Array.IndexOf(_allowedSizes, closestSize);
        if (index >= 0 && index < _sizeLabels.Length)
        {
            _sizeText.text = _sizeLabels[index];
        }
    }
    void PlaySound()
    {
        if (_audioSource != null)
        {
            _audioSource.Play();
        }
    }
}
