using UnityEngine;

[CreateAssetMenu(menuName = "Cybersecurity/Lesson")]
public class LessonData : ScriptableObject
{
    public string lessonTitle;

    [TextArea(6,15)]
    public string lessonDescription;

    [TextArea(6,15)]
    public string warningSigns;

    public Sprite lessonImage;
}