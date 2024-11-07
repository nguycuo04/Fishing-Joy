using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Boundery")]
public class Boundery : ScriptableObject
{
    private float xLimit;
    private float yLimit;
    public float xLimitPub
    {
        get
        {
            CalculateLimit();
            return xLimit;
        }
    }
    public float yLimitPub
    {
        get
        {
            CalculateLimit();
            return yLimit;
        }
    }
    private void CalculateLimit()
    {
        yLimit = Camera.main.orthographicSize + 1f;
        xLimit = yLimit * Screen.width / Screen.height + 1f;
    }
}
