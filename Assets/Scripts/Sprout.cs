using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprout : MonoBehaviour
{
    public Vector2Int boardMinXY;
    public Vector2Int boardMaxXY;
    public SnakeMove snakeMove;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("MoveSprout", 0, 2);
    }

    void MoveSprout()
    {
        float sproutx = Random.Range(boardMinXY.x, boardMaxXY.x);
        float sprouty = Random.Range(boardMinXY.y, boardMaxXY.y);

        transform.position = new Vector2(sproutx, sprouty);
    }
}
