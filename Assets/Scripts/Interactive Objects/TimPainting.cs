using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class TimPainting : InteractiveObj
{
    [YarnCommand("Burn_Painting")]
    public override void interact()
    {
        Debug.Log("burned");
    }
}
