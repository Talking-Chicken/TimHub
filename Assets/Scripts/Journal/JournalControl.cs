using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using TMPro;
using Yarn.Unity;
using UnityEngine.UI;

#region ENTRY of evidence that player collected by interacting with item or NPC
[System.Serializable]
public struct entry
{
    public string entryName;
    [ResizableTextArea] public string entryDes;
    public Sprite entryImage;
    public EntryType entryType;

    public entry(string entryName, string entryDes, Sprite entryImage, EntryType entryType)
    {
        this.entryName = entryName;
        this.entryDes = entryDes;
        this.entryImage = entryImage;
        this.entryType = entryType;
    }
}
#endregion

//to categorize which kind of entry the entry is (item or alibi)
public enum EntryType { Item, Alibi, Custom }

public class JournalControl : MonoBehaviour
{
    //gameobjects of different sections
    [SerializeField, BoxGroup("Journal Body"), Tooltip("this is used to block player mouse raycast, when clicking on journal icon")]
    private GameObject blockRaycastSquareForJournal, blockRaycastSquareForExit; //the suqare that blocks mouse raycast, so player won't move when they clicked on journal icon
    [SerializeField, BoxGroup("Journal Body")] private GameObject journalBody, journalIcon, exitIcon;
    [SerializeField, BoxGroup("Alibi")] private GameObject alibiEntriesContainer, alibieTab;
    [SerializeField, BoxGroup("Item")] private GameObject itemEntriesContainer, itemTab;
    [SerializeField, BoxGroup("Case Report")] private GameObject caseReport, caseReportTab;
    [SerializeField, BoxGroup("Case Brief")] private GameObject caseBrief, caseBriefTab;

    //journal entries
    private List<entry> alibies = new List<entry>(), items = new List<entry>();
    private List<GameObject> alibiEntryObjects = new List<GameObject>();
    private List<GameObject> itemEntryObjects = new List<GameObject>();

    //general jouranl variable
    private int page = 1, maxPage = 1; //maxPage will alter in different journal state
    [SerializeField, BoxGroup("General")] private TextMeshProUGUI pageText;
    [SerializeField, BoxGroup("Case Report")] private List<CaseReportQuestion> questions;

    #region STATES
    private JournalStateBase currentState;
    public JournalStateAlibis stateAlibis = new JournalStateAlibis();
    public JournalStateItems stateItems = new JournalStateItems();
    public JournalStateCaseReport stateCaseReport = new JournalStateCaseReport();
    public JournalStateCaseBrief stateCaseBrief = new JournalStateCaseBrief();
    public JournalStateIdle stateIdle = new JournalStateIdle();

    public void changeState(JournalStateBase newState)
    {
        if (currentState != newState)
        {
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
    public void changeToAlibiState() { changeState(stateAlibis); }
    public void changeToItemState() { changeState(stateItems); }
    public void changeToCaseReportState() { changeState(stateCaseReport); }
    public void changeToCaseBriefState() { changeState(stateCaseBrief); }
    #endregion

    void Start()
    {
        changeState(stateIdle);

        //set entry objects
        JournalEntry[] entryArray = itemEntriesContainer.GetComponentsInChildren<JournalEntry>(true);
        for (int i = 0; i < entryArray.Length; i++)
        {
            itemEntryObjects.Add(entryArray[i].gameObject);
        }
        entryArray = alibiEntriesContainer.GetComponentsInChildren<JournalEntry>(true);
        for (int i = 0; i < entryArray.Length; i++)
        {
            alibiEntryObjects.Add(entryArray[i].gameObject);
        }
        Canvas.ForceUpdateCanvases();
    }


    void Update()
    {
        currentState.updateState(this);

        //happens all states
        blockRaycastSquareForJournal.transform.position = Camera.main.ScreenToWorldPoint(journalIcon.transform.position);
        blockRaycastSquareForExit.transform.position = Camera.main.ScreenToWorldPoint(exitIcon.transform.position);
        pageText.text = page + "/" + maxPage;
    }

    #region open and close sections, also notice that tab will change their hierarchy in canvas
    public void openAlibi()
    {
        alibiEntriesContainer.SetActive(true);
        bringToTop(alibieTab, journalBody);
    }

    public void closeAlibi()
    {
        alibiEntriesContainer.SetActive(false);
        //destroy all alibi entries
    }

    public void openItem()
    {
        itemEntriesContainer.SetActive(true);
        bringToTop(itemTab, journalBody);
    }

    public void closeItem()
    {
        itemEntriesContainer.SetActive(false);
    }

    public void openCaseReport()
    {
        caseReport.SetActive(true);
        bringToTop(caseReportTab, journalBody);
    }

    public void closeCaseReport()
    {
        caseReport.SetActive(false);
    }

    public void openCaseBrief()
    {
        caseBrief.SetActive(true);
        bringToTop(caseBriefTab, journalBody);
    }

    IEnumerator resetObj(GameObject obj)
    {
        obj.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        obj.SetActive(true);
        LayoutRebuilder.ForceRebuildLayoutImmediate(obj.GetComponent<RectTransform>());
    }

    public void closeCaseBrief()
    {
        caseBrief.SetActive(false);
    }
    #endregion

    /*reset page number to 1*/
    public void resetPageNum()
    {
        page = 1;
    }

    //bring the gameobject to the top in UI, basically change it's hierarhy to the last in the canvas
    private void bringToTop(GameObject topObject, GameObject secondToTopObject)
    {
        secondToTopObject.transform.SetAsLastSibling();
        topObject.transform.SetAsLastSibling();
    }

    /*add entry to either alibi or item evidence which haven't been added
      for now only adds to alibi*/
    public void addEntry(entry newEntry)
    {
        if (newEntry.entryType == EntryType.Alibi && !containsEntry(newEntry.entryName, alibies))
            alibies.Add(newEntry);
        else if (newEntry.entryType == EntryType.Item && !containsEntry(newEntry.entryName, items))
            items.Add(newEntry);
    }

    /* add entry to either alibi or item evidence
       the entry is from the list, if no int parameter, default will be 0
       if a entry with the same name is already in there, this function will return false*/
    public bool addEntry(List<entry> entryList, string entryName)
    {
        //throw exception
        if (entryList == null)
        {
            Debug.LogWarning("entry list is null");
            return false;
        }
        if (entryList.Count <= 0)
        {
            return false;
        }

        for (int i = 0; i < entryList.Count; i++)
        {
            if (entryList[i].entryName.ToLower().Trim().Equals(entryName.ToLower().Trim()))
            {
                entry newEntry = entryList[i];

                //decides where to put the entry (alibi or item)
                if (newEntry.entryType == EntryType.Alibi && !containsEntry(newEntry.entryName, alibies))
                {
                    alibies.Add(newEntry);
                    return true;
                }
                else if (newEntry.entryType == EntryType.Item && !containsEntry(newEntry.entryName, items))
                {
                    items.Add(newEntry);
                    return true;
                }
            }
        }
        return false;
    }


    [YarnCommand("Add_Entry")]
    /* add entry to journal based on NPC's name and entry name
        since Yarn spinner cannot have [space] in parameter, this entry name should substitute [space] to [_]*/
    public void addEntry(string entryName)
    {
        InteractiveObj[] NPCs = FindObjectsOfType<InteractiveObj>();

        //modify entryName
        char[] nameCharacters = entryName.ToCharArray();
        for (int i = 0; i < nameCharacters.Length; i++)
        {
            if (nameCharacters[i].Equals('_'))
                nameCharacters[i] = ' ';
        }
        string newEntryName = "";
        for (int i = 0; i < nameCharacters.Length; i++)
        {
            newEntryName += nameCharacters[i];
        }

        //search through all interactive objects and check if we can add the entry from them
        foreach (InteractiveObj character in NPCs)
        {
            if (addEntry(character.EntryList, newEntryName))
            {
                //if not contains this name, create a key in hashtable of GameManager
                if (!GameManager.ProgressTable.ContainsKey(newEntryName.ToLower().Trim()))
                {
                    GameManager.ProgressTable.Add(newEntryName.ToLower().Trim(), true);
                }
                else
                {
                    GameManager.ProgressTable[newEntryName.ToLower().Trim()] = true;
                    return;
                }
            }
        }

        Debug.LogWarning("add entry false, can't find entry: " + newEntryName);

    }

    /*show entries of the current state*/
    public void showEntries(JournalStateBase currentState)
    {
        if (currentState == stateAlibis && alibies.Count > 0)
        {
            //set max page
            if (alibies.Count / alibiEntryObjects.Count > 0)
            {
                if (alibies.Count % alibiEntryObjects.Count > 0)
                {
                    maxPage = alibies.Count / alibiEntryObjects.Count + 1;
                }
                else
                    maxPage = alibies.Count / alibiEntryObjects.Count;
            }
            else
                maxPage = 1;

            //active alibi entry and draw them based ob which page we are on
            for (int i = 0; i < Mathf.Min(alibiEntryObjects.Count, alibies.Count); i++)
            {
                alibiEntryObjects[i].SetActive(true);

                //set information of new entry
                JournalEntry newAlibi = alibiEntryObjects[i].GetComponent<JournalEntry>();

                int startIndex = (page - 1) * alibiEntryObjects.Count + i; //decide from which alibi to draw, in the journal
                if (startIndex <= alibies.Count - 1)
                {
                    newAlibi.CurrentEntry = alibies[startIndex];
                    newAlibi.drawSelf();
                }
                else
                {
                    newAlibi.gameObject.SetActive(false);
                }
            }
        }
        else if (currentState == stateItems && items.Count > 0)
        {
            //set max page
            if (items.Count / itemEntryObjects.Count > 0)
            {
                if (items.Count % itemEntryObjects.Count > 0)
                    maxPage = items.Count / itemEntryObjects.Count + 1;
                else
                    maxPage = items.Count / itemEntryObjects.Count;
            }
            else
                maxPage = 1;

            //active item entry and draw them based on which page we are on
            for (int i = 0; i < Mathf.Min(itemEntryObjects.Count, items.Count); i++)
            {
                itemEntryObjects[i].SetActive(true);

                //set information of new entry
                JournalEntry newItem = itemEntryObjects[i].GetComponent<JournalEntry>();

                int startIndex = (page - 1) * itemEntryObjects.Count + i; //decide from which item to draw, in the journal
                if (startIndex <= items.Count - 1)
                {
                    newItem.CurrentEntry = items[startIndex];
                    newItem.drawSelf();
                }
                else
                {
                    newItem.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            //when there's nothing collected, set the max page to 1
            maxPage = 1;
        }
    }

    /*hide alibi entries
    destroy entries that been created by show entries*/
    public void hideEntries(JournalStateBase currentState)
    {
        if (currentState == stateAlibis)
        {
            for (int i = 0; i < alibiEntryObjects.Count; i++)
            {
                alibiEntryObjects[i].SetActive(false);
            }
        }
        else if (currentState == stateItems)
        {
            for (int i = 0; i < itemEntryObjects.Count; i++)
            {
                itemEntryObjects[i].SetActive(false);
            }
        }
    }

    //return whether entryList contains a entry that equals to the entryName
    public bool containsEntry(string entryName, List<entry> entryList)
    {
        for (int i = 0; i < entryList.Count; i++)
        {
            if (entryList[i].entryName.ToLower().Trim().Equals(entryName.ToLower().Trim()))
                return true;
        }
        return false;
    }

    /*flip to next page if there's any*/
    public void nextPage()
    {
        page = Mathf.Min(maxPage, page + 1);
        showEntries(currentState);
    }

    /*flip to previous page if there's any*/
    public void previousPage()
    {
        page = Mathf.Max(1, page - 1);
        showEntries(currentState);
    }

    /* set case report questions to have corresponding questions in their list*/
    public void setQuestions()
    {
        for (int i = 0; i < questions.Count; i++)
        {
            switch (questions[i].Type)
            {
                case EntryType.Item:
                    questions[i].updateOptions(items);
                    break;
                case EntryType.Alibi:
                    questions[i].updateOptions(alibies);
                    break;
            }
        }
    }


    [YarnCommand("Add_Answer")]
    public void addAnswer(string dropdownName, string answer)
    {
        foreach (CaseReportQuestion question in questions)
        {
            if (question.name.ToLower().Trim().Equals(dropdownName.ToLower().Trim()))
                question.AnswerDropdown.options.Add(new TMP_Dropdown.OptionData() { text = answer });
        }
    }
}
