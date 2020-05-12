using UnityEngine;
/// <summary>
/// this class to handle attraction effect 
/// </summary>
public class Atrractor : MonoBehaviour
{
    // I don't need this variable as I can access it from sprite directly 
    // But I maybe need to do that in the future based on 2d designer 
    // [SerializeField]private Color color;
    private float attractionForce = 50f;
    private GameObject player; 
    [SerializeField]private bool isAttractionEnabled = false; 


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.gameObject.tag == "Player" && isSameColor())
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
        if(this.isAttractionEnabled && isSameColor())
        {
            attractPlayer();
        }
    }

    /// <summary>
    /// function to check if the player has same color as this aobject 
    /// </summary>
    /// <returns> true if colors are samiliar false if not </returns>
    private bool isSameColor()
    {
        Color palayerColor = player.GetComponent<SpriteRenderer>().color;
        if (palayerColor == GetComponent<SpriteRenderer>().color)
            return true;
        else
            return false; 
    }
}
