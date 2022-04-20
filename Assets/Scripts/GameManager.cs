using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class GameManager : Singleton<GameManager>
{
    public static Hashtable ProgressTable = new Hashtable(); //this table records all progress that can affect player action in the future
    void Start()
    {
        ProgressTable.Add("accessToBodega", false);
        ProgressTable.Add("accessToChurch", false);
        ProgressTable.Add("accessToSecretRoom", false);

        InteractiveObj[] interactiveObjs = FindObjectsOfType<InteractiveObj>();
        for (int i = 0; i < interactiveObjs.Length; i++) {
            foreach(entry entryName in interactiveObjs[i].EntryList) {
                if (!entryName.entryName.Equals("")) {
                    string newEntryName = substituteUnderscoreWithSpace(entryName.entryName).ToLower().Trim();

                    ProgressTable.Add(newEntryName, false);
                }
            }
        }
    }

    /* return value in progressTable with the key : entryName
       if can't find the key, create one with the nodeName and set it to false*/
    [YarnFunction("Check_Collected")]
    public static bool checkCollected(string entryName) {
        string newEntryName = substituteUnderscoreWithSpace(entryName).ToLower().Trim();

        if (ProgressTable.Contains(newEntryName)) {
            return (bool)ProgressTable[newEntryName];
        } else {
            ProgressTable.Add(newEntryName, false);
            Debug.LogWarning("can't find " + newEntryName + ", but created one for u :)");
            return (bool)ProgressTable[newEntryName];
        }
    }

    [YarnFunction("Check_Read")]
    public static bool checkRead(string nodeName) {
        return checkCollected(nodeName);
    }

    /* set value in progressTable with the key : nodeName to true
       if can't find the key, create one with the nodeName and set it to true*/ 
    [YarnCommand("Set_Read")]
    public static void setRead(string nodeName) {
        string newNodeName = substituteUnderscoreWithSpace(nodeName).ToLower().Trim();

        if (ProgressTable.ContainsKey(newNodeName))
            ProgressTable[newNodeName] = true;
        else {
            ProgressTable.Add(newNodeName, true);
            Debug.LogWarning("can't find " + newNodeName +", but created one for u :)");
        }
    }

    public static void setCollected(string key) {
        setRead(key);
    }

    public static string substituteUnderscoreWithSpace(string sentenceToModify) {
        if (sentenceToModify != null) {
            char[] characters = sentenceToModify.ToCharArray();
            for (int n = 0; n < characters.Length; n++) {
                if (characters[n].Equals('_'))
                    characters[n] = ' ';
            }
            string newSentence = "";
            for (int n = 0; n < characters.Length; n++) {
                newSentence += characters[n];
            }
            return newSentence;
        }
        return "";
    }
}
