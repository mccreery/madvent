using UnityEngine;

public class IcingSwatch : MonoBehaviour
{
    public CookieDraw cookieDraw;
    public GameObject cursor;
    public Color color;

    public void OnMouseDown()
    {
        cursor.transform.position = cookieDraw.cursor.position;

        cookieDraw.cursor.gameObject.SetActive(false);
        cursor.SetActive(true);

        cookieDraw.cursor = cursor.transform;
        cookieDraw.currentColor = color;
    }
}
