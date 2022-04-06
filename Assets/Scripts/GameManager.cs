using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class GameManager : Singleton<GameManager>
{
    public static Hashtable ProgressTable = new Hashtable(); //this table records all progress that can affect player action in the future
    void Start()
    {
        
        //for rigatoni progress check
        ProgressTable.Add("rigatoni introread", false);
        ProgressTable.Add("rigatoni investigationread", false);
        ProgressTable.Add("rigatoni forgetfulnessread", false);

        ProgressTable.Add("accessToBodega", false);
        ProgressTable.Add("accessToChurch", false);
        ProgressTable.Add("accessToSecretRoom", false);

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

    [YarnFunction("Check_Read")]
    public static bool checkRead(string nodeName) {
        return checkCollected(nodeName);
    }

    [YarnCommand("Set_Read")]
    public static void setRead(string nodeName) {
        //modify entryName
        char[] nameCharacters = nodeName.ToCharArray();
        for (int n = 0; n < nameCharacters.Length; n++) {
            if (nameCharacters[n].Equals('_'))
                nameCharacters[n] = ' ';
        }
        string newNodeName = "";
        for (int n = 0; n < nameCharacters.Length; n++) {
            newNodeName += nameCharacters[n];
        }
        newNodeName = newNodeName.ToLower().Trim();

        if (ProgressTable.ContainsKey(newNodeName))
            ProgressTable[newNodeName] = true;
        else
            Debug.LogWarning("can't find node: " + newNodeName);
    }
}
