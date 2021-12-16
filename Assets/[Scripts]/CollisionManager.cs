using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class CollisionManager : MonoBehaviour
{
    public List<Actor> actors;

    // Start is called before the first frame update
    void Start()
    {
        //actors = FindObjectsOfType<Actor>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        DetectCollisions();
    }

    private void DetectCollisions()
    {
        for (int i = 0; i < actors.Count; i++)
        {
            for (int j = 0; j < actors.Count; j++)
            {
                if (i != j)
                {
                    if (actors[i].gameObject.GetComponent<CubeBehaviour>() &&
                        actors[j].gameObject.GetComponent<CubeBehaviour>())
                    {
                        CheckAABBs(actors[i] as CubeBehaviour, actors[j] as CubeBehaviour);
                    }

                    if (actors[i].gameObject.GetComponent<SphereBehaviour>() &&
                        actors[j].gameObject.GetComponent<CubeBehaviour>())
                    {
                        CheckSphereABBB(actors[i] as SphereBehaviour, actors[j] as CubeBehaviour);
                    }

                    if (actors[i].gameObject.GetComponent<SphereBehaviour>() &&
                        actors[j].gameObject.GetComponent<SphereBehaviour>())
                    {
                        CheckSpheres(actors[i] as SphereBehaviour, actors[j] as SphereBehaviour);
                    }
                }
            }
        }
    }

    public static void CheckAABBs(CubeBehaviour a, CubeBehaviour b)
    {
        if ((a.min.x <= b.max.x && a.max.x >= b.min.x) &&
            (a.min.y <= b.max.y && a.max.y >= b.min.y) &&
            (a.min.z <= b.max.z && a.max.z >= b.min.z))
        {
            AddContact(a, b);
        }
        else
        {
            RemoveContact(a, b);
        }

    }

    public static void CheckSphereABBB(SphereBehaviour a, CubeBehaviour b)
    {
        // get closest point to sphere center by clamping
        var x = Mathf.Max(b.min.x, Mathf.Min(a.transform.position.x, b.max.x));
        var y = Mathf.Max(b.min.y, Mathf.Min(a.transform.position.y, b.max.y));
        var z = Mathf.Max(b.min.z, Mathf.Min(a.transform.position.z, b.max.z));

        var dx = (x - a.transform.position.x);
        var dy = (y - a.transform.position.y);
        var dz = (z - a.transform.position.z);

        var distance = Mathf.Sqrt(dx * dx + dy * dy + dz * dz);

        if (distance < a.radius)
        {
            AddContact(a, b);
        }
        else
        {
            RemoveContact(a, b);
        }
    }


    public static void CheckSpheres(SphereBehaviour a, SphereBehaviour b)
    {
        if (Vector3.Distance(a.transform.position, b.transform.position) < a.radius + b.radius)
        {
            AddContact(a, b);
        }
        else
        {
            RemoveContact(a, b);
        }
    }

    // Utility Functions
    private static void RemoveContact(Actor a, Actor b)
    {
        if (a.contacts.Contains(b))
        {
            a.contacts.Remove(b);
            if (a.contacts.Count < 1)
            {
                a.isColliding = false;
            }
        }

        if (b.contacts.Contains(a))
        {
            b.contacts.Remove(a);
            if (b.contacts.Count < 1)
            {
                b.isColliding = false;
            }
        }
    }

    private static void AddContact(Actor a, Actor b)
    {
        if (!a.contacts.Contains(b))
        {
            a.contacts.Add(b);
            a.isColliding = true;
        }

        if (!b.contacts.Contains(a))
        {
            b.contacts.Add(a);
            b.isColliding = true;
        }
    }
}
