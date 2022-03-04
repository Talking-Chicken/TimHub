using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class JournalStateBase
{
    public abstract void enterState(JournalControl journal);
    public abstract void updateState(JournalControl journal);
    public abstract void leaveState(JournalControl journal);
}
