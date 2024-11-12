using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float speed = 5f; // Tốc độ di chuyển của cá
    public Vector2 movementBounds; // Giới hạn di chuyển theo trục X và Y

    private Vector2 direction; // Hướng di chuyển hiện tại của cá
    [SerializeField] private bool caughtFish = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Đặt hướng ban đầu của cá theo hướng mà nó đang đối diện
        direction = transform.right;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        // Di chuyển cá theo hướng hiện tại
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        CheckBoundsAndTurn();
        Caught();
    }

    private void CheckBoundsAndTurn()
    {
        Vector2 currentPosition = transform.position;

        // Nếu cá vượt giới hạn theo trục X
        if (currentPosition.x > movementBounds.x || currentPosition.x < -movementBounds.x)
        {
            // Quay ngược hướng theo trục X
            direction = new Vector2(-direction.x, direction.y);
            // Lật đối tượng để hướng quay lại (nếu cần)
            transform.Rotate(0, 180, 0);
        }

        // Nếu cá vượt giới hạn theo trục Y
        if (currentPosition.y > movementBounds.y || currentPosition.y < -movementBounds.y)
        {
            // Quay ngược hướng theo trục Y
            direction = new Vector2(direction.x, -direction.y);
            // Lật đối tượng để hướng quay lại (nếu cần)
            transform.Rotate(0, 180, 0);
        }
    }



    private void Caught()
    {
        if (caughtFish)
        {
            caughtFish=true;
            spriteRenderer.enabled = false;
            StartCoroutine(RespawnFish());
        }
        
    }

    IEnumerator RespawnFish()
    {
        yield return new WaitForSeconds(3);
        caughtFish = false;
        spriteRenderer.enabled=true;
    }
}

