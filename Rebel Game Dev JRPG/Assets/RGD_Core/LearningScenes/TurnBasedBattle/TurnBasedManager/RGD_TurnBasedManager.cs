using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGD_TurnBasedManager : MonoBehaviour
{
    [SerializeField] private List<TurnBasedCharacter> characters;
    [SerializeField] private TurnBasedCharacter currentPlayer;
    [SerializeField] private TurnBasedCharacter currentEnemy;

    public void MakePlayerAttack (TurnBasedMove move)
    {

        currentEnemy.health -= move.damage;
    }
}

