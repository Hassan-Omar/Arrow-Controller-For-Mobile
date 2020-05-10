using UnityEngine;
/// <summary>
/// This Created by H.Omar To Control On Player Object  
/// </summary>
public class Player : MonoBehaviour
{
    // to keep first touch position relative to screen coordinates 
    private Vector2 startTouchPos;
    // reference to arrow to activation or deactivation 
    [SerializeField] GameObject arrow;
    // temp value to hold the last distance between player , touch in the frame no n-1 
    public float dist;
    // hold the reduis of effective area around touched point 
    public float raduis= 300; 

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
                    // Record initial touch position.
                    startTouchPos = touch.position;
                     break;

                //Determine if the touch is a moving touch
                case TouchPhase.Moved:
                    arrow.SetActive(true);
                    // apply slow motion effect 
                    Time.timeScale = 0.5f;
                    
                    // rotate the arrow opposite to current touched point 
                    Vector3 diff = Camera.main.ScreenToWorldPoint(touch.position) - transform.position;
                    diff.Normalize();
                    float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                    arrow.transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);

                    // calculate value for and new length to update arrow 
                    float lengthValue = Vector2.Distance(startTouchPos, touch.position);
                    //Debug.Log("/|/|  " + lengthValue);
                    // update arrow's length   
                    Vector3 pointInWorld = Camera.main.ScreenToWorldPoint(touch.position);
                    if (CheckDirection(new Vector2(pointInWorld.x, pointInWorld.y)))
                    { 
                      
                            // decrease Arrow Length 
                            Arrow.Instance.updateLength(0.003f * lengthValue, -1);
                            Debug.Log("Toward");
                       
                    }
                    else
                    {
                            // increase Arrow Length 
                            Arrow.Instance.updateLength(0.003f*lengthValue, 1);
                            Debug.Log("Away");
                       
                    }
                    break;
                    
                case TouchPhase.Ended:
                    // end slow motion effect 
                    Time.timeScale = 1;
                    arrow.SetActive(false);
                    // reset arrow's lengh 
                    Arrow.Instance.resetToStartLenght();
                    arrow.transform.localRotation = Quaternion.identity;
                    // here we will project the player 
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
        float distTemp = Vector2.Distance(pointPos, new Vector3(transform.position.x,transform.position.y));
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
}
