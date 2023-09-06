using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperWalkingState : ChomperBaseState
{
    private float direction = 0f;
    public override void EnterState(ChomperStateManager chomper)
    {
        Animator animator = chomper.GetComponent<Animator>();
        animator.Rebind();

        animator.SetTrigger("ChomperWalk");
    }

    public override void UpdateState(ChomperStateManager chomper)
    {
        if (WalkAnimationTriggered(chomper))
            chomper.RestoreSpeed();

        Vector3 position = chomper.transform.position;
        position.x += chomper.transform.forward.z * chomper.currentSpeed * Time.deltaTime;
        chomper.transform.position = position;

        BoxCollider2D enemyCollider = chomper.GetComponent<BoxCollider2D>();

        RaycastHit2D rayhit = Physics2D.Raycast(enemyCollider.bounds.center, new Vector2(chomper.transform.forward.z, -0.5f), 2.5f, LayerMask.GetMask("Platform"));

        if (!rayhit && WalkAnimationTriggered(chomper))
        {
            if (direction == 180f)
                direction = 0f;
            else
                direction = 180f;

            chomper.SwitchState(chomper.idle);
            chomper.transform.eulerAngles = new Vector3(0, direction, 0);
        }
    }

    private bool WalkAnimationTriggered(ChomperStateManager chomper)
    {
        return chomper.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Chomper_walk");
    }
}
