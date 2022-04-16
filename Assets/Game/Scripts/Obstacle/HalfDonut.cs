using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HalfDonut : MonoBehaviour
{
    [SerializeField] private Transform movingStick;
    private float timePassed = 0;
    // Start is called before the first frame update
    void Start()
    {
       // movingStick.transform.DOMoveX(-0.15f, 2f)
       //     .SetLoops(-1, LoopType.Yoyo)
       //     .SetUpdate(true);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Opponent"))
        {
            //other.attachedRigidbody.isKinematic = false;
            //other.attachedRigidbody.GetComponent<Character>().MoveDirection = new Vector3(0.3f, 0, 1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Opponent"))
        {
            //other.attachedRigidbody.isKinematic = false;
            //other.attachedRigidbody.GetComponent<Character>().MoveDirection = Vector3.zero;
        }
    }


}
