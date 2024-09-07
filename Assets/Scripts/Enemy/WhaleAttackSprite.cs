using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleAttackSprite : MonoBehaviour
{
    [SerializeField] private Transform attackSpriteTransform;
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
                cameraTransform = FindObjectOfType<CameraFollow>().gameObject.transform.GetChild(0).transform;
            }
        }
    }

    void LateUpdate()
    {
        //attackSpriteTransform.rotation = cameraTransform.rotation;
        gameObject.transform.rotation = cameraTransform.rotation;
        //Vector3 direction = (playerTransform.position - cameraTransform.position);
        //direction.Normalize();
        //attackSpriteTransform.LookAt(attackSpriteTransform.position + direction);
    }
}
