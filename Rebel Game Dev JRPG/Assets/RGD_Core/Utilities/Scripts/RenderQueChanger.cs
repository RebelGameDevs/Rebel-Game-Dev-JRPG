using UnityEngine;

public class RenderQueChanger : MonoBehaviour
{
    [SerializeField] private int renderQueToChange;
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
    public void ResetRenderQue(int render)
    {
        renderQueToChange = render;
        ResetMaterial();
    }
}
public class TestScript : MonoBehaviour
{
    [SerializeField] private RenderQueChanger renderQueToChange;
    private void ChangeRenderQue(int newRenderQue)
    {
        renderQueToChange.ResetRenderQue(newRenderQue);
    }
}

