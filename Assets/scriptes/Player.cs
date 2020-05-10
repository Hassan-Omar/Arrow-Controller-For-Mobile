using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This Created by H.Omar To Control On Player Object  
/// </summary>
public class Player : MonoBehaviour
{
    // to keep first touch position relative to screen coordinates 
    private Vector2 startTouchPos;
    // length of arrow before starting controlling  
    private float startArrowLength;
    // vars to contorol on Senstivity 
    public float rotationSenstivity = 0.01f;
    public float changingLengthSenstivity = 0.01f;
    // reference to arrow to activation or deactivation 
    [SerializeField] GameObject arrow; 
  
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
                    // calculate values for rotation and new length to update arrow 
                    float rotationValue = (touch.position.x - (Screen.width / 2) )* rotationSenstivity;
                    float lengthValue = Vector2.Distance(startTouchPos, touch.position);

                    // rotate the arrow opposite to current touched point 
                    Vector3 diff = Camera.main.ScreenToWorldPoint(touch.position) - transform.position;
                    diff.Normalize();
                    float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                    arrow.transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);


                    /*// update arrow's length   
                    if(lengthValue> startArrowLength)
                    {
                        // increase Arrow Length 
                        Arrow.Instance.updateLength(lengthValue * changingLengthSenstivity, 1);
                    }
                    else
                    {
                        // decrease Arrow Length 
                        Arrow.Instance.updateLength(lengthValue * changingLengthSenstivity, -1);
                    }*/
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
    /// function to check if the user finger up or down relative to the player 
    /// </summary>
    /// <param name="point">touched screen point </param>
    /// <returns> 1 if up , -1 if down </returns>
    private int getDirection(Vector2 point)
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(point);
        if (p.y > transform.position.y)
        {
            return 1;
        }
        else
            return -1;
    }
}
