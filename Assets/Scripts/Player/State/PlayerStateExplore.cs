using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateExplore : PlayerStateBase
{
    public override void enterState(PlayerControl player){
        player.blurCamera.SetActive(false);
    }
    public override void updateState(PlayerControl player){
        InteractiveObj interactingObj = null;

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null) {
                Debug.Log("hit something");
                if (hit.collider.GetComponentInParent<InteractiveObj>() != null) {
                    Debug.Log("hit interactive object");
                    interactingObj = hit.collider.GetComponentInParent<InteractiveObj>();
                }
            }
            else 
                player.Destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        //start dialogue first, then interact
        if (interactingObj != null) {
            //for test first, add its entry to journal
            player.Journal.addEntry(interactingObj.Entry, EntryType.Alibi);

            if (interactingObj.IsInteractFirst)
                interactingObj.interact();
            else {
                if (interactingObj.IsTalkable)
                    interactingObj.talk();
                else if (interactingObj.IsInteractable)
                    interactingObj.interact();
            }    
        }
    }

    public override void fixedUpdate(PlayerControl player) {
        //move
        if (Vector2.Distance(player.transform.position, player.Destination) > 0.1f)
            player.moveTo(player.Destination);
    }
    public override void leaveState(PlayerControl player){
        //stop player moving
        player.Destination = player.transform.position;
        player.blurCamera.SetActive(true);
    }
}
