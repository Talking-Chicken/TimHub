using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Timling : InteractiveObj
{
    [YarnCommand("Collect_Timling")]
    public override void interact()
    {
        gameObject.SetActive(false);
    }
}
