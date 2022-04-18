using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameSettings : ScriptableObject
{
    public float ForwardSpeed;
    public float SideMovementSensivity;

    public float OpponentSpeed;
    public float OpponentLeftLimit, OpponentRightLimit;

}