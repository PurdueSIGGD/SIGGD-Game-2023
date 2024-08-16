using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class MiniMap : MonoBehaviour
{
    const float sonarCoolDownTime = 3f;
    const float iconTurnOnTime = 0.7f;
    const float iconTurnOffTime = 2f;

    private bool blackout;
    private bool blackoutOnce;
    private bool sonarCoolDown;
    private byte cameraAlpha;
    private int maskingLayer;
    private Camera minimapCamera;

    private ArrayList minimapIcons = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        minimapCamera = gameObject.GetComponent<Camera>();
        cameraAlpha = Convert.ToByte(minimapCamera.backgroundColor.a * 255);
        maskingLayer = LayerMask.NameToLayer("Minimap");
        ChargePylon[] chargePylons = FindObjectsOfType<ChargePylon>();
        foreach (ChargePylon chargePylon in chargePylons)
        {
            GameObject minimapIcon = chargePylon.transform.Find("minimapIcon").gameObject;
            minimapIcons.Add(minimapIcon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If blackout happens, turn on or off the minimap accordingly
        blackout = FindObjectOfType<uiBarManager>().blackout;
        if (blackout && !blackoutOnce)
        {
            blackoutOnce = true;
            TurnCameraOff();
        } else if (!blackout && blackoutOnce)
        {
            blackoutOnce = false;
            TurnCameraOn();
        }

        // Pressing space for radar
        if (Input.GetKeyDown(KeyCode.Space) && !sonarCoolDown)
        {
            StartCoroutine(SonarRecovery());
            foreach (GameObject minimapIcon in minimapIcons)
            {
                Debug.Log(minimapIcon);
                IconFading(minimapIcon);
                Debug.Log(2);
            }
        }
    }

    // Turning the camera off
    [ContextMenu("OFF")]
    void TurnCameraOff()
    {
        // Change background color to transparent
        Color32 CameraBg = minimapCamera.backgroundColor;
        Color32 cameraBgOff = new Color32(CameraBg.r, CameraBg.g, CameraBg.b, 0);
        minimapCamera.backgroundColor = cameraBgOff;

        // Change masking layer to none
        minimapCamera.cullingMask &= ~(1 << maskingLayer);
    }

    // Turning the camera on
    [ContextMenu("ON")]
    void TurnCameraOn()
    {
        Color32 CameraBg = gameObject.GetComponent<Camera>().backgroundColor;
        Color32 cameraBgOn = new Color32(CameraBg.r, CameraBg.g, CameraBg.b, cameraAlpha);
        gameObject.GetComponent<Camera>().backgroundColor = cameraBgOn;

        // Change masking layer to none
        minimapCamera.cullingMask |= 1 << maskingLayer;
    }

    // Sonar recovery timer after sonar was used
    public IEnumerator SonarRecovery()
    {
        sonarCoolDown = true;
        yield return new WaitForSeconds(sonarCoolDownTime);
        sonarCoolDown = false;
    }

    // Minimap icons fading in and out after the sonar ping
    void IconFading(GameObject minimapIcon)
    {
        //Debug.Log(minimapIcon.GetComponent<SpriteRenderer>());
        Color minimapIconColor = minimapIcon.GetComponent<SpriteRenderer>().color;
        float alpha = minimapIconColor.a;

        // Let the icon turn on
        while (alpha < 1 )
        {
            alpha += (1 / iconTurnOnTime) * Time.deltaTime;
            minimapIconColor = new Color(minimapIconColor.r, minimapIconColor.g, minimapIconColor.b, alpha);
            Debug.Log(minimapIconColor.a);
        }

        Debug.Log(minimapIconColor.a);

        // Let the icon turn off
        while (alpha > 0)
        {
            alpha -= (1 / iconTurnOffTime) * Time.deltaTime;
            minimapIconColor = new Color(minimapIconColor.r, minimapIconColor.g, minimapIconColor.b, alpha);
            Debug.Log(minimapIconColor.a);
        }

        Debug.Log(minimapIconColor.a);
    }
}
