using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    [SerializeField] int score;
    [SerializeField] int highscore;
    public Color[] template;

    private int playerColor = 0;
    [SerializeField] Image colorImage;
    private int playerNextColor = 0;
    [SerializeField] Image colorNextImage;
    private UIController uiController;

    private float time;
    [SerializeField] float timeToChangeColor;
    [SerializeField] float timeOfGame;
    public Camera UICamera;
    // Start is called before the first frame update
    void Start()
    {
        uiController = GetComponent<UIController>();
        Reset();
    }

    float checkTime = 0;
    float scale = 1;
    public float scaleByX = 0;
    // Update is called once per frame
    void Update()
    {
        time -= (scale+scaleByX) * Time.deltaTime;
        checkTime += Time.deltaTime;
        UpdateSlider();

        
        if(time < 0)
        {
            GameOver();
        }
    }

    public void UpdateSlider()
    {
        uiController.UpdateSlider(time);
    }

    public void UpdateSliderByValue(float value)
    {
        UpdateScore();
        time += value;
        if(time >timeOfGame)
            time = timeOfGame;
    }

    public void SetSlider()
    {
        uiController.SetSlider(timeOfGame);
    }

    public void OnPressHandle(int index)
    {
        //if(index == playerColor)
        //{
        //    UpdateScore();
        //}
        //else
        //{
        //    GameOver();
        //}
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        uiController.GameOver();
        Reset();
    }

    public void UpdateScore()
    {
        //time+=2;
        score++;
        uiController.UpdateScore(score);
        if (score > highscore)
        {
            highscore = score;
            uiController.UpdateHighScore(highscore);
            PlayerPrefs.SetInt("highscore", highscore);
        }
    }

    public void UpdateColor()
    {
        colorImage.color = template[playerColor];
        colorNextImage.color = template[playerNextColor];
    }

    public void ChangeColor()
    {
        playerColor = playerNextColor;
        playerNextColor = Random.Range(0, template.Length);
        while(playerNextColor == playerColor)
        {
            playerNextColor = Random.Range(0, template.Length);
        }
        UpdateColor();
    }

    public void Reset()
    {
        Time.timeScale = 1;
        SoundController.Instance.PlayAudio(SoundController.Instance.bg, 0.3f, true);
        playerNextColor = Random.Range(0, template.Length);
        ChangeColor();
        time = timeOfGame;
        SetSlider();
        score = 0;
        highscore = PlayerPrefs.GetInt("highscore");
        uiController.UpdateScore(score);
        uiController.UpdateHighScore(highscore);
    }

}
