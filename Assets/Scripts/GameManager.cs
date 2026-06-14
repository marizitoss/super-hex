using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject scorePanel;
    [SerializeField] private TMP_Text scoreText;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowFinalScore()
    {
        int score = CalculateScore();

        scoreText.text = $"Score: {score}";

        scorePanel.SetActive(true);
    }

    private int CalculateScore()
    {
        HexCell[] cells =
            FindObjectsByType<HexCell>(
                FindObjectsSortMode.None);

        int total = 0;

        foreach (var cell in cells)
        {
            total += cell.Value;
        }

        return total;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex);
    }
}