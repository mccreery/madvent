using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour {
  private int score = 0;
  public int Score {
    get => score;
    set {
      score = value;

            if (score == 5)
            {
                gameManager.score++;
                SceneManager.LoadScene(1);
            }

      UpdateText();
    }
  }

    public GameManager gameManager;

  private void Start() {
    UpdateText();
  }

  private void UpdateText() {
    GetComponent<Text>().text = score.ToString("D3");
  }
}
