using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Opponent"))
        {
            other.attachedRigidbody.GetComponent<Character>().HandleObstacleHit();
        }
    }
}
