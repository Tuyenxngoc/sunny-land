using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI heartText;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    public GameObject gamePausedPanel;

    private static UIController _instance;

    public static UIController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIController>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(nameof(UIController));
                    _instance = singletonObject.AddComponent<UIController>();
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

    void Start()
    {
        UpdateScoreUI(0);
        HideGameOverPanel();
        HidePausePanel();
    }

    public void UpdateScoreUI(int score)
    {
        scoreText.text = $"Gem: {score}";
    }

    public void UpdateHeartUI(int hearts)
    {
        heartText.text = $"Cherry: {hearts}";
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void HideGameOverPanel()
    {
        gameOverPanel.SetActive(false);
    }

    public void ShowPausePanel()
    {
        gamePausedPanel.SetActive(true);
    }

    public void HidePausePanel()
    {
        gamePausedPanel.SetActive(false);
    }
}
