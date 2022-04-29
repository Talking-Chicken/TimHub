using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    [SerializeField, BoxGroup("Dialogue GUI")] TextMeshProUGUI dialogueNameText; 
    [SerializeField, BoxGroup("Dialogue GUI")] Image portraitImage;
    [SerializeField, Foldout("Portraits")] 
    Sprite penneDefault, radiatorDefault, RigatoniDefault, stelleDefault, timDefault, neroDefault, orzoDefault, portraitDefault; 

    /* show portrait of the talking character, if there's one.
       if there's not, show default portrait*/
    public void changePortrait() {
        switch (dialogueNameText.text.ToLower().Trim()) {
            case "penne":
                portraitImage.sprite = penneDefault;
                break;
            case "radiator":
                portraitImage.sprite = radiatorDefault;
                break;
            case "rigatoni":
                portraitImage.sprite = RigatoniDefault;
                break;
            case "stelle":
                portraitImage.sprite = stelleDefault;
                break;
            case "tim":
                portraitImage.sprite = timDefault;
                break;
            case "nero":
                portraitImage.sprite = neroDefault;
                break;
            case "orzo":
                portraitImage.sprite = orzoDefault;
                break;
            default:
                portraitImage.sprite = portraitDefault;
                Debug.Log("default portrait used, check whether dialogue control spelled name correctly");
                break;
        }
    }
}
