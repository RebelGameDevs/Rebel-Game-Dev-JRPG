using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class RGD_UI_Extra_Manager : MonoBehaviour
{
    [SerializeField] private RGD_UIGenericButton TheMainButton;
    [SerializeField] private TextMeshProUGUI counterText;
    private int counter = 0;
    [SerializeField] private Image backgroundMarginLeft;
    //Button 1:
    [SerializeField] private RGD_UIGenericButton button1;
    [SerializeField] private RGD_UIGenericButton button2;
    private bool changesColor = false;
    private void Awake()
    {
        TheMainButton.eventWhenPressed += AddToCounter;
        button1.eventWhenPressed += Button1Pressed;
        button2.eventWhenPressed += Button2Pressed;
    }
    private void AddToCounter()
    {
        counter++;
        counterText.SetText($"Counter: {counter}");
    }
    private void ChangeBackgroundColor()
    {
        backgroundMarginLeft.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1);
    }
    private void Button1Pressed()
    {
        if(TheMainButton.buttonClickAction is RGD_UIGenericButton.ButtonClickType.OnDown) TheMainButton.buttonClickAction = RGD_UIGenericButton.ButtonClickType.OnRelease;
        else TheMainButton.buttonClickAction = RGD_UIGenericButton.ButtonClickType.OnDown;
        button1.GetComponentInChildren<TextMeshProUGUI>().SetText($"Change Button Click Type: <color=#00CCFF>{TheMainButton.buttonClickAction}</color>");
    }
    private void Button2Pressed()
    {
        changesColor = !changesColor;
        if (changesColor)
        {
            TheMainButton.eventWhenPressed += ChangeBackgroundColor;
            button2.GetComponentInChildren<TextMeshProUGUI>().SetText($"Button Changes Background Color: <color=green>True</color>.");
            return;
        }
        TheMainButton.eventWhenPressed -= ChangeBackgroundColor;
        button2.GetComponentInChildren<TextMeshProUGUI>().SetText($"Button Changes Background Color: <color=red>False</color>.");
    }
   
}
