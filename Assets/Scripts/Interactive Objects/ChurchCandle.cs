using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ChurchCandle : InteractiveObj
{
    [YarnCommand("Pick_Up_Candle")]
    public override void interact()
    {
        GameManager.setCollected("church_candle");
        gameObject.SetActive(false);
    }
}
