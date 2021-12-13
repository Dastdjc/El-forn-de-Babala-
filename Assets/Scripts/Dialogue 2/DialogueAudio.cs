
using UnityEngine;
using TMPro;

public class DialogueAudio : MonoBehaviour
{
    //private VillagerScript villager;
    public TMP_Animated animatedText;

    public AudioClip[] voices;
    public AudioClip[] punctuations;
    [Space]
    public AudioSource voiceSource;
    public AudioSource punctuationSource;
    public string character;
    private DialogueManager dm;

    // Start is called before the first frame update
    void Start()
    {
        //villager = GetComponent<VillagerScript>();

        dm = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        animatedText.onTextReveal.AddListener((newChar) => ReproduceSound(newChar));
    }

    public void ReproduceSound(char c)
    {
        
        if (character == dm.conversation.lines[dm.conversationIndex].character.name)
        {
            if (c == '.' && !punctuationSource.isPlaying)
            {
                voiceSource.Stop();
                punctuationSource.clip = punctuations[Random.Range(0, punctuations.Length)];
                punctuationSource.Play();
            }

            if (char.IsLetter(c) && !voiceSource.isPlaying)
            {
                punctuationSource.Stop();
                voiceSource.clip = voices[Random.Range(0, voices.Length)];
                voiceSource.Play();
            }
        }

    }



}
