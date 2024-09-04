using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class MiniMap : MonoBehaviour
{
    [SerializeField] float sonarCoolDownTime;
    [SerializeField] float iconTurnOnTime;
    [SerializeField] float iconFadeBufferTime;
    [SerializeField] float iconTurnOffTime;
    [SerializeField] Sprite emptyPylonIcon;
    [SerializeField] Sprite emptyArtifactIcon;
    [SerializeField] AudioSource pingSound;
    [SerializeField] GameObject pingWave;
    [SerializeField] ControlledEnemySpawner sonarEnemySpawner;
    [SerializeField] List<enemyType> sonarEnemyList;

    public bool sonarEnabled;
    public bool enemiesEnabled;
    private bool blackout;
    private bool blackoutOnce;
    private bool sonarCoolDown;
    private byte cameraAlpha;
    private int maskingLayer;
    private Camera minimapCamera;

    private ArrayList minimapIcons = new ArrayList();

    private uiBarManager playerUIBars;

    // Start is called before the first frame update
    void Start()
    {
        minimapCamera = gameObject.GetComponent<Camera>();
        cameraAlpha = Convert.ToByte(minimapCamera.backgroundColor.a * 255);
        maskingLayer = LayerMask.NameToLayer("Minimap");

        playerUIBars = FindObjectOfType<uiBarManager>();

        // Add pylons to the minimap icon array
        ChargePylon[] chargePylons = FindObjectsOfType<ChargePylon>();
        foreach (ChargePylon chargePylon in chargePylons)
        {
            GameObject minimapIcon = chargePylon.transform.Find("minimapIcon").gameObject;
            minimapIcons.Add(minimapIcon);
        }

        // Add the final pylon to the minimap icon array
        FinalPylon[] finalPylons = FindObjectsOfType<FinalPylon>();
        foreach(FinalPylon finalPylon in finalPylons)
        {
            GameObject minimapIcon = finalPylon.transform.Find("minimapIcon").gameObject;
            minimapIcons.Add(minimapIcon);
        }

        // Add artifacts to the minimap icon array
        Artifact[] artifacts = FindObjectsOfType<Artifact>();
        foreach (Artifact artifact in artifacts)
        {
            GameObject minimapIcon = artifact.transform.Find("miniMapIcon").gameObject;
            minimapIcons.Add(minimapIcon);
        }

        pingWave.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // If blackout happens, turn on or off the minimap accordingly
        //blackout = FindObjectOfType<uiBarManager>().blackout;
        blackout = playerUIBars.blackout;
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
        if (Input.GetKeyDown(KeyCode.Space) && !sonarCoolDown && sonarEnabled)
        {
            StartCoroutine(sonarEnemyWave());
            StartCoroutine(SonarRecovery());
            StartCoroutine(releaseSonarPing());
            /*pingSound.Play();
            foreach (GameObject minimapIcon in minimapIcons)
            {
                StartCoroutine(IconFading(minimapIcon));
            }*/
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
    IEnumerator IconFading(GameObject minimapIcon)
    {
        Color minimapIconColor = minimapIcon.GetComponent<SpriteRenderer>().color;
        float alpha = minimapIconColor.a;

        // Let the icon turn on
        while (alpha < 1)
        {
            alpha += (1 / iconTurnOnTime) * Time.deltaTime;
            minimapIconColor.a = alpha;
            minimapIcon.GetComponent<SpriteRenderer>().color = minimapIconColor;
            yield return null;
        }

        // Let the icon stay bright for some time
        yield return new WaitForSeconds(iconFadeBufferTime);

        // Let the icon turn off
        while (alpha > 0)
        {
            alpha -= (1 / iconTurnOffTime) * Time.deltaTime;
            minimapIconColor.a = alpha;
            minimapIcon.GetComponent<SpriteRenderer>().color = minimapIconColor;
            yield return null;
        }
    }

    /**
     * Change the minimap icon of pylons to the empty icon
     * when they have been activated or consumed
     */
    public void changePylonIcon(GameObject pylon)
    {
        GameObject icon = pylon.transform.Find("minimapIcon").gameObject;
        icon.GetComponent<SpriteRenderer>().sprite = emptyPylonIcon;
    }

    /**
     * Change the minimap icon of artifacts to the empty icon
     * when they have been activated or consumed
     */
    public void changeArtifactIcon(GameObject artifact)
    {
        GameObject icon = artifact.transform.Find("miniMapIcon").gameObject;
        icon.GetComponent<SpriteRenderer>().sprite = emptyArtifactIcon;
    }



    public IEnumerator sonarEnemyWave()
    {
        if (enemiesEnabled)
        {
            yield return new WaitForSeconds(4f);
            sonarEnemySpawner.spawnEnemyWave(sonarEnemyList);
            yield return new WaitForSeconds(4f);
            sonarEnemySpawner.spawnEnemyWave(sonarEnemyList);
        }
        yield return null;
    }



    public IEnumerator releaseSonarPing()
    {
        pingSound.Play();
        yield return new WaitForSeconds(1f);
        foreach (GameObject minimapIcon in minimapIcons)
        {
            StartCoroutine(IconFading(minimapIcon));
        }

        pingWave.transform.localScale = Vector3.zero;
        pingWave.SetActive(true);
        float waveIncreaseTime = 1f;
        float waveFinalSize = 1f;
        float waveSize = 0f;
        //Debug.Log("Pingwing1: " + pingWave.active);
        while (waveSize < waveFinalSize)
        {
            waveSize += (waveFinalSize / waveIncreaseTime) * Time.deltaTime;
            pingWave.transform.localScale += new Vector3(waveSize, waveSize, waveSize);
            yield return null;
        }
        //Debug.Log("Pingwing2: " + pingWave.active);
        pingWave.SetActive(false);
    }
}
