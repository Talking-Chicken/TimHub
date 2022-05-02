using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private GameObject nero, tim;
    private bool isTimFading = false;
    private SpriteRenderer timeRenderer;
    private TransitionManager transition;
    void Start()
    {
        timeRenderer = tim.GetComponent<SpriteRenderer>();
        transition = FindObjectOfType<TransitionManager>();
    }

    
    void Update()
    {
        if (isTimFading)
            timeRenderer.color += new Color(0,0,0, 0.5f * Time.deltaTime);
        if (timeRenderer.color.a >= 255)
            isTimFading = false;
    }

    [YarnCommand("Tim_Fade_In")]
    public void timFadeIn() {
        isTimFading = true;
        tim.SetActive(true);
    }

    [YarnCommand("Nero_Transition_Out")]
    public void neroTransitionIn() {
        transition.roomTransitionAnimation();
        StartCoroutine(waitToDeactive(nero));
    }

    IEnumerator waitToDeactive(GameObject NPC) {
        yield return new WaitForSeconds(1.0f);
        NPC.SetActive(false);
    }
}
