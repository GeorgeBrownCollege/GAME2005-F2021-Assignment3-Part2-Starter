using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Color = UnityEngine.Color;

public class CubeBehaviour : Actor
{
    [Header("Collision Boundaries")]
    public Vector3 min;
    public Vector3 max;
    
    private Bounds bounds;
    private Renderer renderer;
    private Vector3 start;

    // Start is called before the first frame update
    void Start()
    {
        AddToCollisionManager();
        debug = false;

        renderer = GetComponent<Renderer>(); 
        bounds = renderer.bounds;

        start = transform.position;

        max = bounds.max;
        min = bounds.min;
    }

    // Update is called once per frame
    void Update()
    {
        if (start != transform.position)
        {
            var offset = transform.position - start;
            max = bounds.max + offset;
            min = bounds.min + offset;
        }
    }

    private void OnDrawGizmos()
    { 
        if (debug)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, bounds.max - bounds.min);
        }
    }
}
