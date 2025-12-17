using UnityEngine;

public class TimerSurvival : MonoBehaviour
{
    public static TimerSurvival Instance;

    public float TimeAlive { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Update()
    {
        if (GameManager.Instance != null && !GameManager.Instance.IsRunning) return;
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver) return;

        TimeAlive += Time.deltaTime;

        // Update HUD continuously
        UIManager.Instance?.SetHUD(ScoreManager.Instance != null ? ScoreManager.Instance.Score : 0, TimeAlive);
    }

    public void ResetTimer()
    {
        TimeAlive = 0f;
    }
}
