using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Observer.PlayerObstacleHit?.Invoke();
        }
        if (other.CompareTag("Opponent"))
        {
            other.GetComponent<OpponentAI>().HandleObstacleHit();
        }
    }
}
