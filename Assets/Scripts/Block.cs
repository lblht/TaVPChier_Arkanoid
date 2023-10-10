using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Block : MonoBehaviour
{
    public delegate void OnBlockDestroyed();
    public static event OnBlockDestroyed onBlockDestroyed;

    public delegate void OnBlockEnabled();
    public static event OnBlockEnabled onBlockEnabled;

    void OnEnable()
    {
        if(onBlockEnabled != null)
                onBlockEnabled();
    }

    void OnDisable()
    {
        if(onBlockDestroyed != null)
                onBlockDestroyed();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            gameObject.SetActive(false);
        }
    }
}
