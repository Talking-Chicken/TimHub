using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private GameObject nero, tim, secretNero, rigatoni;
    private bool isTimFading = false;
    [SerializeField] private SpriteRenderer timRenderer;
    private TransitionManager transition;
    [SerializeField] Vector2 candlePosition;
    void Start()
    {
        transition = FindObjectOfType<TransitionManager>();
    }


    void Update()
    {
        if (isTimFading)
            timRenderer.color += new Color(0, 0, 0, 0.5f * Time.deltaTime);
        if (timRenderer.color.a >= 255)
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

    [YarnCommand("Nero_Death")]
    public void neroDeath()
    {
        StartCoroutine(waitToDeactive(secretNero));
    }

    [YarnCommand("Nero_In")]
    public void neroIn()
    {
        transition.roomTransitionAnimation();
        StartCoroutine(waitToActive(secretNero));
    }

    [YarnCommand("Rigatoni_Move_To_Candle")]
    public void rigatoniMoveToCandle()
    {
        rigatoni.transform.position = candlePosition;
    }

    [YarnCommand("Rigatoni_Move_Out")]
    public void rigatoniMoveOut()
    {
        rigatoni.SetActive(false);
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
