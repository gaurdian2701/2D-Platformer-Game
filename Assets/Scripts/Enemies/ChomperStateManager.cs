using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperStateManager : MonoBehaviour
{
    ChomperBaseState currentState;

    public ChomperIdleState idle = new ChomperIdleState();
    public ChomperWalkingState walk = new ChomperWalkingState();
    public ChomperAttackState attack = new ChomperAttackState();

    private BoxCollider2D enemyCollider;
    private float moveSpeed = 2f;

    public float currentSpeed;

    private void Start()
    {
        currentState = idle;
        currentSpeed = moveSpeed;
        currentState.EnterState(this);
        enemyCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(ChomperBaseState chomper)
    {
        currentState = chomper;
        currentState.EnterState(this);
    }

    public void RestoreSpeed() => currentSpeed = moveSpeed;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        RaycastHit2D rayhit = Physics2D.Raycast(enemyCollider.bounds.center, new Vector2(transform.forward.z, -0.5f), 2.5f, LayerMask.GetMask("Platform"));

        if(rayhit)
            Gizmos.color = Color.red;

        Gizmos.DrawRay(enemyCollider.bounds.center, new Vector2(transform.forward.z, -0.5f));
    }
}
