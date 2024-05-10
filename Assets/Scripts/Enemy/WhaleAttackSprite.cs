using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleAttackSprite : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    void Start()
    {
        if (cameraTransform == null)
        {
            var mainCam = Camera.main;
            if (mainCam != null)
            {
                cameraTransform = mainCam.transform;
            }
            else
            {
                cameraTransform = FindObjectOfType<Camera>().transform;
            }
        }
    }

    void LateUpdate()
    {
        gameObject.transform.rotation = cameraTransform.rotation;
    }
}
