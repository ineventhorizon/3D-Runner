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
    private float forwardSpeed => SettingsManager.GameSettings.forwardSpeed;
    private float sideMovementSensivity => SettingsManager.GameSettings.sideMovementSensivity;
    private Vector3 cameraPosition;
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CurrentGameState != GameState.GAMEPLAY) return;
        HandleMovement();
    }
    private void OnEnable()
    {
        Observer.PlayerObstacleHit += HandleObstacleHit;
    }
    private void OnDisable()
    {
        Observer.PlayerObstacleHit -= HandleObstacleHit;
    }
    private void HandleMovement()
    {
        if (InputManager.Instance.MouseClicking)
        {
            //RayHitMousePoint();
            HandleForwardMovement();
            HandleSideMovement();
            UpdateAnimState(CharacterAnimState.RUNNING);
        }
        else
        {
            UpdateAnimState(CharacterAnimState.IDLE);
        }
    }
    //Swerve controls, player will move forward when touching the screen
    private void HandleForwardMovement()
    {
        if (GameManager.Instance.CurrentGameState != GameState.GAMEPLAY) return;
        transform.position += Vector3.forward * (forwardSpeed * Time.deltaTime);
        cameraPosition = transform.position;
        cameraPosition.x = 0;
        cameraFollow.position = cameraPosition;
    }
    private void HandleSideMovement()
    {
        if (GameManager.Instance.CurrentGameState != GameState.GAMEPLAY) return;
        var pos = sideMovementRoot.localPosition;
        pos.x += InputManager.Instance.MouseInput.x * sideMovementSensivity;
        pos.x = Mathf.Clamp(pos.x, leftLimitX, rightLimitX);
        sideMovementRoot.localPosition = Vector3.Lerp(sideMovementRoot.localPosition, pos, Time.deltaTime * 20f);

        if (GameManager.Instance.CurrentGameState != GameState.GAMEPLAY) return;
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


