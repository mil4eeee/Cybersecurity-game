using UnityEngine;

[CreateAssetMenu(
    fileName = "NewLesson",
    menuName = "Cybersecurity/Lesson"
)]
public class LessonData : ScriptableObject
{
    [Header("Lesson Information")]
    public string lessonTitle;

    [TextArea(5, 15)]
    public string lessonDescription;

    [TextArea(4, 12)]
    public string warningSigns;

    public Sprite lessonImage;

    [Header("Practice Email")]
    public string senderName;
    public string senderAddress;
    public string emailSubject;

    [TextArea(6, 15)]
    public string emailBody;

    public string displayedLink;

    [Header("Practice Answer")]
    public bool isPhishing;

    [TextArea(3, 10)]
    public string correctFeedback;

    [TextArea(3, 10)]
    public string incorrectFeedback;

    [Header("Progress")]
    public int xpReward = 100;
}