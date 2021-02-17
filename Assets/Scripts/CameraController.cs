using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [Tooltip("The game object to follow with the camera")]
    public GameObject focus;
    Transform focusTransform;
    Transform selfTransform;

    Bounds bound;

    float viewWidth, viewHeight;

    // Start is called before the first frame update
    void Start()
    {
        selfTransform = transform;
        focusTransform = focus.transform;

        bound = GameObject.Find("Grid/Tilemap").GetComponent<Renderer>().bounds;

        Camera cam = Camera.main;
        viewHeight = cam.orthographicSize;
        viewWidth = viewHeight * cam.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        selfTransform.position = new Vector3(Mathf.Clamp(focusTransform.position.x, bound.center.x - bound.extents.x + viewWidth, bound.center.x + bound.extents.x - viewWidth), 
                                            Mathf.Clamp(focusTransform.position.y, bound.center.y - bound.extents.y + viewHeight, bound.center.y + bound.extents.y - viewHeight), 
                                            selfTransform.position.z);
    }
}
