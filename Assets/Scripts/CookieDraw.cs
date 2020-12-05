using System;
using System.Collections.Generic;
using UnityEngine;

public class CookieDraw : MonoBehaviour
{
    public MeshRenderer uvSource;
    public MeshRenderer overlay;

    public new Camera camera;

    public Material penMaterial;
    private Texture2D texture;

    public float penRadius = 10;

    private void Start()
    {
        if (camera == null)
        {
            camera = Camera.main;
        }

        var overlayMaterial = new Material(penMaterial);

        texture = new Texture2D(1024, 1024, TextureFormat.RGBA32, false);

        Color32 transparent = new Color32(255, 0, 0, 0);
        Color32[] fill = new Color32[texture.width * texture.height];
        for (int i = 0; i < fill.Length; i++) fill[i] = transparent;

        texture.SetPixels32(fill);
        texture.Apply();

        overlayMaterial.mainTexture = texture;
        overlay.material = overlayMaterial;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2? uvNullable = GetUVAtCursor();

            if (uvNullable is Vector2 uv)
            {
                //texture.SetPixel((int)(uv.x * texture.width), (int)(uv.y * texture.height), new Color(1, 0, 0, 1));

                DrawCircle(texture, uv * new Vector2(texture.width, texture.height), penRadius, Color.red);
                texture.Apply();
            }
        }
    }

    private Vector2? GetUVAtCursor()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform == uvSource.transform)
        {
            return hit.textureCoord;
        }
        else
        {
            return null;
        }
    }

    private void DrawCircle(Texture2D texture2d, Vector2 center, float radius, Color color)
    {
        int minX = Mathf.FloorToInt(center.x - radius);
        int maxX = Mathf.CeilToInt(center.x + radius);

        minX = Math.Max(minX, 0);
        maxX = Math.Min(maxX, texture2d.width - 1);

        int minY = Mathf.FloorToInt(center.y - radius);
        int maxY = Mathf.CeilToInt(center.y + radius);

        minY = Math.Max(minY, 0);
        maxY = Math.Min(maxY, texture2d.height - 1);

        Color[] pixels = texture2d.GetPixels(minX, minY, maxX - minX + 1, maxY - minY + 1);

        float radiusSq = radius * radius;

        int i = 0;
        for (int y = minY; y <= maxY; y++)
        {
            for (int x = minX; x <= maxX; x++)
            {
                float dx = x - center.x;
                float dy = y - center.y;
                float distanceSq = dx * dx + dy * dy;

                if (distanceSq < radiusSq)
                {
                    pixels[i] = color;
                }
                i++;
            }
        }

        texture2d.SetPixels(minX, minY, maxX - minX + 1, maxY - minY + 1, pixels);
    }
}
