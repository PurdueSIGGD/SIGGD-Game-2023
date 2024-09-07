using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class LightOrb : MonoBehaviour
{

    [SerializeField] private int lightContained;
    [SerializeField] private int healthContained;
    [SerializeField] public int regenerationCooldown;
    [SerializeField] private Light pointLight;
    [SerializeField] public AudioSource lightSound;
    [SerializeField] public AudioSource fullSound;
    private bool playerHit = false;
    private bool orbActive = true;
    private float baseLightIntensity;


    private void Start()
    {
        baseLightIntensity = pointLight.intensity;
    }



    private void OnTriggerStay(Collider other)
    {
        GameObject player = other.gameObject;
        Debug.Log(player.tag + " Collided");
        if (orbActive && player.tag.Equals("Player") && !playerHit && player.GetComponentInParent<LightResource>() != null)
        {
            playerHit = true;
            float addedLight = player.GetComponentInParent<LightResource>().addLight(lightContained);
            float healing = player.GetComponent<HealthPoints>().healEntity(healthContained);
            if (addedLight <= 0 && healing <= 0)
            {
                playerHit = false;
            } else
            {
                //Destroy(gameObject);
                playPickupSound(player);
                StartCoroutine(regenerationTimer());
            }
        }
    }

    private IEnumerator regenerationTimer()
    {
        playerHit = false;
        orbActive = false;
        //lightSound.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        pointLight.intensity = baseLightIntensity * 0.25f;
        yield return new WaitForSeconds(regenerationCooldown);
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
        pointLight.intensity = baseLightIntensity;
        orbActive = true;
    }


    private void playPickupSound(GameObject player)
    {
        float lightRatio = (player.GetComponentInParent<LightResource>().currentLight) / (player.GetComponentInParent<LightResource>().maximumLight);
        float healthRatio = (player.GetComponent<HealthPoints>().currentHealth) / (player.GetComponent<HealthPoints>().maximumHealth);
        lightSound.pitch = (lightRatio <= healthRatio) ? (0.2f + (lightRatio * 0.5f)) : (0.2f + (healthRatio * 0.5f));
        lightSound.Play();
        if (lightRatio == 1f && healthRatio == 1f)
        {
            fullSound.Play();
        }
    }

}
