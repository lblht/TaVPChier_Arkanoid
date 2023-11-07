using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Block : MonoBehaviour
{
    public delegate void OnBlockDestroyed(int score);
    public static event OnBlockDestroyed onBlockDestroyed;

    public delegate void OnBlockEnabled();
    public static event OnBlockEnabled onBlockEnabled;
    [SerializeField] int blockScore;
    [SerializeField] int hitsToTake;
    [SerializeField] GameObject destroyEffect;

    void OnEnable()
    {
        if(onBlockEnabled != null)
                onBlockEnabled();
    }

    void OnDisable()
    {
        if(onBlockDestroyed != null)
                onBlockDestroyed(blockScore);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            BlockHit();
        }
    }

    public void BlockHit()
    {
        hitsToTake--;

        if(hitsToTake <= 0)
        {
            SpawnDestroyEffect();
            gameObject.SetActive(false); 
        }
        else
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }

    void SpawnDestroyEffect()
    {
        GameObject effect = Instantiate(destroyEffect, transform.position, Quaternion.identity);
        var psMain = effect.GetComponent<ParticleSystem>().main;
        psMain.startColor = GetComponent<SpriteRenderer>().color;
        Destroy(effect, 5);
    }
}
