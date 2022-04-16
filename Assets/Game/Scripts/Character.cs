using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;
    //public Vector3 MoveDirection;

    private void OnEnable()
    {
        Observer.OpponentsAnimState += UpdateAnimState;
    }
    private void OnDisable()
    {
        Observer.OpponentsAnimState -= UpdateAnimState;
    }
    protected void UpdateAnimState(CharacterAnimState state)
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
    public virtual void EnableRigidbody()
    {
        rb.isKinematic = false;
    }

    public virtual void DisableRigidbody()
    {
        rb.isKinematic = true;
    }
    public virtual void HandleObstacleHit()
    {
        //Handles when characters hits to obstacle
    }
}

public enum CharacterAnimState
{
    IDLE,
    RUNNING
}
