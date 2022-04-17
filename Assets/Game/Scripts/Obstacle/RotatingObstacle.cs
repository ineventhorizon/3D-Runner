using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class RotatingObstacle : Obstacle
{
    [SerializeField] private Rigidbody movingStickRb;
    // Start is called before the first frame update
    void Start()
    {
        movingStickRb.DORotate(new Vector3(0, 360, 0), 1f).SetLoops(-1, LoopType.Incremental).SetUpdate(UpdateType.Fixed).SetEase(Ease.Linear);

    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(0, 0, 50 * Time.deltaTime);
    }
}
