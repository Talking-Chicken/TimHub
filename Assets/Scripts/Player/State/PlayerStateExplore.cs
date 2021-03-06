using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateExplore : PlayerStateBase
{
    public override void enterState(PlayerControl player){
        player.blurCamera.SetActive(false);
        player.CanInvestigate = true;
    }
    public override void updateState(PlayerControl player){
        InteractiveObj interactingObj = null;
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (Input.GetMouseButtonDown(player.PrimaryMouseBuotton) || Input.GetMouseButton(player.PrimaryMouseBuotton)) {
            if (hit.collider == null || (!hit.collider.tag.Equals("Blocker") && hit.collider.GetComponent<InteractiveObj>() == null))
                if (player.TargetingDialogueNPC == null || !player.TargetingDialogueNPC.name.ToLower().Trim().Contains("rigatoni")) {
                    player.Destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    player.IsForcedToMove = false;
                }
        }

        if (!player.IsForcedToMove) {
            if (Input.GetMouseButtonDown(player.SecondaryMouseButton)) {
                if (hit.collider != null) {
                    if (hit.collider.GetComponentInParent<InteractiveObj>() != null) {
                        interactingObj = hit.collider.GetComponentInParent<InteractiveObj>();
                    }
                }
            }

            //start dialogue first, then interact
            if (interactingObj != null) {
                if (interactingObj.IsInteractFirst) {
                    interactingObj.interact();
                    player.moveAndTalkTo(interactingObj.name);
                    //interactingObj.talk();
                }
                else {
                    if (interactingObj.IsTalkable)
                        player.moveAndTalkTo(interactingObj.name);
                        //interactingObj.talk();
                    if (interactingObj.IsInteractable)
                        interactingObj.interact();
                }    
            }
        } else if (player.TargetingDialogueNPC != null){
            player.moveAndTalkTo(player.TargetingDialogueNPC);
        }
    }

    public override void fixedUpdate(PlayerControl player) {
        //move
        if (Vector2.Distance(player.transform.position, player.Destination) > 0.1f)
            player.moveTo(player.Destination);
    }
    public override void leaveState(PlayerControl player){
        player.CanInvestigate = false;

        //stop player moving
        player.Destination = player.transform.position;
        player.blurCamera.SetActive(true);
        
        player.previousState = this;
    }
}
