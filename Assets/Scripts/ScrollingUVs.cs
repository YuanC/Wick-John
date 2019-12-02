using UnityEngine;
using System.Collections;

// Scrolls the texture UVs over time
public class ScrollingUVs : MonoBehaviour
{
    private Renderer rend;
    private Vector2 uvOffset;

    public Vector2 uvAnimationRate = new Vector2(1.0f, 0.0f);

    void Start()
    {
        rend = GetComponent<Renderer>();
        uvOffset = Vector2.zero;
    }

    void Update()
    {
        uvOffset += (uvAnimationRate * Time.deltaTime);
        if (rend.enabled)
        {
            rend.material.SetTextureOffset("_MainTex", uvOffset);
        }
    }
}