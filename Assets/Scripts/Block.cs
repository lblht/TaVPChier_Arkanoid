using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Block : MonoBehaviour
{
    public delegate void OnBlockDestroyed();
    public static event OnBlockDestroyed onBlockDestroyed;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            if(onBlockDestroyed != null)
                onBlockDestroyed();
                
            Destroy(gameObject); 
        }
    }
}
