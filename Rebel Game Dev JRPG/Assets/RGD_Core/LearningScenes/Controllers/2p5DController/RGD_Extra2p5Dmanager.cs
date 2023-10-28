using TMPro;
using UnityEngine;
public class RGD_Extra2p5Dmanager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI enterText;
    [SerializeField] private TextMeshProUGUI stayText;
    [SerializeField] private TextMeshProUGUI exitText;
    
    public void Trigger1(string type)
    {
        DisplayText(1, type);
    }
    public void Trigger2(string type)
    {
        DisplayText(2, type);
    }
    public void Trigger3(string type)
    {
        DisplayText(3, type);
    }
    private void DisplayText(int triggerNumber, string type)
    {
        if(type == "Enter") enterText.SetText($"Enter: <color=#0033ff>Trigger {triggerNumber}</color>");
        if(type == "Stay") stayText.SetText($"Stay: <color=#0033ff>Trigger {triggerNumber}</color>");
        if(type == "Exit") exitText.SetText($"Exit: <color=#0033ff>Trigger {triggerNumber}</color>");
    }
}
