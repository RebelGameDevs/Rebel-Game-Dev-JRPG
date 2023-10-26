using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class john_playerHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counterText;
    private int counter = 0;
    public void UpdateCounter()
    {
        counter++;
        counterText.SetText($"<color=red>Entered # of times: </color>{counter}");
    }
}
