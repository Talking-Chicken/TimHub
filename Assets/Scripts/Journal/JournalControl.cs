using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

#region ENTRY of evidence that player collected by interacting with item or NPC
[System.Serializable]
public struct entry {
    public string entryName;
    public string entryDes;
    public Sprite entryImage;
    public EntryType entryType;
}
#endregion

//to categorize which kind of entry the entry is (item or alibi)
public enum EntryType {Item, Alibi}

public class JournalControl : MonoBehaviour
{
    //gameobjects of different sections
    [SerializeField, BoxGroup("Journal Body"), Tooltip("this is used to block player mouse raycast, when clicking on journal icon")]
    private GameObject blockRaycastSquare; //the suqare that blocks mouse raycast, so player won't move when they clicked on journal icon
    [SerializeField, BoxGroup("Journal Body")] private GameObject journalBody, journalIcon;
    [SerializeField, BoxGroup("Alibi")] private GameObject alibiEntriesContainer, alibieTab, alibiEntry;
    [SerializeField, BoxGroup("Item")] private GameObject itemEntriesContainer, itemTab, itemEntry;
    [SerializeField, BoxGroup("Case Report")] private GameObject caseReport, caseReportTab;

    //journal entries
    private List<entry> alibies = new List<entry>(), items = new List<entry>();
    private List<GameObject> alibiEntryObjects = new List<GameObject>(), itemEntryObjects = new List<GameObject>();


    #region STATES
    private JournalStateBase currentState;
    public JournalStateAlibis stateAlibis = new JournalStateAlibis();
    public JournalStateItems stateItems = new JournalStateItems();
    public JournalStateCaseReport stateCaseReport = new JournalStateCaseReport();
    public JournalStateIdle stateIdle = new JournalStateIdle();

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
    #endregion

    void Start()
    {
        changeState(stateIdle);
    }

    
    void Update()
    {
        currentState.updateState(this);

        //happens all states
        blockRaycastSquare.transform.position = Camera.main.ScreenToWorldPoint(journalIcon.transform.position);
    }

    #region open and close sections, also notice that tab will change their hierarchy in canvas
    public void openAlibi() {
        alibiEntriesContainer.SetActive(true);
        bringToTop(alibieTab, journalBody);

        //draw all alibi entries
        
    }

    public void closeAlibi() {
        alibiEntriesContainer.SetActive(false);

        //destroy all alibi entries
        
    }    

    public void openItem() {
        itemEntriesContainer.SetActive(true);
        bringToTop(itemTab, journalBody);
    }

    public void closeItem() {
        itemEntriesContainer.SetActive(false);
    }

    public void openCaseReport() {
        caseReport.SetActive(true);
        bringToTop(caseReportTab, journalBody);
    }

    public void closeCaseReport() {
        caseReport.SetActive(false);
    }
    #endregion

    //bring the gameobject to the top in UI, basically change it's hierarhy to the last in the canvas
    private void bringToTop(GameObject topObject, GameObject secondToTopObject) {
        secondToTopObject.transform.SetAsLastSibling();
        topObject.transform.SetAsLastSibling();
    }

    /*add entry to either alibi or item evidence
      for now only adds to alibi*/
    public void addEntry(entry newEntry) {
        if (newEntry.entryType == EntryType.Alibi)
            alibies.Add(newEntry);
        else if (newEntry.entryType == EntryType.Item)
            items.Add(newEntry);
    }

    /*show entries of the current state*/
    public void showEntries(JournalStateBase currentState) {
        if (currentState == stateAlibis) {
            //for now instantiate new alibi entry every time, change to object pool when we know how many entries should be one page
            for (int i = 0; i < alibies.Count; i++) {
                alibiEntryObjects.Add(Instantiate(alibiEntry, Vector2.zero, Quaternion.identity));
                alibiEntryObjects[i].transform.SetParent(alibiEntriesContainer.transform);
                
                //set information of new entry
                JournalEntry newAlibi = alibiEntryObjects[i].GetComponent<JournalEntry>();
                newAlibi.CurrentEntry = alibies[i];
                newAlibi.drawSelf();
            }
        } else if (currentState == stateItems) {
            //for now instantiate new item entry every time, change to object pool when we know how many entries should be one page
            for (int i = 0; i < alibies.Count; i++) {
                itemEntryObjects.Add(Instantiate(itemEntry, Vector2.zero, Quaternion.identity));
                itemEntryObjects[i].transform.SetParent(itemEntriesContainer.transform);
                
                //set information of new entry
                JournalEntry newAlibi = itemEntryObjects[i].GetComponent<JournalEntry>();
                newAlibi.CurrentEntry = items[i];
                newAlibi.drawSelf();
            }
        }
    }

    /*hide alibi entries
    destroy entries that been created by show entries*/
    public void hideEntries(JournalStateBase currentState) {
        if (currentState == stateAlibis) {
            for (int i = 0; i < alibiEntryObjects.Count; i++) {
                GameObject deletingEntry = alibiEntryObjects[i];
                alibiEntryObjects.RemoveAt(i);
                Destroy(deletingEntry);
            }
        } else if (currentState == stateItems) {
            for (int i = 0; i < itemEntryObjects.Count; i++) {
                GameObject deletingEntry = itemEntryObjects[i];
                itemEntryObjects.RemoveAt(i);
                Destroy(deletingEntry);
            }
        }
    }
}
