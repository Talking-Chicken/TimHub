using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalStateItems : JournalStateBase
{
    public override void enterState(JournalControl journal) {
        journal.openItem();
    }
    public override void updateState(JournalControl journal) {

    }
    public override void leaveState(JournalControl journal) {
        journal.closeItem();
    }
}
