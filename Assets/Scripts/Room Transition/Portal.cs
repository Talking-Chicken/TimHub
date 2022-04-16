using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private bool isExit;
    [SerializeField] private int portalNum;
    void Start()
    {
        GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
    }

    public bool equalsButNotSelf(Portal other) {
        if (other == null) {
            Debug.LogWarning("cannot compare null portal");
            return false;
        }

        if (other != this)
            if (other.portalNum == this.portalNum)
                return true;
        return false;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.GetComponent<PlayerControl>() != null) {
            PortalManager.currentPortal = this;
        }
    }
}
