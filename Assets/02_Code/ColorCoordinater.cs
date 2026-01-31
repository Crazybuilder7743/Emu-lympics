using UnityEngine;

public class ColorCoordinater : MonoBehaviour
{

    public Renderer _maskRenderer;

    public void ChangeMaskColor(HeadMask mask)
    {
        _maskRenderer.material.color = mask.maskColor;
    }
}
