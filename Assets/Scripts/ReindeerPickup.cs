using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReindeerPickup : MonoBehaviour
{
    public SnakeMove snakeMove;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("pickup"))
        {
            snakeMove.AddTail(other.gameObject.transform);
        }
    }
}
