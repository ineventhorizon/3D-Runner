using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OpponentAI : Character
{
    [SerializeField] private int numberOfRays = 17;
    [SerializeField] private float angle = 60, rayLength = 1f;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private Transform sideMovementRoot;
    private float speed => SettingsManager.GameSettings.OpponentSpeed;
    private float leftLimit => SettingsManager.GameSettings.OpponentLeftLimit;
    private float rightLimit => SettingsManager.GameSettings.OpponentRightLimit;
    private Vector3 moveDirection = Vector3.zero;
    private List<bool> hitList;
    // Start is called before the first frame update
    void Start()
    {
        hitList = Enumerable.Repeat(false, numberOfRays).ToList();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.Instance.CurrentGameState != GameState.GAMEPLAY)
        {
            this.rb.velocity = MoveDirection;
            return;
        }
        MoveCharacter();
    }
    private void MoveCharacter()
    {
        moveDirection = this.MoveDirection;
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
                deltaPosition -= (1f / numberOfRays) * direction;
                
            }
            else
            {
                Debug.DrawRay(transform.position + transform.up, direction * rayLength, Color.green, 0f);
                deltaPosition += (1f / numberOfRays) * direction;
            }
        }
        //TODO
        moveDirection += deltaPosition.normalized;
        
        var targetRotation = deltaPosition.x <= leftLimit || deltaPosition.x >= rightLimit ? Quaternion.LookRotation(Vector3.forward, Vector3.up) : Quaternion.LookRotation(moveDirection.normalized, Vector3.up);
        sideMovementRoot.localRotation = Quaternion.Lerp(sideMovementRoot.localRotation, targetRotation, Time.fixedDeltaTime * 5f);
        this.rb.velocity = moveDirection * speed * Time.fixedDeltaTime;

        if (this.rb.position.x <= leftLimit || this.rb.position.x >= rightLimit)
        {
            moveDirection = Vector3.zero;
            HandleObstacleHit();
        }
    }
    public override void HandleObstacleHit()
    {
        //Returns character to random spawn point
        transform.position = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-8f, -3f));
    }
}
