using UnityEngine;
/// <summary>
/// This Class Created by H.Omar to Handle Updating lenght of the dashing arrow 
/// </summary>
public class Arrow : MonoBehaviour
{
    public float maxLength =0.216f;
    public float minLength = 0.0757f;
    [SerializeField] GameObject[] arrowPoints;
    
    // static object for external access only 
    public static Arrow Instance;
    private void Awake()
    {
        if(Instance==null)
        {
            this.maxLength = 0.216f;
            this.minLength = 0.0757f;
            Instance = this;
        }
    }

    /// <summary>
    /// this function to update the arrow length with 
    /// </summary>
    /// <param name="value"> the value of increment or decrement of length</param>
    /// <param name="status"> this value to tell update increament or decreament </param>
    public void updateLength(float value,int direction)
    { 
        // Increament Operation 
        // first point of arrow is fixed 
        // I should Move last point and second will be in the middle 
        arrowPoints[2].transform.localPosition += new Vector3(0, value, 0)* direction;
        arrowPoints[1].transform.localPosition += 0.5f * new Vector3(0, value, 0)* direction;
    }
    /// <summary>
    /// function to reset the arrow lenght 
    /// </summary>
    public void resetToStartLenght()
    {
        arrowPoints[0].transform.localPosition = new Vector3(0, 0.09f, 0);
        arrowPoints[1].transform.localPosition = new Vector3(0, 0.15f, 0);
        arrowPoints[2].transform.localPosition = new Vector3(0, 0.21f, 0);
    }

    public float getLength()
    {
        return Mathf.Abs(arrowPoints[2].transform.localPosition.y - arrowPoints[0].transform.localPosition.y);
    }
}
