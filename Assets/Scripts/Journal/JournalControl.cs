using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[System.Serializable]
public struct entry {
    public string entryName;
    public string entryDes;
    public Sprite entryImage;
}

public class JournalControl : MonoBehaviour
{
    //gameobjects of different sections
    [SerializeField, BoxGroup("Journal Body"), Tooltip("this is used to block player mouse raycast, when clicking on journal icon")]
    private GameObject blockRaycastSquare; //the suqare that blocks mouse raycast, so player won't move when they clicked on journal icon
    [SerializeField, BoxGroup("Journal Body")] private GameObject journalBody, journalIcon;
    [SerializeField, BoxGroup("Alibi")] private GameObject alibiEntries, alibieTab;
    [SerializeField, BoxGroup("Alibi")] private AlibiControl alibiControl;
    [SerializeField, BoxGroup("Item")] private GameObject itemEntries, itemTab;
    [SerializeField, BoxGroup("Case Report")] private GameObject caseReport, caseReportTab;


    //states
    private JournalStateBase currentState;
    public JournalStateAlibis stateAlibis = new JournalStateAlibis();
    public JournalStateItems stateItems = new JournalStateItems();
    public JournalStateCaseReport stateCaseReport = new JournalStateCaseReport();

    public void changeState(JournalStateBase newState) {
        if (currentState != newState) {
            if (currentState != null)
            {
                currentState.leaveState(this);
            }

            currentState = newState;

            if (currentState != null)
            {  
                currentState.enterState(this);
            }
        }
    }

    //change state functions, used for unity event (button)
    public void changeToAlibiState() {changeState(stateAlibis);}
    public void changeToItemState() {changeState(stateItems);}
    public void changeToCaseReportState() {changeState(stateCaseReport);}


    void Start()
    {
        changeState(stateAlibis);    
    }

    
    void Update()
    {
        currentState.updateState(this);

        //happens all states
        blockRaycastSquare.transform.position = Camera.main.ScreenToWorldPoint(journalIcon.transform.position);
    }

    //open and close sections, also notice that tab will change their hierarchy in canvas
    public void openAlibi() {
        alibiEntries.SetActive(true);
        bringToTop(alibieTab, journalBody);
    }

    public void closeAlibi() {
        alibiEntries.SetActive(false);
    }    

    public void openItem() {
        itemEntries.SetActive(true);
        bringToTop(itemTab, journalBody);
    }

    public void closeItem() {
        itemEntries.SetActive(false);
    }

    public void openCaseReport() {
        caseReport.SetActive(true);
        bringToTop(caseReportTab, journalBody);
    }

    public void closeCaseReport() {
        caseReport.SetActive(false);
    }

    //bring the gameobject to the top in UI, basically change it's hierarhy to the last in the canvas
    private void bringToTop(GameObject topObject, GameObject secondToTopObject) {
        secondToTopObject.transform.SetAsLastSibling();
        topObject.transform.SetAsLastSibling();
    }
}
