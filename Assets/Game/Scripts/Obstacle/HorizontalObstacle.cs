using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalObstacle : Obstacle
{
    private enum MoveDirection
    {
        XAxis,
        YAxis
    }

    //TODO Need to set first direction, left or right or up and down

    [SerializeField] private MoveDirection moveDirection;
    [SerializeField] private float moveRange = 1, moveSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveBetweenRoutine());
    }
    private void OnDestroy()
    {
        StopCoroutine(MoveBetweenRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator MoveBetweenRoutine()
    {
        var direction = Vector3.zero;
        if (moveDirection == MoveDirection.XAxis) direction.x = 1*moveRange;
        else direction.z = 1*moveRange;
        while (true)
        {
            direction *= -1;
            yield return StartCoroutine(MoveToRoutine(direction));
        }
    }


    private IEnumerator MoveToRoutine(Vector3 direction)
    {
        var newPos = this.transform.position - direction;
        while (true)
        {
            
            this.transform.position = Vector3.MoveTowards(this.transform.position, newPos, Time.deltaTime * moveSpeed);
            var distanceLeft = (transform.position - newPos).sqrMagnitude;
            if (distanceLeft < 0.001f * 0.001f) break;

            yield return null;
        }
    }
}

