using UnityEngine;

public class IcingSwatch : MonoBehaviour
{
    public CookieDraw cookieDraw;
    public GameObject cursor;
    public Color color;

    public Vector3 highlightOffset;
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private Vector3 velocity;
    private void Update()
    {
        Vector3 targetPosition = hovered ? initialPosition + highlightOffset : initialPosition;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0.1f);
    }

    private bool hovered = false;
    private void OnMouseEnter()
    {
        hovered = true;
    }

    private void OnMouseExit()
    {
        hovered = false;
    }

    public void OnMouseDown()
    {
        cursor.transform.position = cookieDraw.cursor.position;

        cookieDraw.cursor.gameObject.SetActive(false);
        cursor.SetActive(true);

        cookieDraw.cursor = cursor.transform;
        cookieDraw.currentColor = color;
    }
}
