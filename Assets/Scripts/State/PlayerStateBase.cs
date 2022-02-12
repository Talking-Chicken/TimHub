using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerStateBase
{
    public abstract void enterState(PlayerControl player);
    public abstract void updateState(PlayerControl player);
    public abstract void leaveState(PlayerControl player);
}
