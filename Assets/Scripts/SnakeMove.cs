using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeMove : MonoBehaviour
{
	public Transform firstReindeer;

    public float interval;
    public float stepSize;
    [HideInInspector]
    public float timeSinceLastStep = 0f;

    Vector2 moveDirection = Vector2.right;


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
        firstReindeer.right = moveDirection;
        firstReindeer.position += firstReindeer.right * stepSize;
    }

    public void Die()
    {
        SceneManager.LoadScene(0);
    }
}
