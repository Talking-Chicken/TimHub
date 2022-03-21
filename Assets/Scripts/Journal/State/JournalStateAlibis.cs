using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalStateAlibis : JournalStateBase
{
    public override void enterState(JournalControl journal) {
        journal.openAlibi();
    }
    public override void updateState(JournalControl journal) {

    }
    public override void leaveState(JournalControl journal) {
        journal.closeAlibi();
    }
}