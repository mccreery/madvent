using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeMove : MonoBehaviour
{
    public List<Transform> reindeerTrail = new List<Transform>();

    public Vector2Int boardMinXY;
    public Vector2Int boardMaxXY;

    public float interval;
    public float stepSize;
    [HideInInspector]
    public float timeSinceLastStep = 0f;

    public Vector2 HeadPosition => firstReindeer.position;

    Vector2 moveDirection = Vector2.right;

    private Transform firstReindeer;
    public Transform FirstReindeer => firstReindeer;

    private Quaternion previousEndRotation;
    private Vector3 previousEndPosition;

    // Start is called before the first frame update
    void Awake()
    {
        firstReindeer = reindeerTrail[0];
        winText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Win();
        Control();
        if (timeSinceLastStep >= interval)
        {
            Move();
            timeSinceLastStep = 0f;
        }
        timeSinceLastStep += Time.deltaTime;
    }

    public GameManager gameManager;

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

        if (firstReindeer.position.x > boardMaxXY.x || firstReindeer.position.y > boardMaxXY.y)
        {
            Die();
        }

        else if (firstReindeer.position.x < boardMinXY.x || firstReindeer.position.y < boardMinXY.y)
        {
            Die();
        }
    }

    public void AddTail(Transform reindeer)
    {
        reindeer.rotation = previousEndRotation;
        reindeer.position = previousEndPosition;
        reindeerTrail.Add(reindeer);
    }

    public void IncreaseSpeed()
    {
        interval = interval - 0.04f;
    }

    public Vector2 RandomPosition()
    {
        float x = Random.Range(boardMinXY.x, boardMaxXY.x) + 0.5f;
        float y = Random.Range(boardMinXY.y, boardMaxXY.y) + 0.5f;

        return new Vector2(x, y);
    }

    public void Die()
    {
        SceneManager.LoadScene(1);
    }

    public GameObject winText;

    public void Win()
    {
        if (reindeerTrail.Count == 10)
        {
            StartCoroutine(WinText());
        }
    }

    private IEnumerator WinText()
    {
        winText.SetActive(true);

        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;

        gameManager.score++;
        SceneManager.LoadScene(1);
    }
}
