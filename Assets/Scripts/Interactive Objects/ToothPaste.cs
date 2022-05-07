using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ToothPaste : InteractiveObj
{
    [YarnCommand("Collect_ToothPaste")]
    public override void interact()
    {
        gameObject.SetActive(false);
    }
}
