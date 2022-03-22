using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class JournalEntry : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText, descriptionText;
    [SerializeField] private Image image;

    private entry currentEntry; //the entry that currenty storing in this alibi entry 

    public entry CurrentEntry {get {return currentEntry;} set {currentEntry = value;}}
    
    /*present self with name, description and image*/
    public void drawSelf() {
        nameText.text = CurrentEntry.entryName;
        descriptionText.text = CurrentEntry.entryDes;
        this.image.sprite = CurrentEntry.entryImage;
    }
}
