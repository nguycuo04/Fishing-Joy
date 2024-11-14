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

    private void Start()
    {
        // Đặt hướng ban đầu của cá theo hướng mà nó đang đối diện
        direction = transform.right;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Di chuyển cá theo hướng hiện tại, áp dụng lerp để chuyển hướng mượt hơn
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        CheckBoundsAndTurn();
        RotateTowardsMovement();
        Caught();
    }

    private void CheckBoundsAndTurn()
    {
        Vector2 currentPosition = transform.position;
        bool isOutOfBounds = false;

        // Kiểm tra giới hạn theo trục X và cập nhật hướng di chuyển
        if (currentPosition.x > movementBounds.x || currentPosition.x < -movementBounds.x)
        {
            isOutOfBounds = true;
            direction = new Vector2(-direction.x, direction.y);
        }

        // Kiểm tra giới hạn theo trục Y và cập nhật hướng di chuyển
        if (currentPosition.y > movementBounds.y || currentPosition.y < -movementBounds.y)
        {
            isOutOfBounds = true;
            direction = new Vector2(direction.x, -direction.y);
        }

        // Khi chạm giới hạn, chọn góc ngẫu nhiên để thay đổi hướng di chuyển mượt mà
        if (isOutOfBounds)
        {
            float randomAngle = Random.Range(-45f, 45f); // Thay đổi nhỏ góc xoay để tự nhiên hơn
            direction = Quaternion.Euler(0, 0, randomAngle) * direction;
            direction.Normalize();

            // Lật sprite nếu cần thiết
            spriteRenderer.flipY = direction.x < 0;
        }
    }

    private void RotateTowardsMovement()
    {
        // Tính góc cần xoay theo hướng di chuyển
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Xoay nhanh chóng hướng đầu cá về phía di chuyển
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * 8f);
    }

    private void Caught()
    {
        if (caughtFish)
        {
            caughtFish = true;
            spriteRenderer.enabled = false;
            StartCoroutine(RespawnFish());
        }
    }

    IEnumerator RespawnFish()
    {
        yield return new WaitForSeconds(3);
        caughtFish = false;
        spriteRenderer.enabled = true;
    }
}
