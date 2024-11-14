using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawnFishesRandom : MonoBehaviour
{
    [SerializeField] private GameObject[] fishPrefabs; // Danh sách các prefab của cá
    [SerializeField] private int fishCount; // Số lượng cá cần spawn
    [SerializeField] private Vector2 spawnAreaSize; // Kích thước khu vực spawn cá (dài, rộng)
    public List<FishMovement> fishMovements = new List<FishMovement>();
    void Start()
    {
        SpawnFish();
    }

    void SpawnFish()
    {
        for (int i = 0; i < fishCount; i++)
        {
            // Chọn ngẫu nhiên một vị trí trong khu vực spawn
            Vector2 spawnPosition = new Vector2(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
            );

            // Chọn ngẫu nhiên một prefab cá từ danh sách
            GameObject fishPrefab = fishPrefabs[Random.Range(0, fishPrefabs.Length)];

            // Tạo cá tại vị trí spawn ngẫu nhiên
            GameObject fish = Instantiate(fishPrefab, spawnPosition, Quaternion.identity);

            // Thêm FishMovement của cá vào danh sách fishMovements
            FishMovement fishMovement = fish.GetComponent<FishMovement>();
            if (fishMovement != null)
            {
                fishMovements.Add(fishMovement);
            }
        }
    }

}

