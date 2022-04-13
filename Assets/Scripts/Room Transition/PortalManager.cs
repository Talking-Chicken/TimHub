using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    private static List<Portal> portals = new List<Portal>();
    public static Portal currentPortal;
    void Start()
    {
        portals.AddRange(FindObjectsOfType<Portal>());
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            teleport();
    }

    public static bool teleport() {
        if (currentPortal != null) {
            for (int i = 0; i < portals.Count; i++)
            {
                if (currentPortal.equalsButNotSelf(portals[i])) {
                    FindObjectOfType<PlayerControl>().transform.position = portals[i].transform.position;
                }
            }
        }
        return false;
    }
}
