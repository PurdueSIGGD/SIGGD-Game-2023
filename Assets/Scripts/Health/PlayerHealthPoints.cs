using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthPoints : HealthPoints
{

    [SerializeField] private Light playerLight;
    [SerializeField] private Light blackoutLight;
    [SerializeField] private LightResource lightResource;
    public bool blackout;
    private bool flickerActive;
    private bool dying;
    private float deadCount = 2f;
    private bool isInvulnerable;
    [SerializeField] public float iFrameDuration;
    private float baseLightIntensity;

    // Start is called before the first frame update
    void Start()
    {
        playerLight.enabled = true;
        blackoutLight.enabled = false;
        blackout = false;
        flickerActive = false;
        baseLightIntensity = playerLight.intensity;
    }

    /// <summary>
    /// Deals the specified amount of damage to the entity owning this health script.
    /// This method enforces the minimum health value of zero.
    /// </summary>
    /// <param name="damage">
    /// The amount of damage to deal to this entity
    /// </param>
    /// <returns>
    /// The actual amount of damage dealt
    /// </returns>
    public override float damageEntity(float damage)
    {
        if (isInvulnerable)
        {
            return 0f;
        }

        if (blackout)
        {
            if (!dying)
            {
                dying = true;
                // Start fading out entire screen
                StartCoroutine(waitForDeath());
            }
            
            return 0f;
        }

        float damageDealt = base.damageEntity(damage);
        setInvulnerable(iFrameDuration);
        Debug.Log("INVULNERABLE CALLED - DAMAGE - " + iFrameDuration);
        return damageDealt;
    }

    /// <summary>
    /// Destroys the entity owning this health script.
    /// </summary>
    public override void kill()
    {
        blackout = true;
        lightResource.blackout = true;

        StartCoroutine(flickerOut());

        /*float baseLightIntensity = playerLight.intensity;
        playerLight.intensity = baseLightIntensity * 0.4f;
        StartCoroutine(wait(0.2f));
        playerLight.intensity = baseLightIntensity * 0.6f;
        StartCoroutine(wait(0.7f));
        playerLight.intensity = baseLightIntensity * 0.2f;
        StartCoroutine(wait(0.2f));
        playerLight.intensity = baseLightIntensity * 0.3f;
        StartCoroutine(wait(0.3f));

        playerLight.enabled = false;
        blackoutLight.enabled = true;*/
        
    }



    /// <summary>
    /// Provides the specified amount of healing to the entity owning this health script.
    /// This method enforces the maximum health value of the entity.
    /// </summary>
    /// <param name="healing">
    /// The amount of healing to provide to this entity
    /// </param>
    /// <returns>
    /// The actual amount of healing provided
    /// </returns>
    public override float healEntity(float healing)
    {
        //Debug.Log("HEALING YOU");
        if (blackout)
        {
            //return 0f;

            blackout = false;
            lightResource.blackout = false;
            StartCoroutine(flickerIn());

            //return 0f;
        }

        return base.healEntity(healing);
    }



    // Update is called once per frame
    void Update()
    {
        if (!flickerActive && currentHealth <= 30)
        {
            StartCoroutine(flicker());
        }

        if (healDEV > 0)
        {
            healEntity(healDEV);
            healDEV = 0;
        }

        if (damageDEV > 0)
        {
            damageEntity(damageDEV);
            damageDEV = 0;
        }
    }


    public void setInvulnerable(float duration)
    {
        Debug.Log("INVULNERABLE CALLED - SET - " + duration);
        StartCoroutine(setInvulnerableCo(duration));
    }

    private IEnumerator setInvulnerableCo(float duration)
    {
        isInvulnerable = true;
        Debug.Log("IS INVULNERABLE - CO - " + duration);
        yield return new WaitForSeconds(duration);
        isInvulnerable = false;
    }


    private IEnumerator waitForDeath()
    {
        var deathTime = 2f;
        var fader = FindObjectOfType<Fader>();
        if (fader == null)
        {
            Debug.LogError("You need to drag the fader prefab into the scene");
        }
        fader.FadeOut(Color.red, deathTime);
        yield return new WaitForSeconds(deathTime);
        SceneManager.LoadScene("DeathScene");
    }

    private IEnumerator flickerOut()
    {
        float baseLightIntensity = playerLight.intensity;
        playerLight.intensity = baseLightIntensity * 0.1f;
        yield return new WaitForSeconds(0.025f);
        playerLight.intensity = baseLightIntensity * 0.4f;
        yield return new WaitForSeconds(0.085f);
        playerLight.intensity = baseLightIntensity * 0.9f;
        yield return new WaitForSeconds(0.025f);
        playerLight.intensity = baseLightIntensity * 0.3f;
        yield return new WaitForSeconds(0.037f);
        playerLight.enabled = false;
        blackoutLight.enabled = true;
    }

    private IEnumerator flickerIn()
    {
        playerLight.enabled = true;
        blackoutLight.enabled = false;
        //float baseLightIntensity = playerLight.intensity;
        playerLight.intensity = baseLightIntensity * 0.1f;
        yield return new WaitForSeconds(0.037f);
        playerLight.intensity = baseLightIntensity * 0.4f;
        yield return new WaitForSeconds(0.025f);
        playerLight.intensity = baseLightIntensity * 0.9f;
        yield return new WaitForSeconds(0.085f);
        playerLight.intensity = baseLightIntensity * 0.3f;
        yield return new WaitForSeconds(0.025f);
        playerLight.intensity = baseLightIntensity;
    }

    public IEnumerator flicker()
    {
        flickerActive = true;
        float baseLightIntensity = playerLight.intensity;
        playerLight.intensity = baseLightIntensity * Random.Range(0.7f, 0.95f);
        yield return new WaitForSeconds(Random.Range(0.025f, 0.15f));
        playerLight.intensity = baseLightIntensity;
        yield return new WaitForSeconds(Random.Range(0.025f, 0.8f));
        flickerActive = false;
    }



}
