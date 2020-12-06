using UnityEngine;

public class CookieDesign : MonoBehaviour
{
    public Material baseMaterial;
    public new MeshRenderer renderer;

    public Texture2D[] designs;

    private void Start()
    {
        SetDesign(designs[Random.Range(0, designs.Length)]);
    }

    public void SetDesign(Texture2D design)
    {
        Material material = new Material(baseMaterial);
        material.mainTexture = design;
        renderer.material = material;
    }
}
