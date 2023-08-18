using UnityEngine;

public class RenderQueChanger : MonoBehaviour
{
    public int renderQueToChange;
    public void Awake()
    {
        if(gameObject.TryGetComponent(out Renderer renderer))
        {
            renderer.material.renderQueue = renderQueToChange;
        }
    }
    [ContextMenu("Reseter")]
    public void ResetMaterial()
    {
        if(gameObject.TryGetComponent(out Renderer renderer))
        {
            renderer.material.renderQueue = renderQueToChange;
        }
    }
}
