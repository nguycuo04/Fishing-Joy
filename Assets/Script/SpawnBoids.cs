using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoids : MonoBehaviour
{
    [SerializeField] private ListBoidVarriable boids;
    [SerializeField] private GameObject boidPrefab;
    [SerializeField] private float boidCount;

    private void Start()
    {
        // Tìm script SpawnFishesRandom để lấy danh sách cá câu được
        SpawnFishesRandom fishSpawner = FindObjectOfType<SpawnFishesRandom>();
        List<FishMovement> fishMovements = fishSpawner.fishMovements; // Đảm bảo lấy danh sách từ fishSpawner

        if (boids.boidMovements.Count > 0) boids.boidMovements.Clear();
        for (int i = 0; i < boidCount; i++)
        {
            float dirction = Random.Range(0f, 360f);
            Vector3 position = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
            GameObject boid = Instantiate(boidPrefab, position, Quaternion.Euler(Vector3.forward * dirction) * boidPrefab.transform.localRotation);
            boid.transform.SetParent(transform);

            BoidMovement boidMovement = boid.GetComponent<BoidMovement>();
            if (boidMovement != null)
            {
                // Gán danh sách cá câu được vào mỗi boid
                boidMovement.fishMovements = fishMovements;
                boids.boidMovements.Add(boidMovement);
            }
        }
    }

}
