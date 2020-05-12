using UnityEngine;
/// <summary>
/// This Created by H.Omar To Control On Player Object  
/// </summary>
public class Player : MonoBehaviour
{
    [SerializeField] float sensetivity = 0.005f;
    // to keep first touch position relative to screen coordinates 
    private Vector2 startTouchPos;
    // reference to arrow to activation or deactivation 
    [SerializeField] GameObject arrow;
    // temp value to hold the last distance between player , touch in the frame no n-1 
    public float dist;
    // hold the rigid body of the player 
    private Rigidbody2D rb;


    private void Start()
    {
         rb = transform.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        // prevent calling if we have no touching 
        if(Input.touchCount>0)
        {
            Touch touch = Input.GetTouch(0);
            // Handle finger movements based on TouchPhase
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Time.timeScale = 0.2f;
                    rb.gravityScale = 0.25f;
                    // Record initial touch position.
                    startTouchPos = touch.position;
                    break;

                //Determine if the touch is a moving touch
                case TouchPhase.Moved:
                    arrow.SetActive(true);
                    // apply slow motion effect 

                    // rotate the arrow opposite to current touched point 
                    Vector3 diff = Camera.main.ScreenToWorldPoint(touch.position) - transform.position;
                    diff.Normalize();
                    float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                    arrow.transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);

                    // calculate value for and new length to update arrow 
                    float lengthValue = Vector2.Distance(startTouchPos, touch.position);

                    // map touched point to world space   
                    Vector3 pointInWorld = Camera.main.ScreenToWorldPoint(touch.position);

                    if (CheckDirection(new Vector2(pointInWorld.x, pointInWorld.y)))
                    {
                        if (Arrow.Instance.getLength() > Arrow.Instance.minLength)
                        {
                            // decrease Arrow Length 
                            Arrow.Instance.updateLength(sensetivity * Time.deltaTime * lengthValue, -1);
                        }
                    }
                    else
                    {
                        if (Arrow.Instance.getLength()<Arrow.Instance.maxLength)
                        {
                            // increase Arrow Length 
                            Arrow.Instance.updateLength(sensetivity * Time.deltaTime * lengthValue, 1);
                        }
                       
                    }
                    break;
                    
                case TouchPhase.Ended:
                    // end slow motion effect 
                    Time.timeScale = 1;
                    rb.gravityScale = 1;
                    arrow.SetActive(false);
                    // here we will project the player 
                    projecteThePlayer(arrow.transform.up, 400);
                   
                    // reset arrow's lengh 
                    Arrow.Instance.resetToStartLenght();
                    arrow.transform.localRotation = Quaternion.identity;
                    
                    break;
            }
        }
    }


    /// <summary>
    /// function to check if the user moving towards the cube or not 
    /// </summary>
    /// <param name="pointPos">position touch 0 </param>
    /// <returns> true if we move touched point towards to the player </returns>
    private bool CheckDirection(Vector2 pointPos)
    {
        float distTemp = Vector2.Distance(pointPos, new Vector2(transform.position.x, transform.position.y));
        if (distTemp < dist)
        {
            dist = distTemp;
            return true;
        }
        else
        { // rigorous checking
            dist = distTemp;
            return false;
        }
    }
    

    /// <summary>
    /// function to projecte the player in direction of the arrow 
    /// </summary>
    /// <param name="direction">direction of the arrow </param>
    /// <param name="force">value of the force to move the cube </param>
    private void projecteThePlayer(Vector2 direction,int force)
    {
        rb.AddForce(direction * force * Arrow.Instance.getLength());
        rb.gravityScale = 1; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
