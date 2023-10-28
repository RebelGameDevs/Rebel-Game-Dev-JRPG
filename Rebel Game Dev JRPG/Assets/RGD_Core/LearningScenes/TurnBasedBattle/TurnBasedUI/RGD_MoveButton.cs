using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGD_MoveButton : MonoBehaviour
{
    public TurnBasedMove move;
   public RGD_TurnBasedManager manager;

    public void Clicked() {
        manager.MakePlayerAttack(move);
    }
}
