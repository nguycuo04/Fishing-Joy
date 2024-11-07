using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BoidMovement : MonoBehaviour
{
    [SerializeField] private ListBoidVarriable boids;
    private float radius = 2f;
    private float visionAngle = 270f;
    public Vector3 velocity { get; private set; }
    [SerializeField] private float forwardSpeed = 5f;

    private void FixedUpdate()
    {
        velocity = Vector2.Lerp(velocity, transform.forward.normalized * forwardSpeed, Time.fixedDeltaTime);
        transform.position += velocity * Time.fixedDeltaTime;
        LookRotation();
    }
    private void LookRotation()
    {
        transform.rotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(velocity), Time.fixedDeltaTime);
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
        Vector2 dirctionToPosition = position - (Vector2)transform.position;
        float dotProduct = Vector2.Dot(transform.forward, dirctionToPosition);
        float cosHalfVisionAngle = Mathf.Cos(visionAngle * 0.5f * Mathf.Deg2Rad);
        return dotProduct >= cosHalfVisionAngle;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
        var boidsInrange = BoidInRange();
        foreach (var boid in boidsInrange)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, boid.transform.position);
        }
    }
}
