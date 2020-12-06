using UnityEngine;
using UnityEngine.SceneManagement;

public class Preload : MonoBehaviour
{
    public GameManager gameManager;

    void Start()
    {
        gameManager.Reset();
        SceneManager.LoadScene(1);
    }
}
