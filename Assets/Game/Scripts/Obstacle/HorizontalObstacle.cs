using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HorizontalObstacle : Obstacle
{
    private enum MoveDirection
    {
        XAxis,
        YAxis
    }
    [SerializeField] private MoveDirection moveDirection;
    [SerializeField] private float moveRange = 1, moveTime = 1f;
    private Tween obstacleTween;
    // Start is called before the first frame update
    void Start()
    {
        switch (moveDirection)
        {
            case MoveDirection.XAxis:
                obstacleTween = transform.DOMove(Vector3.right * moveRange, moveTime)
            .SetLoops(-1, LoopType.Yoyo)
            .SetRelative();
                break;
            case MoveDirection.YAxis:
                obstacleTween = transform.DOMove(Vector3.forward * moveRange, moveTime)
            .SetLoops(-1, LoopType.Yoyo)
            .SetRelative();
                break;
            default:
                break;
        }
    }
    private void OnDisable()
    {
        DOTween.Kill(obstacleTween);
    }
}

