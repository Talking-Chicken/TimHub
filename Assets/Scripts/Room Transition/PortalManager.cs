using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

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
        /*
        if (Input.GetKeyDown(KeyCode.A))
            StartCoroutine(transitionAnimation());
        */
    }

    public static void teleport()
    {
        if (currentPortal != null)
        {
            for (int i = 0; i < portals.Count; i++)
            {
                if (currentPortal.equalsButNotSelf(portals[i]))
                {
                    FindObjectOfType<PlayerControl>().teleport(portals[i].transform.position);
                }
            }
        }

    }

    [YarnCommand("Transition_Anim")]
    public void transitionAnim()
    {
        StartCoroutine(transitionAnimation());
    }

    [YarnCommand("Room_Change")]
    public void roomChange()
    {
        StartCoroutine(transitionAnimation());
    }

    IEnumerator transitionAnimation()
    {
        TransitionManager.Instance.roomTransitionAnimation();
        yield return new WaitForSeconds(1.0f);
        teleport();
    }
}
