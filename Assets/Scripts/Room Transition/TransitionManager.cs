using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;

public class TransitionManager : Singleton<TransitionManager>
{
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private Image blackScreen;
    private bool isBlackingOut = false;

    private void Update() {
        if (isBlackingOut) {
            blackScreen.color = new Color(0,0,0, blackScreen.color.a + 1 * Time.deltaTime);
            if (blackScreen.color.a >= 255)
                isBlackingOut = false;
        }
    }

    public void roomTransitionAnimation() {
        transitionAnimator.SetTrigger("Transit");
    }

    [YarnCommand("Black_Out_Screen")]
    public void blackOutScreen() {
        isBlackingOut = true;
    }
}
