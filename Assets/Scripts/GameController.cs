using System.Collections;
using UnityEngine;
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
    public int levelNumber = 1;

    private int currentScore = 0;

    private void Awake()
    {
        if (instance)
        {
            Debug.LogError("This GameController already exists!");
        }

        instance = this;
    }

    private void Start()
    {
        if (SceneManager.sceneCount < 2)
        {
            SceneManager.LoadScene(levelNumber, LoadSceneMode.Additive);
        }
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

    private IEnumerator ReloadLevel()
    {
        AsyncOperation unloading = SceneManager.UnloadSceneAsync(levelNumber);
        while (!unloading.isDone)
        {
            yield return null;
        }
        SceneManager.LoadScene(levelNumber, LoadSceneMode.Additive);
    }
}
