using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    [Header("Collision Contacts")]
    public List<Actor> contacts;

    [Header("Collision State")]
    public bool isColliding;
    public bool debug;

    protected CollisionManager collisionManager;

    public void AddToCollisionManager()
    {
        collisionManager = FindObjectOfType<CollisionManager>();
        collisionManager.actors.Add(this);
    }

    public void RemoveFromCollisionManager()
    {
        collisionManager = FindObjectOfType<CollisionManager>();
        collisionManager.actors.Remove(this);
    }
}
