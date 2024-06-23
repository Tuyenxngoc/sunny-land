using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    private Material material;
    private Vector2 offset = Vector2.zero;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        offset = material.GetTextureOffset("_MainTex");
    }

    void Update()
    {
        float x = Mathf.Repeat(Time.time * scrollSpeed, 1);
        offset.x = x;
        material.SetTextureOffset("_MainTex", offset);
    }
}
