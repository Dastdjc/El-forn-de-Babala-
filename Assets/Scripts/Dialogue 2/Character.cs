using UnityEngine;
[CreateAssetMenu(fileName ="New Character", menuName = "Character")]
public class Character : ScriptableObject
{
    public string fullName;
    //public Sprite portrait;
    public enum MoodType
    {
        Normal,
        Alegre,
        Sorprpendido,
        Vergonzoso,
        Dudoso,
        Enfadado,
        Triste
    };
    [System.Serializable] public class Mood 
    {
        public MoodType emotion;
        public Sprite graphic;
    }
    public Mood[] mood;
}
