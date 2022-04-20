using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdjuster : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.GetComponent<PlayerControl>() != null) {
            CameraMovement.InChurch = true;
        }
    }

    void OnTriggerExit2D (Collider2D collider) {
        if (collider.GetComponent<PlayerControl>() != null) {
            CameraMovement.InChurch = false;
        }
    }
}
