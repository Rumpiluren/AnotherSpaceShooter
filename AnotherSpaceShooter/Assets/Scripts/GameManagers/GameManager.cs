using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int enemies;
    public EnemyTank[] livingTanks;
    public EnemyChopper[] livingChopper;
    public EnemyTank Tankprefab;
    public EnemyChopper Chopperprefab;
    public BossShip BossPrefab;
    public Transform target;
    public float difficulty = 1f;
    public float difficultyRampup = 5;
    public static int score;
    public static int round;
    public static int kills;
    public float oldRoundDifficulty;
    Player player;
    public Image screenFadeImg;

    private void Awake()
    {
        Player newPlayer;
        newPlayer = Instantiate(GameStarter.player);
        target = newPlayer.transform;
        newPlayer.OnDeath += EndGame;
        score = 0;
        round = 1;
        kills = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            difficultyRampup = Mathf.Clamp(difficultyRampup - 0.25f, 0.25f, 10);
            CancelInvoke("IncreaseDifficulty");
            InvokeRepeating("IncreaseDifficulty", 0, difficultyRampup);
        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            difficultyRampup = Mathf.Clamp(difficultyRampup + 0.25f, 0.25f, 10);
            CancelInvoke("IncreaseDifficulty");
            InvokeRepeating("IncreaseDifficulty", difficultyRampup, difficultyRampup);
        }

        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
    }

    void Start()
    {
        InvokeRepeating("SpawnTank", 0, Random.Range(.2f, .4f));
        InvokeRepeating("SpawnChopper", 0, Random.Range(.2f, .4f));
        InvokeRepeating("IncreaseDifficulty", difficultyRampup, difficultyRampup);
        Invoke("Boss", 10);
    }

    void SpawnTank()
    {
        livingTanks = FindObjectsOfType<EnemyTank>();
        livingChopper = FindObjectsOfType<EnemyChopper>();
        if (livingTanks.Length + livingChopper.Length < enemies)
        {
            Vector2 spawnPosition = Random.insideUnitSphere;
            spawnPosition = spawnPosition.normalized * 5;
            EnemyTank newTank = Instantiate(Tankprefab, spawnPosition, Quaternion.identity);
            newTank.target = target;
            newTank.fireRate = difficulty;
            newTank.OnDeath += AddPoint;
        }
    }

    void SpawnChopper()
    {
        livingTanks = FindObjectsOfType<EnemyTank>();
        livingChopper = FindObjectsOfType<EnemyChopper>();
        if (livingTanks.Length + livingChopper.Length < enemies)
        {
            Vector2 spawnPosition = Random.insideUnitSphere;
            spawnPosition = spawnPosition.normalized * 5;
            EnemyChopper newChopper = Instantiate(Chopperprefab, spawnPosition, Quaternion.identity);
            newChopper.target = target;
            newChopper.desiredDistance = Random.Range(1, 2.5f);
            newChopper.fireRate = difficulty;
            newChopper.OnDeath += AddPoint;
        }
    }

    IEnumerator FadeScreen()
    {
        while (screenFadeImg.color.a < 1)
        {
            Color newAlpha = screenFadeImg.color;
            newAlpha.a += Time.deltaTime / 2;
            screenFadeImg.color = newAlpha;
            yield return null;
        }

        SceneManager.LoadScene("MainMenu");
        yield return null;
    }

    void EndGame()
    {
        StartCoroutine(FadeScreen());
    }

    void AddPoint()
    {
        kills++;
        score++;
    }

    void IncreaseDifficulty()
    {
        difficulty += 0.25f;
        enemies += 1;
    }

    void ResumeDifficulty()
    {
        round++;
        difficulty = Mathf.Clamp(difficulty - .5f, oldRoundDifficulty + 0.25f, Mathf.Infinity);
        InvokeRepeating("SpawnTank", 0, Random.Range(.2f, .4f));
        InvokeRepeating("SpawnChopper", 0, Random.Range(.2f, .4f));
        InvokeRepeating("IncreaseDifficulty", difficultyRampup, difficultyRampup);
        Invoke("Boss", 30);
        oldRoundDifficulty = difficulty;
        score += 10;
    }

    void Boss()
    {
        kills++;
        CancelInvoke();
        Vector2 spawnPosition = Random.insideUnitSphere;
        spawnPosition = spawnPosition.normalized * 5;
        BossShip newBoss = Instantiate(BossPrefab, spawnPosition, Quaternion.identity);
        newBoss.target = target;
        newBoss.desiredDistance = Random.Range(2f, 4f);
        newBoss.fireRate = difficulty;
        newBoss.OnDeath += ResumeDifficulty;
    }
}
