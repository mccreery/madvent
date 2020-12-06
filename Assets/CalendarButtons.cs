using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CalendarButtons : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject[] days = new GameObject[24];

    public Sprite[] doors;

    public int[] dayToLevel;

    public GameManager gameManager;

    private void Start()
    {
        var dayNumbers = new List<int>();
        for (int i = 0; i < 24; i++)
        {
            dayNumbers.Add(i);
        }
        Shuffle(dayNumbers);

        foreach (int i in dayNumbers)
        {
            GameObject button = Instantiate(buttonPrefab, transform);

            button.GetComponentInChildren<Image>().sprite = doors[Random.Range(0, doors.Length)];

            button.GetComponentInChildren<Text>().text = (i + 1).ToString();

            Button buttonButton = button.GetComponentInChildren<Button>();
            buttonButton.enabled = i <= gameManager.day;
            buttonButton.onClick.AddListener(() =>
            {
                gameManager.day = i + 1;
                SceneManager.LoadScene(dayToLevel[i]);
            });
        }

        // todo if i == 24 show score
    }

    public static void Shuffle<T>(IList<T> list)
    {
        for (var i = list.Count; i > 0; i--)
            Swap(list, 0, Random.Range(0, i));
    }

    public static void Swap<T>(IList<T> list, int i, int j)
    {
        var temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }
}
