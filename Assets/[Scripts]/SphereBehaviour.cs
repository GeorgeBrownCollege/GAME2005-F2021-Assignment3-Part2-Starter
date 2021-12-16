using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SphereBehaviour : Actor
{
    [Header("Collision Properties")] 
    public float radius;

    [Header("Movement")] 
    public float speed;
    public Vector3 direction;
    public bool isMoving;

    private MeshFilter meshFilter;

    void Start()
    {
        AddToCollisionManager();
        debug = false;

        // Find the Farthest vertex
        meshFilter = GetComponent<MeshFilter>();
        Vector3 farthestVertex = new Vector3();
        foreach (var vertex in meshFilter.mesh.vertices.ToList())
        {
            if (Vector3.Scale(vertex, transform.localScale).magnitude > farthestVertex.magnitude)
            {
                farthestVertex = Vector3.Scale(vertex, transform.localScale);
            }
        }

        radius = Vector3.Distance(transform.position, transform.position + farthestVertex);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            Move();
        }

        // if sphere is more than 100 units away from the origin
        if (transform.position.magnitude > 100.0f)
        {
            RemoveFromCollisionManager();
            Destroy(this.gameObject);
        }
    }

    private void Move()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
