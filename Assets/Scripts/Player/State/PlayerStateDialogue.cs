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
            player.StartCoroutine(player.waitToChangeState(player.stateExplore));
        if (Input.GetMouseButtonDown(0)) {
            if (player.HoveringObj == null || !player.HoveringObj.tag.Equals("Blocker")) {
                Debug.Log("changing dialogue");
                player.runner.GetComponentInChildren<LineView>().OnContinueClicked();
                player.dialogueControl.changePortrait();
            }
        }
    }

    public override void fixedUpdate(PlayerControl player)
    {
        
    }
    public override void leaveState(PlayerControl player) {
        player.previousState = this;
    }
}
