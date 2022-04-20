using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChurchCandle : InteractiveObj
{
    public override void interact()
    {
        GameManager.setCollected("church_candle");
        gameObject.SetActive(false);
    }
}
