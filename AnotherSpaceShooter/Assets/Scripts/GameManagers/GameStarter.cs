using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    public EnemyTank Tankprefab;
    public EnemyChopper Chopperprefab;
    public BossShip BossPrefab;
    public Player playerClass;
    public static Player player;

    public void LoadScene()
    {
        player = playerClass;
        SceneManager.LoadScene("TestScene");
    }
}
