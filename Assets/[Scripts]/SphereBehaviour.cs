using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBehaviour : MonoBehaviour
{
    [Header("Movement")] 
    public float speed;
    public Vector3 direction;

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
