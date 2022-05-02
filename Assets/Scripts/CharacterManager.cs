using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private GameObject nero, tim;
    private bool isTimFading = false;
    private SpriteRenderer timeRenderer;
    void Start()
    {
        timeRenderer = tim.GetComponent<SpriteRenderer>();
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
}
