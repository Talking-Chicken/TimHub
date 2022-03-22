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
    [SerializeField, BoxGroup("Alibi")] private List<GameObject> alibiEntryObjects = new List<GameObject>();
    [SerializeField, BoxGroup("Item")] private List<GameObject> itemEntryObjects = new List<GameObject>();

    //general jouranl variable
    private int page = 1, maxPage = 1; //maxPage will alter in different journal state


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

    /*add entry to either alibi or item evidence which haven't been added
      for now only adds to alibi*/
    public void addEntry(entry newEntry) {
        if (newEntry.entryType == EntryType.Alibi && !containsEntry(newEntry.entryName, alibies))
            alibies.Add(newEntry);
        else if (newEntry.entryType == EntryType.Item && !containsEntry(newEntry.entryName, items))
            items.Add(newEntry);
    }

    /*show entries of the current state*/
    public void showEntries(JournalStateBase currentState) {
        if (currentState == stateAlibis) {
            //set max page
            if (alibies.Count/alibiEntryObjects.Count > 1) {
                if (alibies.Count%alibiEntryObjects.Count > 0)
                    maxPage = alibies.Count/alibiEntryObjects.Count+1;
                else
                    maxPage = alibies.Count/alibiEntryObjects.Count;
            }
        
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
            for (int i = 0; i < items.Count; i++) {
                itemEntryObjects.Add(Instantiate(itemEntry, Vector2.zero, Quaternion.identity));
                itemEntryObjects[i].transform.SetParent(itemEntriesContainer.transform);
                
                //set information of new entry
                JournalEntry newItem = itemEntryObjects[i].GetComponent<JournalEntry>();
                newItem.CurrentEntry = items[i];
                newItem.drawSelf();
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

    //return whether entryList contains a entry that equals to the entryName
    public bool containsEntry(string entryName, List<entry> entryList) {
        for (int i = 0; i < entryList.Count;i++) {
            if (entryList[i].entryName.ToLower().Trim().Equals(entryName.ToLower().Trim()))
                return true;
        }
        return false;
    }

    /*flip to next page if there's any*/
    public void nextPage() {
        page = Mathf.Min(maxPage, page+1);
    }

    /*flip to previous page if there's any*/
    public void previousPage() {
        page = Mathf.Max(1, page-1);
    }
}
