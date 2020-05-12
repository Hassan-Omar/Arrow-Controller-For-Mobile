using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This Class Created by H.Omar to act as a goal object 
/// </summary>
public class Goal : MonoBehaviour
{
    Text testTXT;
    private void Start()
    {
        testTXT = GameObject.Find("testTXT").GetComponent<Text>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            testTXT.text = "Level is Finished"; 
        }
    }
}
