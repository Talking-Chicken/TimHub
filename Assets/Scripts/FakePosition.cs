using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePosition : MonoBehaviour
{
    void Start()
    {
        GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
    }
}
