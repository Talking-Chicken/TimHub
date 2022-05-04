using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatePause : PlayerStateBase
{
    public override void enterState(PlayerControl player)
    {
        player.openPauseScreen();
    }

    public override void updateState(PlayerControl player)
    {
        if (Input.GetKeyDown(KeyCode.P))
            player.changeToPreviousState();
        
    }

    public override void fixedUpdate(PlayerControl player)
    {
        
    }

    public override void leaveState(PlayerControl player)
    {
        player.closePauseScreen();
    }
}
