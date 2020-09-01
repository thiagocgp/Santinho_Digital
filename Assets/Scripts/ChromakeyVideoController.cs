using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChromakeyVideoController : MonoBehaviour
{
    Camera chromakeyCamera;

    void Start()
    {
        chromakeyCamera = FindObjectOfType<Camera>();
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.Cross(Vector3.up, Vector3.Cross(Vector3.up, chromakeyCamera.transform.forward)), Vector3.up);
    }
}
