using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{

    [Range(0.1f, 20.0f)]
    public float velocity = 6.0f;

    [Range(0.0f, 0.9f)]
    public float velocityVariation = 0.5f;

    [Range(0.1f, 20.0f)]
    public float rotationCoeff = 4.0f;

    [Range(0.1f, 10.0f)]
    public float neighborDist = 2.0f;

    float noiseOffset;

    public LayerMask searchLayer;

    [Header("Collisions")]
    public LayerMask obstacleMask;
    public float boundsRadius = .27f;
    public float avoidCollisionWeight = 10;
    public float collisionAvoidDst = 5;


    private void Start()
    {
        noiseOffset = Random.value * 10.0f;
    }


    // Update is called once per frame
    void Update()
    {
        var currentPosition = transform.position;
        var currentRotation = transform.rotation;

        // Current velocity randomized with noise.
        var noise = Mathf.PerlinNoise(Time.time, noiseOffset) * 2.0f - 1.0f;
        var _velocity = velocity * (1.0f + noise * velocityVariation);

        // Initializes the vectors.
        var separation = Vector3.zero;
        var alignment = transform.forward;
        var cohesion = transform.position;

        // Looks up nearby boids.
        var nearbyBoids = Physics.OverlapSphere(currentPosition, neighborDist, searchLayer);

        // Accumulates the vectors.
        foreach (var boid in nearbyBoids)
        {
            if (boid.gameObject == gameObject) continue;
            var t = boid.transform;
            separation += GetSeparationVector(t);
            alignment += t.forward;
            cohesion += t.position;
        }

        var avg = 1.0f / nearbyBoids.Length;
        alignment *= avg;
        cohesion *= avg;
        cohesion = (cohesion - currentPosition).normalized;


        // Calculates a rotation from the vectors.
        var direction = separation + alignment + cohesion;

        if (IsHeadingForCollision())
        {
            direction += ObstacleRays() * 100;
            //Vector3 collisionAvoidForce = SteerTowards(collisionAvoidDir) * avoidCollisionWeight;
            //direction = collisionAvoidDir;
        }

        if (transform.position.y > 19)
            direction += new Vector3(0, -100, 0);

        var rotation = Quaternion.FromToRotation(Vector3.forward, direction.normalized);
        //var rotation = Quaternion.LookRotation(direction.normalized);

        // Applys the rotation with interpolation.
        //if (rotation != currentRotation)
        {
            var ip = Mathf.Exp(-rotationCoeff * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(rotation, currentRotation, ip);
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 40f);
        }

        // Moves forawrd.
        transform.position = currentPosition + transform.forward * (_velocity * Time.deltaTime);
    }

    Vector3 GetSeparationVector(Transform target)
    {
        var diff = transform.position - target.transform.position;
        var diffLen = diff.magnitude;
        var scaler = Mathf.Clamp01(1.0f - diffLen / neighborDist);
        return diff * (scaler / diffLen);
    }

    bool IsHeadingForCollision()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, boundsRadius, transform.forward, out hit, collisionAvoidDst, obstacleMask))
        {
            return true;
        }
        else { }
        return false;
    }

    Vector3 ObstacleRays()
    {
        Vector3[] rayDirections = BoidHelper.directions;

        for (int i = 0; i < rayDirections.Length; i++)
        {
            Vector3 dir = transform.TransformDirection(rayDirections[i]);
            Ray ray = new Ray(transform.position, dir);
            if (!Physics.SphereCast(ray, boundsRadius, collisionAvoidDst, obstacleMask))
            {
                return dir;
            }
        }

        return transform.forward;
    }
}
