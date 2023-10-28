using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class TurnBasedCharacter : MonoBehaviour
{
    public List<TurnBasedMove> MoveList = new List<TurnBasedMove>();
    public float maxHealth = 100f;
    public float health;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    TurnBasedMove getMoveByIndex(int index) 
    {
        return MoveList[index];
    }

    TurnBasedMove getMoveByName(string moveName)
    {
        foreach (TurnBasedMove move in MoveList) {
            if (move.moveName == moveName)
            {
                return move;
            }
        }
        Debug.LogException(new Exception("Move " + moveName + " does not exist."));
        return null;
    }
}