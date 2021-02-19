using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cinemachine;
    [SerializeField] CinemachineConfiner cinemachineConfiner;

    void Start() {
        cinemachine.Follow = GameObject.Find("Player").transform;

        GameObject confiner = GameObject.Find("Confiner");
        if (confiner != null) {
            Collider2D collider = confiner.GetComponent<Collider2D>();
            if (collider != null) {
                cinemachineConfiner.m_BoundingShape2D = confiner.GetComponent<Collider2D>();
            }
        }

    }
}
