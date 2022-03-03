using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateJournal : PlayerStateBase
{
    public override void enterState(PlayerControl player)
    {
        player.openJournal();
        player.blurCamera.SetActive(true);
    }

    public override void updateState(PlayerControl player)
    {
        
    }

    public override void fixedUpdate(PlayerControl player)
    {
        
    }

    public override void leaveState(PlayerControl player)
    {
        player.closeJournal();
        player.blurCamera.SetActive(false);
    }
}
