using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOrb : MonoBehaviour
{

    [SerializeField] private int lightContained;
    [SerializeField] private int healthContained;
    private bool playerHit = false;



    private void OnTriggerStay(Collider other)
    {
        GameObject player = other.gameObject;
        Debug.Log(player.tag + " Collided");
        if (player.tag.Equals("Player") && !playerHit && player.GetComponentInParent<LightResource>() != null)
        {
            playerHit = true;
            float addedLight = player.GetComponentInParent<LightResource>().addLight(lightContained);
            float healing = player.GetComponent<HealthPoints>().healEntity(healthContained);
            if (addedLight <= 0 && healing <= 0)
            {
                playerHit = false;
            } else
            {
                Destroy(gameObject);
            }
        }
    }

}
