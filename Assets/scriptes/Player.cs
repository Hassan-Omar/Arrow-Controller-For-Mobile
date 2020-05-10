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
                    // rotate the player with the moving 
                    float rotationValue = -(touch.position.x - (Screen.width / 2) )* 0.01f; 
                    transform.Rotate(new Vector3(0, 0, rotationValue));

                    // rotate the arrow 
                    break;

                case TouchPhase.Ended:
                    // end slow motion effect 
                    Time.timeScale = 1;
                    arrow.SetActive(false);
                    // here we will project the player 
                    break;
            }
        }
    }
}
