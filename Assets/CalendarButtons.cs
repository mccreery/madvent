using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalendarButtons : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject[] days = new GameObject[24];

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
            if (days[i] != null) days[i].SetActive(false);

            GameObject button = Instantiate(buttonPrefab, transform);
            button.GetComponentInChildren<Text>().text = (i + 1).ToString();
            button.GetComponentInChildren<Button>().onClick.AddListener(() => CurrentDay = i);
        }
    }

    private int currentDay;
    public int CurrentDay
    {
        get => currentDay;
        set
        {
            currentDay = value;
            for (int i = 0; i < 24; i++)
            {
                if (days[i] != null) days[i].SetActive(i == currentDay);
            }
        }
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
