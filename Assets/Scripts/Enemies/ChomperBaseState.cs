using UnityEngine;

public abstract class ChomperBaseState
{
    public abstract void EnterState(ChomperStateManager chomper);

    public abstract void UpdateState(ChomperStateManager chomper);
}
