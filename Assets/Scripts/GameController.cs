using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    private readonly UnityEvent onLevelLoaded = new UnityEvent();

    private void Awake()
    {
        if (instance)
        {
            Debug.LogError("This GameController already exists!");
        }
        instance = this;

        onLevelLoaded.AddListener(OnLevelLoaded);
    }

    private void Start()
    {
        if (SceneManager.sceneCount < 2)
            StartCoroutine(LoadLevel());
        else
            onLevelLoaded.Invoke();
    }

    private void OnDestroy()
    {
        onLevelLoaded.RemoveListener(OnLevelLoaded);
    }

    public void IncrementScore()
    {
        currentScore += 1;
        scoreText.text = currentScore.ToString();
    }

    public void BallFellDown()
    {
        StartCoroutine(nameof(ReloadLevel));
        ResetScore();
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
        onLevelLoaded.Invoke();
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
