using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public GameManager gameManager;

    private void Update()
    {
        GetComponent<TextMeshProUGUI>().text = $"{gameManager.score}/24";
    }
}
