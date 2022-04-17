using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : Singleton<TransitionManager>
{
    [SerializeField] private Animator transitionAnimator;

    public void roomTransitionAnimation() {
        transitionAnimator.SetTrigger("Transit");
    }
}
