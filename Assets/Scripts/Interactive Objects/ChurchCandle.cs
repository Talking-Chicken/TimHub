using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChurchCandle : InteractiveObj
{
    public override void interact()
    {
        GameManager.setCollected("haveCandle");
        gameObject.SetActive(false);
    }
}
