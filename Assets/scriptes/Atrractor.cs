using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atrractor : MonoBehaviour
{
    private float attractionForce = 50f;
    private GameObject player; 
    [SerializeField]private bool isAttractionEnabled = false; 


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
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
    /// <summary>
    /// function to simulate attraction on player 
    /// </summary>
    private void attractPlayer()
    {
        var rb = player.GetComponent<Rigidbody2D>(); 
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        Vector3 forceDirection = transform.position - player.transform.position;
        rb.AddForce(attractionForce * forceDirection.normalized);
        Debug.Log("Called With "+ attractionForce);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.isAttractionEnabled)
        {
            attractPlayer();
        }
    }
}
