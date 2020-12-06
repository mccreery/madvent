using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReindeerPickup : MonoBehaviour
{
    public SnakeMove snakeMove;
    public ReindeerSpawner raindeerSpawner;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("sprout") || snakeMove.reindeerTrail.Contains(other.transform))
        {
            snakeMove.Die();
        }

        else if (other.gameObject.CompareTag("pickup"))
        {
            snakeMove.AddTail(other.gameObject.transform);
            snakeMove.IncreaseSpeed();
            raindeerSpawner.SpawnRandomReindeer();
        }
    }
}
