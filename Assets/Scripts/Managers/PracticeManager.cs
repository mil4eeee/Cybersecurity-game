using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PracticeManager : MonoBehaviour
{
    [Header("Lesson Source")]
    [SerializeField] private LessonManager lessonManager;

    [Header("Practice UI")]
    [SerializeField] private TMP_Text practiceTitleText;
    [SerializeField] private TMP_Text senderText;
    [SerializeField] private TMP_Text subjectText;
    [SerializeField] private TMP_Text emailBodyText;
    [SerializeField] private TMP_Text linkText;
    [SerializeField] private TMP_Text resultText;

    [Header("Answer Buttons")]
    [SerializeField] private Button safeButton;
    [SerializeField] private Button phishingButton;

    private LessonData currentLesson;
    private bool answered;

    private void Start()
    {
        LoadPractice();
    }

    public void LoadPractice()
    {
        if (lessonManager == null)
        {
            Debug.LogError("PracticeManager: LessonManager is not assigned.");
            return;
        }

        currentLesson = lessonManager.GetCurrentLesson();

        if (currentLesson == null)
        {
            Debug.LogError("PracticeManager: Current lesson is unavailable.");
            return;
        }

        answered = false;

        if (practiceTitleText != null)
        {
            practiceTitleText.text =
                $"{currentLesson.lessonTitle} — Practice";
        }

        if (senderText != null)
        {
            senderText.text =
                $"From: {currentLesson.senderName} " +
                $"<{currentLesson.senderAddress}>";
        }

        if (subjectText != null)
        {
            subjectText.text =
                $"Subject: {currentLesson.emailSubject}";
        }

        if (emailBodyText != null)
        {
            emailBodyText.text = currentLesson.emailBody;
        }

        if (linkText != null)
        {
            linkText.text = currentLesson.displayedLink;
        }

        if (resultText != null)
        {
            resultText.text = "Select an answer.";
        }

        SetButtonsInteractable(true);
    }

    public void ChooseSafe()
    {
        CheckAnswer(false);
    }

    public void ChoosePhishing()
    {
        CheckAnswer(true);
    }

    private void CheckAnswer(bool playerSelectedPhishing)
    {
        if (answered || currentLesson == null)
        {
            return;
        }

        answered = true;

        bool isCorrect =
            playerSelectedPhishing == currentLesson.isPhishing;

        if (resultText != null)
        {
            resultText.text = isCorrect
                ? currentLesson.correctFeedback
                : currentLesson.incorrectFeedback;
        }

        SetButtonsInteractable(false);

        Debug.Log(
            isCorrect
                ? $"Correct answer. Award {currentLesson.xpReward} XP."
                : "Incorrect answer."
        );
    }

    private void SetButtonsInteractable(bool value)
    {
        if (safeButton != null)
        {
            safeButton.interactable = value;
        }

        if (phishingButton != null)
        {
            phishingButton.interactable = value;
        }
    }

  public void ResetPractice()
{
    answered = false;
    currentLesson = null;

    if (resultText != null)
    {
        resultText.text = "Select an answer.";
    }

    SetButtonsInteractable(true);
}
}