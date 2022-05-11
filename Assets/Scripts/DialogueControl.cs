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
    Sprite penneDefault, radiatorDefault, RigatoniDefault, stelleDefault, timDefault, neroDefault, orzoDefault, portraitDefault,
           hamDugoDefault, blankieBoyDefault, mikuDefault, wisdomToothDefault, buffTimlingDefault, tapeTimlingDefault,
           mounTimlingDefault, sassyTimlingDefault, teethTimglingDefault, noThoughtTimlingDefault, fridgeBoyDefault; 

    /* show portrait of the talking character, if there's one.
       if there's not, show default portrait*/
    public void changePortrait() {
        switch (dialogueNameText.text.ToLower().Trim()) {
            case "penne":
                portraitImage.sprite = penneDefault;
                break;
            case "radiatore":
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
            case "ham dugo":
                portraitImage.sprite = hamDugoDefault;
                break;
            case "hatsune miku":
                portraitImage.sprite = mikuDefault;
                break;
            case "blankie boy":
                portraitImage.sprite = blankieBoyDefault;
                break;
            case "wisdom tooth":
                portraitImage.sprite = wisdomToothDefault;
                break;
            case "buff timling":
                portraitImage.sprite = buffTimlingDefault;
                break;
            case "tape timling":
                portraitImage.sprite = tapeTimlingDefault;
                break;
            case "mountimdew timling":
                portraitImage.sprite = mounTimlingDefault;
                break;
            case "sassy timling":
                portraitImage.sprite = sassyTimlingDefault;
                break;
            case "teeth timling":
                portraitImage.sprite = teethTimglingDefault;
                break;
            case "no thoughts timling":
                portraitImage.sprite = noThoughtTimlingDefault;
                break;
            case "fridge gremlin":
                portraitImage.sprite = fridgeBoyDefault;
                break;
            default:
                portraitImage.sprite = portraitDefault;
                Debug.Log("default portrait used, check whether dialogue control spelled name correctly");
                break;
        }
    }
}
