using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public AudioSource hitSFX;
    public AudioSource missSFX;
    public TMPro.TextMeshPro scoreText;
    static public int score;
    static public int comboScore;
    static public int maxCombo;
    void Start()
    {
        Instance = this;
        comboScore = 0;
        maxCombo = 0;
        score = 0;
    }
    public static void Hit()
    {
        comboScore += 1;
        score += 100;
        if (comboScore > maxCombo)
            maxCombo = comboScore;
        Instance.hitSFX.Play();
    }
    public static void Miss()
    {
        comboScore = 0;
        Instance.missSFX.Play();
    }
    private void Update()
    {
        scoreText.text = score.ToString();
    }
}