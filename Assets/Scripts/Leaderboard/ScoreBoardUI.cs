using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class ScoreBoardUI : MonoBehaviour
{
    [Header("UI References")]
    public Transform boardRows;      // The Vertical Layout Group container
    public GameObject rowPrefab;         // A prefab with a single TMP_Text
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PopulateScoreboard();
    }

    public void PopulateScoreboard()
    {
        //clear rows
        foreach (Transform child in boardRows)
        {
            Destroy(child.gameObject);
        }

        List<ScoreBoardEntry> scores = ScoreBoardManager.Instance.GetScores();

        for (int i = 0; i < scores.Count; i++)
        {
            GameObject row = Instantiate(rowPrefab, boardRows);
            TMP_Text text = row.GetComponent<TMP_Text>();
            text.text = $"{i + 1}. {scores[i].playerName}  —  {scores[i].score}";
        }
    }
}
