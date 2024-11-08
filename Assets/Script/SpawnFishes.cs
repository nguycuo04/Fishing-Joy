using System.Collections;
using UnityEngine;

public class SpawnFishes : MonoBehaviour
{
    [SerializeField] private ListBoidVarriable boids;
    [SerializeField] private GameObject boidPrefab;
    [SerializeField] private float boidCount;

    private void Awake()
    {
        //gameObject.SetActive(true);
    }
    private void Start()
    {

        if (boids.boidMovements.Count > 0) boids.boidMovements.Clear();
        for (int i = 0; i < boidCount; i++)
        {
            float dirction = Random.Range(0f, 360f);  //random direction
            Vector3 position = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f)); //random position
            GameObject boid = Instantiate(boidPrefab, position, Quaternion.Euler(Vector3.forward * dirction) * boidPrefab.transform.localRotation); //spawn random direction and pisition
            boid.transform.SetParent(transform);
            boids.boidMovements.Add(boid.GetComponent<BoidMovement>()); //them vao danh sach
        }
    }
}
