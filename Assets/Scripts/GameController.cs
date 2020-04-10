using System.Collections;
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
    public GameObject floor;
    public GameObject laserCountUI;
    public Text laserCountText;
    public int levelNumber = 1;

    private int currentScore;

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
        ballManager.onGameOver.AddListener(GameOver);
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

    public void GameOver()
    {
        StartCoroutine(nameof(ReloadLevel));
        ResetScore();
        ballManager.ResetBalls();
        paddle.Reset();
    }

    private void ResetScore()
    {
        currentScore = 0;
        scoreText.text = "0";
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

    private void OnLevelLoaded()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(levelNumber));
    }
}
