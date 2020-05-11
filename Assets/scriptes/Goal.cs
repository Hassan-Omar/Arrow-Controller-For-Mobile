using UnityEngine;
using UnityEngine.UI;


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
