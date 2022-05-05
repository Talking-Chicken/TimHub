using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Yarn.Unity;

public class CaseReportQuestion : MonoBehaviour
{
    [SerializeField] EntryType type;
    [SerializeField] private TMP_Dropdown dropdown;
    private string currentAnswer = "";

    public EntryType Type {get => type; set => type = value;}
    public TMP_Dropdown AnswerDropdown {get => dropdown; set => dropdown = value;}
    void Start()
    {
        dropdown.onValueChanged.AddListener(delegate {
            setCurrentAnswer(dropdown.value);
        });
    }

    public void updateOptions(List<entry> entries) {
        dropdown.options.Clear();
        for (int i = 0; i < entries.Count; i++)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData() {text = entries[i].entryName});
        }
        Debug.Log("options contains " + dropdown.options.Count);
        foreach (TMP_Dropdown.OptionData option in dropdown.options) {
            if (!currentAnswer.Equals("") && option.text.ToLower().Trim().Equals(currentAnswer.ToLower().Trim()))
                dropdown.value = dropdown.options.IndexOf(option);
        }
    }

    public void setCurrentAnswer(int value) {
        currentAnswer = dropdown.options[value].text;
    }

}
