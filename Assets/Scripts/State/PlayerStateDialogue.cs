using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class PlayerStateDialogue : PlayerStateBase
{
    public override void enterState(PlayerControl player) {
        
    }
    public override void updateState(PlayerControl player) {
        if (!player.runner.IsDialogueRunning)
            player.changeState(player.stateExplore);
        if (Input.GetMouseButtonUp(0))
            player.runner.GetComponentInChildren<LineView>().OnContinueClicked();
    }

    public override void fixedUpdate(PlayerControl player)
    {
        
    }
    public override void leaveState(PlayerControl player) {
        
    }
}
