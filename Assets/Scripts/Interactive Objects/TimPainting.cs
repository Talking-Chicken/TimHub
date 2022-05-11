using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class TimPainting : InteractiveObj
{
    [SerializeField] private SpriteRenderer paintingSprite;
    [SerializeField] private Sprite hole;
    [YarnCommand("Burn_Painting")]
    public override void interact()
    {
        paintingSprite.sprite = hole;
        Debug.Log("burned");
    }
}
