using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public float difficulty;
    public float rampup;
    public int round;

    private float visibleScore;
    private float visibleDifficulty;
    public Text t_difframp;
    public Text t_round;
    public Text t_score;
    public GameManager spawner;

    private void Awake()
    {
        score = 0;
    }

    private void Update()
    {
        difficulty = spawner.difficulty * 100;
        score = GameManager.score * 10;
        round = GameManager.round;
        rampup = 10f + (spawner.difficultyRampup * -1);

        if (visibleScore < score)
        {
            visibleScore = visibleScore + 1f;
        }

        if (visibleDifficulty < difficulty)
        {
            visibleDifficulty = visibleDifficulty + 1f;
        }
        else if (visibleDifficulty > difficulty)
        {
            visibleDifficulty = visibleDifficulty - 1f;
        }
        t_difframp.text = visibleDifficulty + "\n" + rampup;
        t_score.text = "SCORE\n" + score;
        t_round.text = "ROUND\n" + round;
    }

}
