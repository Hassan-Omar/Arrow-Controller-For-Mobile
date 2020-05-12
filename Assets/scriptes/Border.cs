using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// This Class Created by H.Omar to act as a border "Player lose when we reach to it "
/// </summary>
public class Border : MonoBehaviour
{
    Text testTXT;
    private void Start()
    {
        testTXT = GameObject.Find("testTXT").GetComponent<Text>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            testTXT.text = "You Lose";
        }
    }
}
