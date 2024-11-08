using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BoidMovement : MonoBehaviour
{
    [SerializeField] private ListBoidVarriable boids; //Moi boid giu 1 danh sach chua cac boid xung quanh
    private float radius = 2f;
    private float visionAngle = 270f;

    private float turnSpeed = 10f;
    public Vector3 velocity { get; private set; }
    [SerializeField] private float forwardSpeed = 5f;

    private void FixedUpdate()
    {
        velocity = Vector2.Lerp(velocity, CalculateVelocity(),turnSpeed/2 * Time.fixedDeltaTime); //di chuyen boid di thang & them 1 vai quy luat
        transform.position += velocity * Time.fixedDeltaTime;
        LookRotation();
    }

    private Vector2 CalculateVelocity() //ham tinh toan van toc va huong di cua boid
    {
        var boidsInRange = BoidInRange();
        Vector2 velocity = ((Vector2)transform.forward
                            + 1.7f * Separation(boidsInRange)
                            + 0.1f * Aligment(boidsInRange)
                            + Cohesion(boidsInRange)
                            ).normalized * forwardSpeed;
        return velocity;
    }

    private void LookRotation() // xoay theo huong nhin cua boids
    {
        transform.rotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(velocity),turnSpeed * Time.fixedDeltaTime);
    }
    private List<BoidMovement> BoidInRange()
    {
        var listBoid = boids.boidMovements.FindAll(boid => boid != this
        && (boid.transform.position - transform.position).magnitude <= radius
        && InVisionCone(boid.transform.position));
        return listBoid;
    }

    private bool InVisionCone(Vector2 position) //Ham tinh vector tu boid hien tai den boid trong tam nhin
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
    private Vector2 Separation(List<BoidMovement> boidMovements) //tranh qua cham lan nhau
    {
        Vector2 direction = Vector2.zero;
        foreach(var boid in boidMovements)
        {
            float ratio = Mathf.Clamp01((boid.transform.position - transform.position)
                                        .sqrMagnitude/radius);
            direction -= ratio * (Vector2)(boid.transform.position - transform.position);
        }
        return direction.normalized;
    }    
    private Vector2 Aligment(List<BoidMovement> boidMovements) //di theo huong chung cac boid khac
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

    private Vector2 Cohesion(List<BoidMovement> boidMovements) //co gang huong ve phia trung tam
    {
        Vector2 direction;
        Vector2 center = Vector2.zero;
        foreach( var boid in boidMovements)
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
