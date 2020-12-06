using UnityEngine;
using System;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CookieDraw : MonoBehaviour
{
    public MeshRenderer uvSource;
    public MeshRenderer overlay;

    public new Camera camera;

    public Material penMaterial;

    [HideInInspector]
    public Texture2D texture;

    public float penRadius = 4;

    public int maskSize = 128;

    public GameObject cursor;
    public float cursorHeight;

    private void Start()
    {
        if (camera == null)
        {
            camera = Camera.main;
        }

        var overlayMaterial = new Material(penMaterial);

        texture = new Texture2D(maskSize, maskSize, TextureFormat.RGBA32, false);

        Color32 transparent = new Color32(255, 0, 0, 0);
        Color32[] fill = new Color32[texture.width * texture.height];
        for (int i = 0; i < fill.Length; i++) fill[i] = transparent;

        texture.SetPixels32(fill);
        texture.Apply();

        overlayMaterial.mainTexture = texture;
        overlay.material = overlayMaterial;
    }

    Vector3 cursorVelocity;

    private void Update()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        float distance = (cursorHeight - ray.origin.y) / ray.direction.y;
        Vector3 cursorTarget = ray.GetPoint(distance);

        if ((Input.GetMouseButton(0) || Input.GetMouseButton(1)) && Physics.Raycast(ray, out RaycastHit hit) && hit.transform == uvSource.transform
            && hit.textureCoord is Vector2 uv)
        {
            cursorTarget = hit.point;
            DrawCircle(texture, uv * new Vector2(texture.width, texture.height), penRadius, Input.GetMouseButton(0) ? Color.red : Color.green);
            texture.Apply();
        }

        if (cursor != null)
        {
            cursor.transform.position = Vector3.SmoothDamp(cursor.transform.position, cursorTarget, ref cursorVelocity, 0.1f);
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

        float colorBleed = 1;
        float radiusSq = (radius + colorBleed) * (radius + colorBleed);

        int i = 0;
        for (int y = minY; y <= maxY; y++)
        {
            for (int x = minX; x <= maxX; x++)
            {
                float dx = x - center.x;
                float dy = y - center.y;
                float distanceSq = dx * dx + dy * dy;

                float f = 1 - Mathf.Sqrt(distanceSq) / (2 * radius);
                f = Math.Max(f, pixels[i].a);

                Color pixelColor;
                if (distanceSq <= radiusSq)
                {
                    pixelColor = color;
                }
                else
                {
                    pixelColor = pixels[i];
                }

                pixelColor.a = f;
                pixels[i] = pixelColor;

                i++;
            }
        }

        texture2d.SetPixels(minX, minY, maxX - minX + 1, maxY - minY + 1, pixels);
    }

#if UNITY_EDITOR

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, 512, 512), texture);
        
        if (GUI.Button(new Rect(0, 20, 100, 20), "Save Design"))
        {
            string filename = EditorUtility.SaveFilePanel("Save Design", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "design", "png");
            File.WriteAllBytes(filename, texture.EncodeToPNG());
        }
    }

#endif
}
