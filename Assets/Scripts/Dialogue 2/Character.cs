using UnityEngine;
[CreateAssetMenu(fileName ="New Character", menuName = "Character")]
public class Character : ScriptableObject
{
    public string fullName;
    public Sprite portrait;

    [System.Serializable] public class Mood 
    {
        public string emotion;
        public Sprite graphic;
    }
    public Mood[] mood;
}
