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

    [Header("Top Bar UI")]
    [SerializeField] private TMP_Text currentLessonText;

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
            Debug.LogError("LessonManager: Invalid lesson index.");
            return;
        }

        LessonData lesson = lessons[currentLessonIndex];

        if (lesson == null)
        {
            Debug.LogError(
                $"LessonManager: Lesson at index {currentLessonIndex} is empty."
            );
            return;
        }

        if (lessonTitleText != null)
        {
            lessonTitleText.text = lesson.lessonTitle;
        }

        if (lessonDescriptionText != null)
        {
            lessonDescriptionText.text = lesson.lessonDescription;
        }

        if (warningSignsText != null)
        {
            warningSignsText.text = lesson.warningSigns;
        }

        if (currentLessonText != null)
        {
            currentLessonText.text =
                $"Lesson {currentLessonIndex + 1:00}";
        }

        if (lessonImage != null)
        {
            if (lesson.lessonImage != null)
            {
                lessonImage.sprite = lesson.lessonImage;
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
        else
        {
            Debug.Log("LessonManager: Already on the final lesson.");
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
        else
        {
            Debug.Log("LessonManager: Already on the first lesson.");
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

    public int GetCurrentLessonIndex()
    {
        return currentLessonIndex;
    }
}