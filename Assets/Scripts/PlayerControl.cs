using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Vector2 destination;
    void Start()
    {
        destination = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            destination = Input.mousePosition;
        }

        if (Vector2.Distance(transform.position, destination) > 0.1f)
            moveTo(Camera.main.ScreenToWorldPoint(destination));
    }

    public void moveTo(Vector2 destination) {
        transform.position = Vector2.Lerp(transform.position, destination, 0.1f);
    }
}
