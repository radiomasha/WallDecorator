using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedVertical : MonoBehaviour
{
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main; 
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 direction = mainCamera.transform.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
        transform.Rotate(0, 180, 0);
    }
}
