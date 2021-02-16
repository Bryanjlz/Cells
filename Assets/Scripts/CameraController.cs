using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [Tooltip("The game object to follow with the camera")]
    public GameObject focus;
    Transform focusTransform;
    Transform selfTransform;

    // Start is called before the first frame update
    void Start()
    {
        selfTransform = transform;
        focusTransform = focus.transform;
    }

    // Update is called once per frame
    void Update()
    {
        selfTransform.position = new Vector3(focusTransform.position.x, focusTransform.position.y, selfTransform.position.z);
    }
}
