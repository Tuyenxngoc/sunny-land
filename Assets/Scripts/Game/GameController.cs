using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private float playTime = 0f;
    public int enemiesKilled = 0;
    private int gemsCollected = 0;
    private int cherriesCollected = 0;

    private int heart = 3;
    private int score = 0;
    public bool gameOver = false;
    public bool isPaused = false;
    private static GameController _instance;

    public static GameController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameController>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(GameController).Name);
                    _instance = singletonObject.AddComponent<GameController>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UIController.Instance.UpdateScoreUI(score);
        UIController.Instance.UpdateHeartUI(heart);
    }

    void Update()
    {
        //Tính toán thời gian chơi
        if (!isPaused)
        {
            playTime += Time.deltaTime;
        }
    }

    public void AddScore(int points)
    {
        gemsCollected += points;
        score += points;
        UIController.Instance.UpdateScoreUI(score);
    }

    public void AddHeart(int points)
    {
        cherriesCollected += points;
        heart += points;
        UIController.Instance.UpdateHeartUI(heart);
    }

    public void SubtractHeart(int amount)
    {
        heart -= amount;
        if (heart < 0)
        {
            heart = 0;
        }

        UIController.Instance.UpdateHeartUI(heart);

        if (heart == 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        gameOver = true;
        Time.timeScale = 0;
        UIController.Instance.ShowGameOverPanel();
    }

    public void RestartGame()
    {
        // Reset lại các giá trị
        playTime = 0;
        enemiesKilled = 0;
        gemsCollected = 0;
        cherriesCollected = 0;

        score = 0;
        heart = 3;
        gameOver = false;
        isPaused = false;

        // Cập nhật lại giao diện
        UIController.Instance.UpdateScoreUI(score);
        UIController.Instance.UpdateHeartUI(heart);
        UIController.Instance.HideGameOverPanel();

        // Đặt lại thời gian
        Time.timeScale = 1;

        // Tải lại cảnh hiện tại
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        UIController.Instance.HideGameOverPanel();
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        UIController.Instance.ShowPausePanel();
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        UIController.Instance.HidePausePanel();
        isPaused = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    internal void SaveInfo()
    {
        PlayerPrefs.SetFloat("PlayTime", playTime);
        PlayerPrefs.SetInt("EnemiesKilled", enemiesKilled);
        PlayerPrefs.SetInt("GemsCollected", gemsCollected);
        PlayerPrefs.SetInt("CherriesCollected", cherriesCollected);
    }
}
