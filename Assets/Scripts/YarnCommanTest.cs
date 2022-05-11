using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class YarnCommanTest : MonoBehaviour
{
    private DialogueRunner runner;
    void Start()
    {
        runner = FindObjectOfType<DialogueRunner>();
    }

    [YarnCommand]
    public void debugMessage() {
        Debug.Log("debugging");
    }
}
