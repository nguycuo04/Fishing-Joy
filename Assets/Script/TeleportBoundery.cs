using UnityEngine;

public class TeleportBoundery : MonoBehaviour //neu vuot qua gioi han se dich chuyen boid ve huong doi dien
{
    [SerializeField] private Boundery boundery;
    private void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.x) > boundery.xLimitPub)
        {
            if (transform.position.x > 0)
            {
                transform.position = new Vector3(-boundery.xLimitPub, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(boundery.xLimitPub, transform.position.y, transform.position.z);
            }
        }
        if (Mathf.Abs(transform.position.y) > boundery.yLimitPub)
        {
            if (transform.position.y > 0)
            {
                transform.position = new Vector3 (transform.position.x, -boundery.yLimitPub, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, boundery.yLimitPub, transform.position.z);
            }
        }
    }
}
