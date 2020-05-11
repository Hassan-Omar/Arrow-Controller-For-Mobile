using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atrractor : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.gameObject.tag == "Player")
       {
            // here i should check color first 
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
        }
    }
}
