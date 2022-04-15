using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalGame : MonoBehaviour
{
    private bool isTriggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isTriggered) return;
            isTriggered = true;
            Debug.Log("Final");
            GameManager.Instance.StartFinal();
        }
        if (other.CompareTag("Opponent"))
        {
            other.gameObject.SetActive(false);
        }

    }
}
