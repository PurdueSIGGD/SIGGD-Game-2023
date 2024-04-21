using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildJellyHealthPoints : HealthPoints
{
    [SerializeField] private GameObject spriteHolder;
    [SerializeField] private float spriteFadeSpeed;
    [SerializeField] private float spriteFadeTime;
    [SerializeField] private GameObject spritePlane;
    [SerializeField] private Material deathMaterial;
    public bool inDeath = false;
    private float startTime;

    public override void kill() {
        inDeath = true;
        this.gameObject.GetComponent<MobNav>().enabled = false;
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        StartCoroutine(fadeSprite());
    }

    IEnumerator fadeSprite() {
        startTime = Time.time;
        spriteHolder.GetComponent<DirectionalSprite>().calculateOnce = true;
        spritePlane.GetComponent<MeshRenderer>().material = deathMaterial;
        while (startTime + spriteFadeTime > Time.time) {
            Vector3 tempPos = spriteHolder.transform.localPosition;
            tempPos.y += spriteFadeSpeed * Time.deltaTime;
            spriteHolder.transform.localPosition = tempPos;
            yield return new WaitForEndOfFrame();
        }
        Destroy(this.gameObject);
        yield return null;
    }
        
}
