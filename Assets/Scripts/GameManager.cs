using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class GameManager : Singleton<GameManager>
{
    public static Hashtable ProgressTable = new Hashtable(); //this table records all progress that can affect player action in the future
    void Start()
    {
        InteractiveObj[] interactiveObjs = FindObjectsOfType<InteractiveObj>();
        for (int i = 0; i < interactiveObjs.Length; i++) {
            foreach(entry entryName in interactiveObjs[i].EntryList) {

                //modify entryName
                char[] nameCharacters = entryName.entryName.ToCharArray();
                for (int n = 0; n < nameCharacters.Length; n++) {
                    if (nameCharacters[n].Equals('_'))
                        nameCharacters[n] = ' ';
                }
                string newEntryName = "";
                for (int n = 0; n < nameCharacters.Length; n++) {
                    newEntryName += nameCharacters[n];
                }
                newEntryName = newEntryName.ToLower().Trim();

                ProgressTable.Add(newEntryName, false);
            }
        }
    }

    
    void Update()
    {
        
    }

    [YarnFunction("Check_Collected")]
    public static bool checkCollected(string entryName) {
        //modify entryName
        char[] nameCharacters = entryName.ToCharArray();
        for (int n = 0; n < nameCharacters.Length; n++) {
            if (nameCharacters[n].Equals('_'))
                nameCharacters[n] = ' ';
        }
        string newEntryName = "";
        for (int n = 0; n < nameCharacters.Length; n++) {
            newEntryName += nameCharacters[n];
        }
        newEntryName = newEntryName.ToLower().Trim();

        if (ProgressTable.Contains(newEntryName)) {
            return (bool)ProgressTable[newEntryName];
        }
        return false;
    }
}
