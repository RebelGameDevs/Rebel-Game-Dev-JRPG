using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGD_MI_Generic : RGD_MouseInteractable
{
    [SerializeField] private string hoveredMessage = "";
    private Coroutine isAnimating = null;
    private Outline outline;
    private void Awake()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }
    public override string HoveredOver()
    {
        if(isAnimating != null) return "";
        outline.enabled = true;
        return hoveredMessage;
    }
    public override void StoppedHovering()
    {
        outline.enabled = false;
    }
    public override void ClickedOn()
    {
        if(isAnimating != null) return;
        isAnimating = StartCoroutine(LerpOutIE());
    }
    private IEnumerator LerpOutIE()
    {
        float localTTime = 0;
        Vector3 startScale = transform.localScale;
        while(localTTime < 1)
        {
            localTTime += Time.deltaTime / .5f;
            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, localTTime);
            yield return null;
        }
        Destroy(this.gameObject);
    }
}
