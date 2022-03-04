using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalStateCaseReport : JournalStateBase
{
    public override void enterState(JournalControl journal) {
        journal.openCaseReport();
    }
    public override void updateState(JournalControl journal) {

    }
    public override void leaveState(JournalControl journal) {
        journal.closeCaseReport();
    }
}
