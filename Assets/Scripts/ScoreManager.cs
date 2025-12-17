using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int Score { get; private set; }
    public int Multiplier { get; private set; } = 1;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void ResetRun()
    {
        Score = 0;
        Multiplier = 1;
    }

    public void Add(int amount)
    {
        Score += amount * Multiplier;
        // no UI now
        // Debug.Log("Score: " + Score);
    }

    public void SetMultiplier(int m) => Multiplier = Mathf.Max(1, m);
}
