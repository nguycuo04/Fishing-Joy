using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawnFishesRandom : MonoBehaviour
{
    public GameObject[] fishPrefabs; // Các prefab cá có sẵn
    public int fishCount = 10; // Số lượng cá muốn spawn
    public float spawnRangeX = 10f; // Phạm vi theo trục X
    public float spawnRangeY = 10f; // Phạm vi theo trục Y
    public float fishSpeed = 2f; // Tốc độ bơi của cá
    public float movementRange = 5f;

    void Start()
    {
        for (int i = 0; i < fishCount; i++)
        {
            SpawnFish();
        }
    }

    void SpawnFish()
    {
        // Lựa chọn ngẫu nhiên một prefab cá
        GameObject fishPrefab = fishPrefabs[Random.Range(0, fishPrefabs.Length)];

        // Tạo vị trí ngẫu nhiên trong phạm vi
        Vector2 spawnPosition = new Vector2(Random.Range(-spawnRangeX, spawnRangeX), Random.Range(-spawnRangeY, spawnRangeY));

        // Tạo cá tại vị trí ngẫu nhiên
        GameObject fishInstance = Instantiate(fishPrefab, spawnPosition, Quaternion.identity);

        // Thiết lập hướng ngẫu nhiên cho cá, giới hạn trong khoảng -90 đến 90 độ
        float randomDirection = Random.Range(-90f, 90f);
        fishInstance.transform.rotation = Quaternion.Euler(0, 0, randomDirection);

        // Gắn script di chuyển cá và khởi tạo tốc độ
        FishMovement fishMovement = fishInstance.AddComponent<FishMovement>();
        //fishMovement.Initialize(fishSpeed, movementRange);
    }
}

