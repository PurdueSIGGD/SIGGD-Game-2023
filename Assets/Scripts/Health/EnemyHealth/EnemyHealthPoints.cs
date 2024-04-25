using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class EnemyHealthPoints : HealthPoints
{
    // Start is called before the first frame update
    [SerializeField] private GameObject spriteHolder;
    [SerializeField] private GameObject attackBox;
    [SerializeField] private float spriteFadeSpeed;
    [SerializeField] private float spriteFadeTime;
    [SerializeField] private GameObject spritePlane;
    [SerializeField] private Material deathMaterial;
    [SerializeField] private EnemyDeathSound deathSoundPlayer;
    private float startTime;

    public override void kill() {
        deathSoundPlayer.playDeathSound();
        this.gameObject.transform.parent.GetComponent<LungeNav>().enabled = false;
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        attackBox.SetActive(false);
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
        Destroy(this.transform.parent.gameObject);
        yield return null;
    }
}
