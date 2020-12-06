using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CookieCheck : MonoBehaviour
{
    public CookieDraw draw;
    public Texture2D target;

    // Proportion of full error pixels corresponding to 0% score
    [Range(0, 1)]
    public float maxError = 0.25f;

    public GameObject ui;
    public TextMeshProUGUI percentageText;

    private void Start()
    {
        ui.SetActive(false);
    }

    public void ShowScore()
    {
        percentageText.text = "0%";
        ui.SetActive(true);
        StartCoroutine(AnimatePercentage());
    }

    [Min(0)]
    public float minScoreDelay = 0.05f;
    [Min(0)]
    public float maxScoreDelay = 0.1f;

    public float puffScale = 0.5f;
    public float puffDecaySpeed = 0.1f;

    private Vector3 puffVelocity;
    private void Update()
    {
        percentageText.transform.localScale = Vector3.SmoothDamp(percentageText.transform.localScale, Vector3.one, ref puffVelocity, puffDecaySpeed);
    }

    private IEnumerator AnimatePercentage()
    {
        int score = Mathf.RoundToInt(CalculateScore() * 100);

        for (int i = 0; i <= score; i++)
        {
            percentageText.text = $"{i}%";
            percentageText.transform.localScale = Vector3.one + new Vector3(puffScale, puffScale, puffScale);
            puffVelocity = Vector3.zero;

            float delay = Mathf.Lerp(minScoreDelay, maxScoreDelay, score);
            yield return new WaitForSeconds(delay);
        }
    }

    public float CalculateScore()
    {
        Texture2D drawing = draw.texture;

        Color[] drawingPixels = drawing.GetPixels();
        Color[] targetPixels = target.GetPixels();

        float totalErrorSq = 0;

        for (int i = 0; i < drawingPixels.Length; i++)
        {
            float maxComponentError = 0;

            for (int j = 0; j < 4; j++)
            {
                float error = Mathf.Abs(drawingPixels[i][j] - targetPixels[i][j]);
                maxComponentError = Mathf.Max(maxComponentError, error);
            }

            totalErrorSq += maxComponentError * maxComponentError;
        }

        float score = 1 - totalErrorSq / (maxError * drawingPixels.Length);
        return Mathf.Clamp01(score);
    }

#if UNITY_EDITOR

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 100, 100, 100), "Current score: " + CalculateScore());
        
        if (GUI.Button(new Rect(0, 400, 100, 20), "Show Score"))
        {
            ShowScore();
        }
    }

#endif
}
