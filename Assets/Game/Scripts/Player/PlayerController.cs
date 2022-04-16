using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //TODO
    //There are similar functions for player and opponent they can be combined under a base class. 
    [SerializeField] private Camera mainCamReference;
    [SerializeField] private LayerMask platformMask;
    [SerializeField] private Animator animator;
    [SerializeField] private float speed;
    [SerializeField] private Transform cameraFollow ,sideMovementRoot, player;
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
        Observer.OpponentsAnimState += UpdateAnimState;
    }
    private void OnDisable()
    {
        Observer.PlayerObstacleHit -= HandleObstacleHit;
        Observer.OpponentsAnimState -= UpdateAnimState;
    }
    private void HandleMovement()
    {
        //TODO XY MOVEMENT
        //MODULAR FOR CHARACTER AI
        //TODO RAYCAST CODE NEEDS TO BE MORE ORGANIZED, NEED TO GET MAINCAM REFERENCE   
        
        if (InputManager.Instance.MouseClicking)
        {
            RayHitMousePoint();
            UpdateAnimState(CharacterAnimState.RUNNING);
        }
        else
        {
            UpdateAnimState(CharacterAnimState.IDLE);
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

        player.position += (sideMovementRoot.forward) * (speed * Time.deltaTime);
        cameraPosition = player.position;
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
    private void UpdateAnimState(CharacterAnimState state)
    {
        switch (state)
        {
            case CharacterAnimState.IDLE:
                animator.SetBool("Running", false);
                break;
            case CharacterAnimState.RUNNING:
                animator.SetBool("Running", true);
                break;
            default:
                break;
        }
    }
    private void HandleObstacleHit()
    {
        //Restarts the game
        GameManager.Instance.RestartGame();
    }
}

public enum CharacterAnimState
{
    IDLE,
    RUNNING
}
