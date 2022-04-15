using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class Observer
{
    public static UnityAction PlayerObstacleHit;
    //This can be an event for every character.
    public static UnityAction<CharacterAnimState> OpponentsAnimState;

}
