using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperIdleState : ChomperBaseState
{
    public override void EnterState(ChomperStateManager chomper)
    {
        chomper.currentSpeed = 0f;
        Animator animator = chomper.GetComponent<Animator>();
        animator.Rebind();

        animator.SetTrigger("ChomperIdle");
    }

    public override void UpdateState(ChomperStateManager chomper)
    {
        chomper.SwitchState(chomper.walk);
    }
}
