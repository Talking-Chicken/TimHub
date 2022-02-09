using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) {
            TestObject g = FindObjectOfType<TestObject>();
            if (g.GetComponent<IInteractable>() != null) {
                g.interact();
            }
        }
    }
}
