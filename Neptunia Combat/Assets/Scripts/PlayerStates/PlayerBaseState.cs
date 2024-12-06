using UnityEngine;

public abstract class PlayerBaseState
{
    public abstract void EnterState(PlayerController playerController);
    public abstract void UpdateState(PlayerController playerController);
    public abstract void ExitState(PlayerController playerController);
}
