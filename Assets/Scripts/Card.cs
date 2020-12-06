using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public float rotateSpeed = 360;
    public bool flipped;

    public Image bottom, gift, top;

    public CardManager cardManager;

    public Sprite GiftSprite => gift.sprite;

    public void Update()
    {
        Quaternion targetRotation = flipped ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);

        top.gameObject.SetActive(FlipAngle < 90);
    }

    private float FlipAngle
    {
        get
        {
            float y = transform.eulerAngles.y;
            y = Mathf.Repeat(y, 360);

            if (y > 180) return 360 - y;
            else return y;
        }
    }

    public void Flip()
    {
        flipped = !flipped;
        if (flipped)
        {
            cardManager.OnCardFlipped(this);
        }
    }

    public bool FlipFinished => FlipAngle > 179;
}
