using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeMove : MonoBehaviour
{
	public List<Transform> reindeerTrail = new List<Transform>();

    public float interval;
    public float stepSize;
    [HideInInspector]
    public float timeSinceLastStep = 0f;

    public Vector2 HeadPosition => firstReindeer.position;

    Vector2 moveDirection = Vector2.right;

    private Transform firstReindeer;

    private Quaternion previousEndRotation;
    private Vector3 previousEndPosition;

    // Start is called before the first frame update
    void Start()
    {
        firstReindeer = reindeerTrail[0];
    }

    // Update is called once per frame
    void Update()
    {
        Control();
        if (timeSinceLastStep >= interval)
        {
            Move();
            timeSinceLastStep = 0f;
        }
        timeSinceLastStep += Time.deltaTime;
    }

    void Control()
    {
		if (Input.GetAxisRaw("Horizontal") > 0.5)
		{
			moveDirection = Vector2.right;
		}
		else if (Input.GetAxisRaw("Horizontal") < -0.5)
		{
			moveDirection = Vector2.left;
		}
		else if (Input.GetAxisRaw("Vertical") > 0.5)
		{
			moveDirection = Vector2.up;
		}
		else if (Input.GetAxisRaw("Vertical") < -0.5)
		{
			moveDirection = Vector2.down;
		}
      

        if (moveDirection == -(Vector2)firstReindeer.right)
        {
            moveDirection = firstReindeer.right;
        }
    }

    void Move()
    {
        previousEndRotation = reindeerTrail[reindeerTrail.Count - 1].rotation;
        previousEndPosition = reindeerTrail[reindeerTrail.Count - 1].position;

        for (int i = reindeerTrail.Count - 1; i >= 1; i--)
        {
            var reindeer = reindeerTrail[i];
            var nextReindeer = reindeerTrail[i - 1];
            reindeer.rotation = nextReindeer.rotation;
            reindeer.position = nextReindeer.position;
        }

        firstReindeer.right = moveDirection;
        firstReindeer.position += firstReindeer.right * stepSize;
    }

    public void AddTail(Transform reindeer)
    {
        reindeer.rotation = previousEndRotation;
        reindeer.position = previousEndPosition;
        reindeerTrail.Add(reindeer);
    }

    public void Die()
    {
        SceneManager.LoadScene(0);
    }
}
