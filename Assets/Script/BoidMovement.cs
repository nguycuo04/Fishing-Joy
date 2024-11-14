using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidMovement : MonoBehaviour
{
    [SerializeField] private ListBoidVarriable boids;
    [SerializeField] public List<FishMovement> fishMovements;

    private float radius = 2f;
    private float visionAngle = 270f;
    private float avoidanceStrength = 10f;
    [SerializeField] private float fishAvoidanceStrength = 20f;

    private float turnSpeed = 10f;
    public Vector2 velocity { get; private set; }
    [SerializeField] private float forwardSpeed = 5f;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        velocity = Vector2.Lerp(velocity, CalculateVelocity(), turnSpeed / 2 * Time.fixedDeltaTime);
        transform.position += (Vector3)velocity * Time.fixedDeltaTime;
        LookRotation();
    }

    private Vector2 CalculateVelocity()
    {
        var boidsInRange = BoidInRange();
        Vector2 velocity = ((Vector2)transform.right
                            + 1.7f * Separation(boidsInRange)
                            + 0.1f * Aligment(boidsInRange)
                            + Cohesion(boidsInRange)
                            ).normalized * forwardSpeed;
        return velocity;
    }

    private void LookRotation()
    {
        // Tính góc cần xoay trên trục Z
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;

        // Nếu hướng thay đổi từ trái sang phải hoặc ngược lại, lật sprite
        if (velocity.x < 0)
        {
            spriteRenderer.flipY = true; // Lật theo trục Y nếu di chuyển sang trái
        }
        else
        {
            spriteRenderer.flipY = false; // Đảm bảo không lật khi di chuyển sang phải
        }

        // Cập nhật góc xoay trên trục Z
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private List<BoidMovement> BoidInRange()
    {
        var listBoid = boids.boidMovements.FindAll(boid => boid != this
        && (boid.transform.position - transform.position).magnitude <= radius
        && InVisionCone(boid.transform.position));
        return listBoid;
    }

    private bool InVisionCone(Vector2 position)
    {
        Vector2 directionToPosition = position - (Vector2)transform.position;
        float dotProduct = Vector2.Dot(transform.right, directionToPosition.normalized);
        float cosHalfVisionAngle = Mathf.Cos(visionAngle * 0.5f * Mathf.Deg2Rad);
        return dotProduct >= cosHalfVisionAngle;
    }

    private Vector2 Separation(List<BoidMovement> boidMovements)
    {
        Vector2 direction = Vector2.zero;

        foreach (var boid in boidMovements)
        {
            float distanceToBoid = (boid.transform.position - transform.position).sqrMagnitude;
            if (distanceToBoid <= radius * radius)
            {
                float ratio = Mathf.Clamp01(1 - (distanceToBoid / (radius * radius)));
                direction -= ratio * (Vector2)(boid.transform.position - transform.position).normalized * avoidanceStrength;
            }
        }

        foreach (var fish in fishMovements)
        {
            float distanceToFish = (fish.transform.position - transform.position).sqrMagnitude;
            if (distanceToFish <= radius * radius)
            {
                float ratio = Mathf.Clamp01(1 - (distanceToFish / (radius * radius)));
                direction -= ratio * (Vector2)(fish.transform.position - transform.position).normalized * fishAvoidanceStrength;
            }
        }

        return direction.normalized;
    }

    private Vector2 Aligment(List<BoidMovement> boidMovements)
    {
        Vector2 direction = Vector2.zero;
        foreach (var boid in boidMovements)
        {
            direction += (Vector2)boid.velocity;
        }
        if (boidMovements.Count != 0)
        {
            direction /= boidMovements.Count;
        }
        else
        {
            direction = velocity;
        }
        return direction.normalized;
    }

    private Vector2 Cohesion(List<BoidMovement> boidMovements)
    {
        Vector2 direction;
        Vector2 center = Vector2.zero;
        foreach (var boid in boidMovements)
        {
            center += (Vector2)boid.transform.position;
        }
        if (boidMovements.Count != 0)
        {
            center /= boidMovements.Count;
        }
        else
        {
            center = transform.position;
        }
        direction = center - (Vector2)transform.position;
        return direction.normalized;
    }
}
