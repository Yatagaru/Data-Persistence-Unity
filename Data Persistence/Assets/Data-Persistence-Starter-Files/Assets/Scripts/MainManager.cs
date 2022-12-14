using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text PlayerInfoText;
    public Button highscoreButton;
    public GameObject GameOverText;
    public string playerName;
    public static int highScore;
    public static string bestName;
    private KeyCode playerKey;
    
    public bool m_Started = false;
    public int m_Points;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        playerName = SceneController.Instance.savedPlayerName;
        SceneController.Instance.LoadHighscoreInfo();
        SceneController.Instance.LoadSettings(playerKey);

        PlayerInfoText.text = "HighScore: " + highScore + " || Player: " + bestName; 
        ScoreText.text = "Player: " + playerName + " || Score: " + m_Points;
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(playerKey))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (m_Points > highScore)
                {
                    SceneController.Instance.SaveHighscoreInfo(m_Points, playerName);
                }
                
                SceneManager.LoadScene(1);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = "Player: " + playerName + " || Score: " + m_Points;
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        highscoreButton.gameObject.SetActive(true);
    }

    public void GoToHighscore()
    {
        Application.LoadLevel(2);
    }
}
