using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReindeerSpawner : MonoBehaviour
{
    public GameObject prefab;
    public SnakeMove snakeMove;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnRandomReindeer();
        }
    }

   public void SpawnRandomReindeer()
    {
        GameObject instance = Instantiate(prefab, snakeMove.RandomPosition(), Quaternion.identity);
        do
        {
            instance.transform.position = snakeMove.RandomPosition();
        }
        while (Vector3.Distance(snakeMove.FirstReindeer.transform.position, instance.transform.position) < 1.5);
    }
}
