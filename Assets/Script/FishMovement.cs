using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // Tốc độ di chuyển của cá
    [SerializeField] private Vector2 movementBounds; // Giới hạn di chuyển theo trục X và Y
    private Vector2 direction; // Hướng di chuyển hiện tại của cá

    [SerializeField] private bool caughtFish = false;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {

        direction = transform.right; // Đặt hướng ban đầu của cá theo hướng mà nó đang đối diện
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        
        transform.Translate(direction * speed * Time.deltaTime, Space.World); // Di chuyển cá theo hướng hiện tại, áp dụng lerp để chuyển hướng mượt hơn

        CheckBoundsAndTurn();
        RotateTowardsMovement();
        Caught();
    }

    private void CheckBoundsAndTurn()
    {
        Vector2 currentPosition = transform.position;
        bool isOutOfBounds = false;

        
        if (currentPosition.x > movementBounds.x || currentPosition.x < -movementBounds.x) // Kiểm tra giới hạn theo trục X và cập nhật hướng di chuyển
        {
            isOutOfBounds = true;
            direction = new Vector2(-direction.x, direction.y);
        }

        
        if (currentPosition.y > movementBounds.y || currentPosition.y < -movementBounds.y) // Kiểm tra giới hạn theo trục Y và cập nhật hướng di chuyển
        {
            isOutOfBounds = true;
            direction = new Vector2(direction.x, -direction.y);
        }

        
        if (isOutOfBounds) // Khi chạm giới hạn, chọn góc ngẫu nhiên để thay đổi hướng di chuyển mượt mà
        {
            float randomAngle = Random.Range(-45f, 45f); // Thay đổi nhỏ góc xoay để tự nhiên hơn
            direction = Quaternion.Euler(0, 0, randomAngle) * direction;
            direction.Normalize();

            
            spriteRenderer.flipY = direction.x < 0; // Lật sprite nếu cần thiết
        } 
    }

    private void RotateTowardsMovement() // Hàm vừa xoay vừa di chuyển
    {
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Tính góc cần xoay theo hướng di chuyển

        
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * 8f); // Xoay nhanh chóng hướng đầu cá về phía di chuyển
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
