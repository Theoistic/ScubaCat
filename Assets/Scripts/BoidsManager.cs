using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsManager : MonoBehaviour
{
    public int Max = 30;
    public float Radius = 30;
    public GameObject BoidPrefab;
    public GameObject BoidsContainer;
    

    // Update is called once per frame
    void LateUpdate()
    {
        var boids = FindObjectsOfType<Boid>();
        if(boids.Length < Max)
        {
            Vector3 pos = transform.position + (Random.onUnitSphere * Radius);
            if (pos.y < 20)
            {
                Quaternion rot = Quaternion.LookRotation(transform.position + (Random.insideUnitSphere * 5));
                var boid = GameObject.Instantiate(BoidPrefab, pos, rot);
                boid.transform.parent = BoidsContainer.transform;
            }
        }

        for (var i = 0; i < boids.Length; i++)
        {
            if(Vector3.Distance(transform.position, boids[i].transform.position) > Radius)
            {
                Destroy(boids[i].gameObject);
            }
        }
    }
}
