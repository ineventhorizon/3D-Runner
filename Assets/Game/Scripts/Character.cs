using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] protected Rigidbody rb;
    public Vector3 MoveDirection;
    private bool triggered = false;

    private void OnEnable()
    {
        Observer.CharactersAnimState += UpdateAnimState;
    }
    private void OnDisable()
    {
        Observer.CharactersAnimState -= UpdateAnimState;
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
    public virtual void HandleObstacleHit()
    {
        if (triggered) return;
        triggered = true;
        //Handles when characters hits to obstacle
    }
}

public enum CharacterAnimState
{
    IDLE,
    RUNNING
}
