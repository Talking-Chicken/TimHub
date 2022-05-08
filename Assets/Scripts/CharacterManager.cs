using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private GameObject nero, tim, secretNero;
    private bool isTimFading = false;
    [SerializeField] private SpriteRenderer timeRenderer;
    private TransitionManager transition;
    void Start()
    {
        transition = FindObjectOfType<TransitionManager>();
    }


    void Update()
    {
        if (isTimFading)
            timeRenderer.color += new Color(0, 0, 0, 0.5f * Time.deltaTime);
        if (timeRenderer.color.a >= 255)
            isTimFading = false;
    }

    [YarnCommand("Tim_Fade_In")]
    public void timFadeIn()
    {
        isTimFading = true;
        tim.SetActive(true);
        //TODO: play tim fade in sound fx
    }

    [YarnCommand("Nero_Transition_Out")]
    public void neroTransitionIn()
    {
        transition.roomTransitionAnimation();
        StartCoroutine(waitToDeactive(nero));
    }

    [YarnCommand("Nero_In")]
    public void neroIn()
    {
        transition.roomTransitionAnimation();
        StartCoroutine(waitToActive(secretNero));
    }

    IEnumerator waitToActive(GameObject NPC)
    {
        yield return new WaitForSeconds(1.0f);
        NPC.SetActive(true);
    }

    IEnumerator waitToDeactive(GameObject NPC)
    {
        yield return new WaitForSeconds(1.0f);
        NPC.SetActive(false);
    }

    [YarnCommand("Camera_Lerp_To")]
    public void cameraLerpTo(GameObject target)
    {
        Camera.main.GetComponent<CameraMovement>().followTransform = target.transform;
    }
}
