using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalStateCaseBrief : JournalStateBase
{
    public override void enterState(JournalControl journal) {
        journal.openCaseBrief();
    }
    public override void updateState(JournalControl journal) {

    }
    public override void leaveState(JournalControl journal) {
        journal.closeCaseBrief();
    }
}
