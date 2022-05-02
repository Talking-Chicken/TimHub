using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using TMPro;
using NaughtyAttributes;

public class EndingManager : MonoBehaviour
{
    [SerializeField, Foldout("player attempt answer")] TextMeshProUGUI criminalAnswerText, causeAnswerText, timeAnswerText, locationAnswerText, detailAnswerText; 
    [SerializeField, BoxGroup("THE correct answer"),ResizableTextArea]string criminal, cause, time, location, detail;

    [YarnFunction("Report_Answer")]
    public int reportAnswer() {
        int percentCorrect = 0;
        if (criminalAnswerText.text.ToLower().Trim().Equals(criminal.ToLower().Trim())) percentCorrect += 20;
        if (causeAnswerText.text.ToLower().Trim().Equals(cause.ToLower().Trim())) percentCorrect += 20;
        if (timeAnswerText.text.ToLower().Trim().Equals(time.ToLower().Trim())) percentCorrect += 20;
        if (locationAnswerText.text.ToLower().Trim().Equals(location.ToLower().Trim())) percentCorrect += 20;
        if (detailAnswerText.text.ToLower().Trim().Equals(detail.ToLower().Trim())) percentCorrect += 20;
        return percentCorrect;
    }
}
