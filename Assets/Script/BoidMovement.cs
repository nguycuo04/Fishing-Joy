using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidMovement : MonoBehaviour
{
    [SerializeField] private ListBoidVarriable boids; // Mỗi boid giữ một danh sách chứa các boid xung quanh
    [SerializeField] public List<FishMovement> fishMovements; // Danh sách các cá câu được

    private float radius = 2f;
    private float visionAngle = 270f;
    private float avoidanceStrength = 10f; // Hệ số né tránh các cá thể câu được
    [SerializeField] private float fishAvoidanceStrength = 20f; // Hệ số né tránh các cá câu được


    private float turnSpeed = 10f;
    //public Vector3 velocity { get; private set; }
    public Vector2 velocity { get; private set; } // Sử dụng Vector2 thay vì Vector3

    [SerializeField] private float forwardSpeed = 5f;

    //private void FixedUpdate()
    //{
    //    velocity = Vector2.Lerp(velocity, CalculateVelocity(), turnSpeed / 2 * Time.fixedDeltaTime);
    //    transform.position += velocity * Time.fixedDeltaTime;
    //    LookRotation();
    //}
    private void FixedUpdate()
    {
        velocity = Vector2.Lerp(velocity, CalculateVelocity(), turnSpeed / 2 * Time.fixedDeltaTime);
        transform.position += (Vector3)velocity * Time.fixedDeltaTime; // Ép kiểu sang Vector3 để thay đổi vị trí
        LookRotation();
    }


    //private Vector2 CalculateVelocity()
    //{
    //    var boidsInRange = BoidInRange();
    //    Vector2 velocity = ((Vector2)transform.forward
    //                        + 1.7f * Separation(boidsInRange)
    //                        + 0.1f * Aligment(boidsInRange)
    //                        + Cohesion(boidsInRange)
    //                        ).normalized * forwardSpeed;
    //    return velocity;
    //}
    private Vector2 CalculateVelocity()
    {
        var boidsInRange = BoidInRange();
        Vector2 velocity = ((Vector2)transform.right // Sử dụng transform.right để lấy hướng 2D
                            + 1.7f * Separation(boidsInRange)
                            + 0.1f * Aligment(boidsInRange)
                            + Cohesion(boidsInRange)
                            ).normalized * forwardSpeed;
        return velocity;
    }


    //private void LookRotation()
    //{
    //    transform.rotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(velocity), turnSpeed * Time.fixedDeltaTime);
    //}
    private void LookRotation()
    {
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle); // Chỉ xoay trên trục Z
    }


    private List<BoidMovement> BoidInRange()
    {
        var listBoid = boids.boidMovements.FindAll(boid => boid != this
        && (boid.transform.position - transform.position).magnitude <= radius
        && InVisionCone(boid.transform.position));
        return listBoid;
    }

    //private bool InVisionCone(Vector2 position)
    //{
    //    Vector2 directionToPosition = position - (Vector2)transform.position;
    //    float dotProduct = Vector2.Dot(transform.forward, directionToPosition);
    //    float cosHalfVisionAngle = Mathf.Cos(visionAngle * 0.5f * Mathf.Deg2Rad);
    //    return dotProduct >= cosHalfVisionAngle;
    //}
    private bool InVisionCone(Vector2 position)
    {
        Vector2 directionToPosition = position - (Vector2)transform.position;
        float dotProduct = Vector2.Dot(transform.right, directionToPosition.normalized); // Sử dụng transform.right cho hướng 2D
        float cosHalfVisionAngle = Mathf.Cos(visionAngle * 0.5f * Mathf.Deg2Rad);
        return dotProduct >= cosHalfVisionAngle;
    }


    private Vector2 Separation(List<BoidMovement> boidMovements)
    {
        Vector2 direction = Vector2.zero;

        // Tránh né các cá boid khác
        foreach (var boid in boidMovements)
        {
            float distanceToBoid = (boid.transform.position - transform.position).sqrMagnitude;
            if (distanceToBoid <= radius * radius)
            {
                float ratio = Mathf.Clamp01(1 - (distanceToBoid / (radius * radius)));
                direction -= ratio * (Vector2)(boid.transform.position - transform.position).normalized * avoidanceStrength;
            }
        }

        // Tránh né các cá câu được
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);

        // Kết nối boid với các boid khác trong phạm vi
        var boidsInRange = BoidInRange();
        foreach (var boid in boidsInRange)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, boid.transform.position);
        }

        // Kết nối boid với các cá câu được trong phạm vi
        foreach (var fish in fishMovements)
        {
            if ((fish.transform.position - transform.position).sqrMagnitude <= radius * radius)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(transform.position, fish.transform.position);
            }
        }
    }

}
