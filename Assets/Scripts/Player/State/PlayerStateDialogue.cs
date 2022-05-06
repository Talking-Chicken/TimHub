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
                //player.dialogueControl.changePortrait();
                player.runner.GetComponentInChildren<LineView>().OnContinueClicked();
                //player.runner.GetComponentInChildren<LineView>().UserRequestedViewAdvancement();
            }
        }

        if (Input.GetKey(KeyCode.LeftControl))
            player.runner.GetComponentInChildren<LineView>().OnContinueClicked();
        player.dialogueControl.changePortrait();
        player.canvasGroup.alpha = 1;
    }

    public override void fixedUpdate(PlayerControl player)
    {
        
    }
    public override void leaveState(PlayerControl player) {
        player.previousState = this;
        player.canvasGroup.alpha = 0;
        if(player.TargetingDialogueNPC.name.ToLower().Trim().Contains("rigatoni"))
            player.TargetingDialogueNPC = null;
    }
}
