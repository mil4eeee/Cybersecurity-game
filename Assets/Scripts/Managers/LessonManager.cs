using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LessonManager : MonoBehaviour
{
    [Header("Lesson Data")]
    [SerializeField] private LessonData[] lessons;

    [Header("Learn Panel UI")]
    [SerializeField] private TMP_Text lessonTitleText;
    [SerializeField] private TMP_Text lessonDescriptionText;
    [SerializeField] private TMP_Text warningSignsText;
    [SerializeField] private Image lessonImage;

    private int currentLessonIndex = 0;

    private void Start()
    {
        LoadCurrentLesson();
    }

    public void LoadCurrentLesson()
    {
        if (lessons == null || lessons.Length == 0)
        {
            Debug.LogError("LessonManager: No lessons assigned.");
            return;
        }

        if (currentLessonIndex < 0 || currentLessonIndex >= lessons.Length)
        {
            Debug.LogError("LessonManager: Current lesson index is invalid.");
            return;
        }

        if (lessonTitleText == null ||
            lessonDescriptionText == null ||
            warningSignsText == null)
        {
            Debug.LogError("LessonManager: One or more UI references are missing.");
            return;
        }

        LessonData currentLesson = lessons[currentLessonIndex];

        if (currentLesson == null)
        {
            Debug.LogError(
                $"LessonManager: Lesson at index {currentLessonIndex} is empty."
            );
            return;
        }

        lessonTitleText.text = currentLesson.lessonTitle;
        lessonDescriptionText.text = currentLesson.lessonDescription;
        warningSignsText.text = currentLesson.warningSigns;

        if (lessonImage != null)
        {
            if (currentLesson.lessonImage != null)
            {
                lessonImage.sprite = currentLesson.lessonImage;
                lessonImage.gameObject.SetActive(true);
            }
            else
            {
                lessonImage.gameObject.SetActive(false);
            }
        }
    }

    public void NextLesson()
    {
        if (lessons == null || lessons.Length == 0)
        {
            return;
        }

        if (currentLessonIndex < lessons.Length - 1)
        {
            currentLessonIndex++;
            LoadCurrentLesson();
        }
    }

    public void PreviousLesson()
    {
        if (lessons == null || lessons.Length == 0)
        {
            return;
        }

        if (currentLessonIndex > 0)
        {
            currentLessonIndex--;
            LoadCurrentLesson();
        }
    }

    public LessonData GetCurrentLesson()
    {
        if (lessons == null ||
            lessons.Length == 0 ||
            currentLessonIndex < 0 ||
            currentLessonIndex >= lessons.Length)
        {
            return null;
        }

        return lessons[currentLessonIndex];
    }
}