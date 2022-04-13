using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera mainCamReference;
    [SerializeField] private LayerMask platformMask;
    [SerializeField] private Animator animator;
    [SerializeField] private float speed;
    [SerializeField] private Transform cameraFollow ,sideMovementRoot, player;
    private Vector3 cameraPosition;
    // Update is called once per frame
    void Update()
    {
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
        //TODO XY MOVEMENT
        //MODULAR FOR CHARACTER AI
        //TODO RAYCAST CODE NEEDS TO BE MORE ORGANIZED, NEED TO GET MAINCAM REFERENCE   
        
        if (InputManager.Instance.MouseClicking)
        {
            RayHitMousePoint();
            UpdateAnimState(PlayerAnimStates.RUNNING);
        }
        else
        {
            UpdateAnimState(PlayerAnimStates.IDLE);
        }
    }
    //Will move the character towards it's forward vector
    private void MoveCharacter(Vector3? direction = null, bool hit = false)
    {
        if(direction?.sqrMagnitude > float.Epsilon && hit)
        {
            var newPoint = new Vector3((float)(direction?.x), sideMovementRoot.position.y, (float)direction?.z);
            var targetRotation = Quaternion.LookRotation(newPoint - sideMovementRoot.position, Vector3.up);
            sideMovementRoot.rotation = Quaternion.Slerp(sideMovementRoot.rotation, targetRotation, 20f * Time.deltaTime);
        }

        sideMovementRoot.position += sideMovementRoot.forward * (speed * Time.deltaTime);
        cameraPosition = sideMovementRoot.position;
        cameraPosition.x = 0;
        cameraFollow.position = cameraPosition;
    }
    private void RayHitMousePoint()
    {

        var ray = mainCamReference.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, platformMask))
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
    private void UpdateAnimState(PlayerAnimStates state)
    {
        switch (state)
        {
            case PlayerAnimStates.IDLE:
                animator.SetBool("Running", false);
                break;
            case PlayerAnimStates.RUNNING:
                animator.SetBool("Running", true);
                break;
            default:
                break;
        }
    }
    private void HandleObstacleHit()
    {
        //Returns player to spawn point which is 0,0,0
        sideMovementRoot.position = cameraFollow.position = Vector3.zero;
    }
}

public enum PlayerAnimStates
{
    IDLE,
    RUNNING
}
