using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; private set; }

    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed {  get; private set; }

    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI hiScoreText;
    [SerializeField] private Button retryButton;

    private Player player;
    private Spawner spawner;

    private float score;

   private void Awake()
   {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
   }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();

        NewGame();
    }

    public void NewGame()
    {
        Obstacles[] obstacles = FindObjectsOfType<Obstacles>();

        foreach(Obstacles obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        score = 0;
        gameSpeed = initialGameSpeed;
        enabled = true;

        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);

        UpdateHiScore();
    }

    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString(format: "D5");
    }

    public void GameOver()
    {
        gameSpeed = 0;
        enabled = false;

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        
        UpdateHiScore();
    }

    private void UpdateHiScore()
    {
        float hiScore = PlayerPrefs.GetFloat("hiscore", 0);

        if (score > hiScore)
        {
            hiScore = score;
            PlayerPrefs.SetFloat("hiscore", hiScore);
        }

        hiScoreText.text = Mathf.FloorToInt(hiScore).ToString(format: "D5");
    }
}
