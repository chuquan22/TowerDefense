using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // projectile speed support
    protected Vector2 impulseForce = Vector2.zero;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    virtual protected void Start()
    {
        // get projectile moving
        GetComponent<Rigidbody2D>().AddForce(
            impulseForce, ForceMode2D.Impulse);
        gameObject.GetComponent<Collider2D>().isTrigger = true;
    }

    /// <summary>
    /// Destroys projectile when it leaves the screen
    /// </summary>
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if( other.CompareTag("Monster"))
        {
            Destroy(gameObject);
        }
    }
}
