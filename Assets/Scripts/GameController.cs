﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(BallManager))]
public class GameController : MonoBehaviour
{
    #region Singleton

    private static GameController instance;

    public static GameController Instance
    {
        get
        {
            if (!instance)
            {
                Debug.LogError("The GameController is not initialized!");
            }

            return instance;
        }
    }

    #endregion

    public Text scoreText;
    public Paddle paddle;
    public GameObject laserCountUI;
    public Text laserCountText;
    public int levelNumber = 1;
    public int maxLevelNumber = 3;
    public GameObject youWonUI;

    [Header("Sound")]
    public AudioSource brickSounds;

    public AudioSource powerUpSounds;

    public AudioSource laserSounds;

    public AudioSource miscSounds;

    private int startScore;
    private int currentScore;
    private bool gameWon;

    private BallManager ballManager;

    public BallManager BallManager => ballManager;

    private void Awake()
    {
        if (instance)
        {
            Debug.LogError("This GameController already exists!");
        }
        instance = this;

        ballManager = GetComponent<BallManager>();
    }

    private void Start()
    {
        if (SceneManager.sceneCount < 2)
            StartCoroutine(LoadLevel());
        else
            OnLevelLoaded();
    }

    public void IncrementScore()
    {
        currentScore += 1;
        scoreText.text = currentScore.ToString();
    }

    public void GameOver(bool resetScore = true)
    {
        if (gameWon)
            return;

        Brick.ResetCount();
        StartCoroutine(nameof(ReloadLevel));
        if (resetScore)
            ResetScore();
        ballManager.ResetBalls();
        paddle.Reset();
    }

    public void GoToNextLevel()
    {
        if (levelNumber >= maxLevelNumber)
        {
            gameWon = true;
            youWonUI.SetActive(true);
            return;
        }

        startScore = currentScore;
        Brick.ResetCount();
        StartCoroutine(LoadNextLevel());
        ballManager.ResetBalls();
        paddle.Reset();
    }

    private void ResetScore()
    {
        currentScore = startScore;
        scoreText.text = currentScore.ToString();
    }

    private IEnumerator LoadLevel()
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(levelNumber, LoadSceneMode.Additive);
        while (!loading.isDone)
            yield return null;
        OnLevelLoaded();
    }

    private IEnumerator ReloadLevel()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
        AsyncOperation unloading = SceneManager.UnloadSceneAsync(levelNumber);
        while (!unloading.isDone)
            yield return null;
        yield return LoadLevel();
    }

    private IEnumerator LoadNextLevel()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
        AsyncOperation unloading = SceneManager.UnloadSceneAsync(levelNumber);
        while (!unloading.isDone)
            yield return null;
        levelNumber++;
        yield return LoadLevel();
    }

    private void OnLevelLoaded()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(levelNumber));
    }
}
