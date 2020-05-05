using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScore : MonoBehaviour
{
    public Text t_kills;
    public Text t_round;
    public Text t_score;
    public Image titleScreen;

    private void Start()
    {
        Color transparent = new Color(0, 0, 0, 0);
        if (GameManager.round <= 0)
        {
            t_round.color = transparent;
            t_kills.color = transparent;
            t_score.color = transparent;
        }
        else
        {
            titleScreen.color = transparent;
        }
    }

    private void Update()
    {
        t_kills.text = "KILLS\n" + GameManager.kills;
        t_score.text = "SCORE\n" + GameManager.score * 10;
        t_round.text = "ROUND\n" + GameManager.round;
        
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit(); // Quits the game
        }
    }
}
