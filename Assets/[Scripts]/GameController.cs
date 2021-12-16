using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Sphere Launch Properties")]
    public List<GameObject> spherePrefabs;
    public float frameDelay;
    public float Offset;

    void Update()
    {
        if ((Input.GetAxisRaw("Fire1") > 0) && (Time.frameCount % frameDelay == 0))
        {
            var randomSphereIndex = Random.Range(0, spherePrefabs.Count);
            var bullet = Instantiate(spherePrefabs[randomSphereIndex], Camera.main.transform.position + Camera.main.transform.forward * Offset, Quaternion.identity);
            bullet.GetComponent<SphereBehaviour>().direction = Camera.main.transform.forward;
            bullet.GetComponent<SphereBehaviour>().isMoving = true;
        }
    }
}
