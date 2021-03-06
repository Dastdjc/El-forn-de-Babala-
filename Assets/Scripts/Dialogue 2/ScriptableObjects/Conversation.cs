using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public struct Line {
    public Character character;

    [TextArea(2, 5)]
    public string text;

    public enum Mood { 
        Normal,
        Alegre,
        Sorprpendido,
        Vergonzoso,
        Dudoso,
        Enfadado,
        Triste
    };
    public Mood Emotion;
}

[CreateAssetMenu(fileName = "New Conversation", menuName = "Conversation")]
public class Conversation : ScriptableObject
{
    public Character speakerLeft;
    public Character speakerRight;
    public List<Line> lines = new List<Line>();

}
