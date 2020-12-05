using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    private int score = 0;
    public int Score
    {
        get =>score;
        set
        {
            score = value;
            UpdateText();
        }
    }

    private void Start()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        GetComponent<Text>().text = score.ToString("D3");
    }
}
