using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardManager : MonoBehaviour
{
    [System.Serializable]
    public struct GiftBox
    {
        public Sprite bottom, top;
    }

    public GiftBox[] boxSprites;
    public Sprite[] gifts;

    private List<Card> cards;

    public GameObject cardPrefab;

    public int maxLives;

    public TextMeshProUGUI livesText;

    private int lives;
    private int Lives
    {
        get => lives;
        set
        {
            lives = value;
            if (livesText != null)
            {
                livesText.text = $"Lives: {lives}";
            }
        }
    }

    private void Start()
    {
        Lives = maxLives;

        var giftsList = new List<Sprite>();
        giftsList.AddRange(gifts);
        giftsList.AddRange(gifts);

        CalendarButtons.Shuffle(giftsList);

        foreach (Sprite gift in giftsList)
        {
            GameObject instance = Instantiate(cardPrefab, transform);
            Card card = instance.GetComponentInChildren<Card>();

            card.cardManager = this;

            GiftBox box = boxSprites[Random.Range(0, boxSprites.Length)];
            card.bottom.sprite = box.bottom;
            card.top.sprite = box.top;

            card.gift.sprite = gift;
        }
    }

    Card lastFlipped;
    private int score = 0;

    public GameManager gameManager;

    public void OnCardFlipped(Card card)
    {
        if (lastFlipped == null)
        {
            lastFlipped = card;
        }
        else
        {
            if (lastFlipped.GiftSprite == card.GiftSprite)
            {
                ++score;
                if (score == gifts.Length)
                {
                    ++gameManager.score;
                    SceneManager.LoadScene(1);
                }
            }
            else
            {
                StartCoroutine(FlipBack(lastFlipped, card));
            }
            lastFlipped = null;
        }
    }

    private IEnumerator FlipBack(Card a, Card b)
    {
        yield return new WaitUntil(() => b.FlipFinished);
        yield return new WaitForSeconds(0.5f);

        --Lives;
        if (Lives <= 0)
        {
            SceneManager.LoadScene(1);
        }

        a.Reset();
        b.Reset();
    }
}
