using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using TMPro;
using NaughtyAttributes;

public class EndingManager : MonoBehaviour
{
    [SerializeField, Foldout("player attempt answer")] TMP_Dropdown criminalAnswerText, causeAnswerText, timeAnswerText, locationAnswerText, detailAnswerText; 
    [SerializeField, BoxGroup("THE correct answer"),ResizableTextArea]string criminal, cause, time, location, detail;
    public static int answerCorrectness = 0;

    [YarnFunction("Report_Answer")]
    public static int reportAnswer() {
        return answerCorrectness;
    }

    public void checkAnswer() {
        int percentCorrect = 0;

        if (criminalAnswerText.value > 0)
            if (criminalAnswerText.options[criminalAnswerText.value].text.ToLower().Trim().Equals(criminal.ToLower().Trim())) percentCorrect += 20;

        if (causeAnswerText.value > 0)
            if (causeAnswerText.options[causeAnswerText.value].text.ToLower().Trim().Equals(cause.ToLower().Trim())) percentCorrect += 20;

        if (timeAnswerText.value > 0)
            if (timeAnswerText.options[timeAnswerText.value].text.ToLower().Trim().Equals(time.ToLower().Trim())) percentCorrect += 20;

        if (locationAnswerText.value > 0)
            if (locationAnswerText.options[locationAnswerText.value].text.ToLower().Trim().Equals(location.ToLower().Trim())) percentCorrect += 20;

        if (detailAnswerText.value > 0)
            if (detailAnswerText.options[detailAnswerText.value].text.ToLower().Trim().Equals(detail.ToLower().Trim())) percentCorrect += 20;
        answerCorrectness = percentCorrect;
        Debug.Log(answerCorrectness + " is the correctness");
    }
}
