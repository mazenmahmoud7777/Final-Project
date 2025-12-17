using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject hudPanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;

    [Header("HUD")]
    public TMP_Text scoreText;
    public TMP_Text timerText;

    [Header("GameOver")]
    public TMP_Text finalScoreText;
    public TMP_Text finalTimeText;

    [Header("Buttons")]
    public Button startButton;
    public Button resumeButton;
    public Button restartButton;
    public Button quitToMenuButton;

    [Header("Optional")]
    public Slider volumeSlider;
    public AudioSource musicSource;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        // wire buttons
        if (startButton) startButton.onClick.AddListener(() => GameManager.Instance?.StartRun());
        if (resumeButton) resumeButton.onClick.AddListener(() => GameManager.Instance?.Resume());
        if (restartButton) restartButton.onClick.AddListener(() => GameManager.Instance?.RestartScene());
        if (quitToMenuButton) quitToMenuButton.onClick.AddListener(() => GameManager.Instance?.ResetToMenu());

        if (volumeSlider)
        {
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
            OnVolumeChanged(volumeSlider.value);
        }
    }

    public void ShowMainMenu()
    {
        if (mainMenuPanel) mainMenuPanel.SetActive(true);
        if (hudPanel) hudPanel.SetActive(false);
        if (pausePanel) pausePanel.SetActive(false);
        if (gameOverPanel) gameOverPanel.SetActive(false);
    }

    public void ShowHUD()
    {
        if (mainMenuPanel) mainMenuPanel.SetActive(false);
        if (hudPanel) hudPanel.SetActive(true);
        if (pausePanel) pausePanel.SetActive(false);
        if (gameOverPanel) gameOverPanel.SetActive(false);
    }

    public void ShowPause()
    {
        if (pausePanel) pausePanel.SetActive(true);
        if (hudPanel) hudPanel.SetActive(false);
    }

    public void ShowGameOver(int score, float timeAlive)
    {
        if (gameOverPanel) gameOverPanel.SetActive(true);
        if (hudPanel) hudPanel.SetActive(false);
        if (pausePanel) pausePanel.SetActive(false);

        if (finalScoreText) finalScoreText.text = "Final Score: " + score;
        if (finalTimeText) finalTimeText.text = "Time: " + timeAlive.ToString("0.0");
    }

    public void SetHUD(int score, float timeAlive)
    {
        if (scoreText) scoreText.text = "Score: " + score;
        if (timerText) timerText.text = "Time: " + timeAlive.ToString("0.0");
    }

    void OnVolumeChanged(float v)
    {
        if (musicSource) musicSource.volume = v;
        AudioListener.volume = v;
    }
}
