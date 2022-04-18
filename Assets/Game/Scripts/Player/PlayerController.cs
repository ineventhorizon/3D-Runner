using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    [SerializeField] private Camera mainCamReference;
    [SerializeField] private LayerMask platformMask;
    [SerializeField] private Transform leftLimit, rightLimit, cameraFollow ,sideMovementRoot;
    private float leftLimitX => leftLimit.localPosition.x;
    private float rightLimitX => rightLimit.localPosition.x;
    private float forwardSpeed => SettingsManager.GameSettings.ForwardSpeed;
    private float sideMovementSensivity => SettingsManager.GameSettings.SideMovementSensivity;
    private Vector3 cameraPosition;
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CurrentGameState != GameState.GAMEPLAY) return;
        HandleMovement();
    }
    private void HandleMovement()
    {
        if (GameManager.Instance.CurrentGameState != GameState.GAMEPLAY) return;
        
        //Free control, rayhit controls
        //RayHitMousePoint();

        //Swerve controls
        HandleForwardMovement();
        HandleSideMovement();
    }
    //Swerve controls
    private void HandleForwardMovement()
    {
        //if (GameManager.Instance.CurrentGameState != GameState.GAMEPLAY) return;
        //Handles characters forward movement
        transform.position += Vector3.forward * (forwardSpeed * Time.deltaTime);
        cameraPosition = transform.position;
        cameraPosition.x = 0;
        cameraFollow.position = cameraPosition;
    }
    private void HandleSideMovement()
    {
        //if (GameManager.Instance.CurrentGameState != GameState.GAMEPLAY) return;
        //Handles character x movement
        var pos = sideMovementRoot.localPosition;
        pos.x += InputManager.Instance.MouseInput.x * sideMovementSensivity;
        pos.x = Mathf.Clamp(pos.x, leftLimitX, rightLimitX);
        sideMovementRoot.localPosition = Vector3.Lerp(sideMovementRoot.localPosition, pos, Time.deltaTime * 20f);


        //Rotation
        var moveDirection = Vector3.forward + InputManager.Instance.RawMouseInput.x * Vector3.right;
        var targetRotation = pos.x == leftLimitX || pos.x == rightLimitX ? Quaternion.LookRotation(Vector3.forward, Vector3.up) : Quaternion.LookRotation(moveDirection.normalized, Vector3.up);
        sideMovementRoot.localRotation = Quaternion.Lerp(sideMovementRoot.localRotation, targetRotation, Time.deltaTime * 5f);
    }
    //RayHitMousePoint and MoveCharacter are for free movement controls.
    //Player will move to the direction of the touch input with respect to the player character
    private void RayHitMousePoint()
    {

        var ray = mainCamReference.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, platformMask))
        {
            //Debug.Log("Hit");
            MoveCharacter(raycastHit.point, true);
        }
        else
        {
            //If not hit will move towards it's forward vector's direction
            MoveCharacter();
        }
    }
    private void MoveCharacter(Vector3? direction = null, bool hit = false)
    {
        if(direction?.sqrMagnitude > float.Epsilon && hit)
        {
            var newPoint = new Vector3((float)(direction?.x), sideMovementRoot.position.y, (float)direction?.z);
            var targetRotation = Quaternion.LookRotation(newPoint - sideMovementRoot.position, Vector3.up);
            sideMovementRoot.rotation = Quaternion.Slerp(sideMovementRoot.rotation, targetRotation, 20f * Time.deltaTime);
        }

        transform.position += (sideMovementRoot.forward) * (forwardSpeed * Time.deltaTime);
        cameraPosition = transform.position;
        cameraPosition.x = 0;
        cameraFollow.position = cameraPosition;
    }
    public override void HandleObstacleHit()
    {
        base.HandleObstacleHit();
        //Restarts the game
        GameManager.Instance.RestartGame();
    }
}


