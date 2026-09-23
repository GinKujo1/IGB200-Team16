using System.Collections.Generic;
using UnityEngine;

public class ScoreBoardManager : MonoBehaviour
{
    public static ScoreBoardManager Instance;

    private const string SAVE_KEY = "localScoreboard";
    private const int MAX_ENTRIES = 10;

    private ScoreBoardList highScores = new ScoreBoardList();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void AddScore(string playerName, float score)
    {
        highScores.entries.Add(new ScoreBoardEntry(playerName, score));

        highScores.entries.Sort((a, b) => b.score.CompareTo(a.score));

        if(highScores.entries.Count > MAX_ENTRIES)
        {
            highScores.entries.RemoveRange(MAX_ENTRIES, highScores.entries.Count - MAX_ENTRIES);
        }

        SaveScores();
    }

    public List<ScoreBoardEntry> GetScores()
    {
        return highScores.entries;
    }

    public bool QualifiesForLeaderboard(float score)
    {
        if (highScores.entries.Count < MAX_ENTRIES) return true;
        return score > highScores.entries[highScores.entries.Count - 1].score;
    }   
    // ---------- SAVE / LOAD ----------

    private void SaveScores()
    {
        string json = JsonUtility.ToJson(highScores);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save(); // CRITICAL for WebGL
        Debug.Log("Leaderboard saved: " + json);
    }
    private void LoadScores()
    {
        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            string json = PlayerPrefs.GetString(SAVE_KEY);
            highScores = JsonUtility.FromJson<ScoreBoardList>(json);
            if (highScores == null) highScores = new ScoreBoardList();
        }
        else
        {
            highScores = new ScoreBoardList();
        }
    }

    // Optional: clear for testing
    [ContextMenu("Clear Leaderboard")]
    public void ClearLeaderboard()
    {
        highScores = new ScoreBoardList();
        SaveScores();
    }
}
