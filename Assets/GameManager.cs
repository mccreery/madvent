using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameManager", order = 1)]
public class GameManager : ScriptableObject
{
    public int day = 0;
    public int score = 0;

    public void Reset()
    {
        day = 0;
        score = 0;
    }
}
