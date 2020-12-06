using UnityEngine;

public class CookieDesign : MonoBehaviour
{
    public Material baseMaterial;
    public MeshRenderer renderer;

    public Texture2D initialDesign;

    private void Start()
    {
        SetDesign(initialDesign);
    }

    public void SetDesign(Texture2D design)
    {
        Material material = new Material(baseMaterial);
        material.mainTexture = design;
        renderer.material = material;
    }
}
