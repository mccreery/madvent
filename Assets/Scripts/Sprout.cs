using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprout : MonoBehaviour
{
    public SnakeMove snakeMove;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("MoveSprout", 0, 3);
    }

    void MoveSprout()
    {
        do
        {
            transform.position = snakeMove.RandomPosition();
        }
        while (Vector3.Distance(snakeMove.FirstReindeer.transform.position, transform.position) <1.5);
    }
}
