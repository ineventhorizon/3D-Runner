using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentAI : Character
{
    
    [SerializeField] private int numberOfRays = 17;
    [SerializeField] private float angle = 60, rayLength = 1f;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private Transform sideMovementRoot;
    private float speed;
    private Vector3 oldPos;
    private Vector3 moveDirection = Vector3.zero;
    private List<bool> hitList;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(4, 5f);
        hitList = new List<bool>();
        for(int i = 0; i < numberOfRays; i++)
        {
            hitList.Add(false);
        }
        oldPos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CurrentGameState != GameState.GAMEPLAY) return;
        MoveCharacter();
    }
    private void MoveCharacter()
    {
        moveDirection = Vector3.zero;
        RaycastHit raycastHit;
        var deltaPosition = Vector3.zero;

        for (int i = 0; i < numberOfRays; i++)
        {
            hitList[i] = false;
            var rotation = this.transform.rotation;
            //When i is 0 new angle will be -angle 
            //When i is numberOfRays-1 new angle will be angle
            var rotationMod = Quaternion.AngleAxis((i / ((float)numberOfRays-1)) * angle * 2 - angle, this.transform.up);
            var direction = rotation * rotationMod * Vector3.forward;

            var ray = new Ray(this.transform.position+ transform.up, direction);
            if (Physics.Raycast(ray, out raycastHit, rayLength, obstacleMask))
            {
                hitList[i] = true;
                Debug.DrawRay(transform.position + transform.up, direction * rayLength, Color.red, 0f);
                deltaPosition -= (1f / numberOfRays) * direction*5f;
                
            }
            else
            {
                Debug.DrawRay(transform.position + transform.up, direction * rayLength, Color.green, 0f);
                deltaPosition += (1f / numberOfRays) * direction*5f;
            }
        }
        //TODO
        moveDirection = deltaPosition.normalized;
        
        var targetRotation = deltaPosition.x <= -6 || deltaPosition.x >= 6 ? Quaternion.LookRotation(Vector3.forward, Vector3.up) : Quaternion.LookRotation(moveDirection.normalized, Vector3.up);
        sideMovementRoot.localRotation = Quaternion.Lerp(sideMovementRoot.localRotation, targetRotation, Time.deltaTime * 5f);
        this.transform.position += moveDirection * speed * Time.deltaTime;

        if (transform.position.x <= -6 || transform.position.x >= 6)
        {
            moveDirection = Vector3.zero;
            HandleObstacleHit();
        }
    }
    public override void HandleObstacleHit()
    {
        base.HandleObstacleHit();
        //Returns player to spawn point which is character's start point
        transform.position = oldPos;
    }
}
