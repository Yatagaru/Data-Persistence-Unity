using System;
using System.Linq;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SceneController : MonoBehaviour
{
    [HideInInspector] public static SceneController Instance;
    [SerializeField] private InputField nameInput;
    [SerializeField] private Button[] menuButtons;
    public string savedPlayerName;
    public KeyCode savedSettingsKey;

    private void Awake()
    {
        SingletonCode();
       
    }

    [Serializable]
    public class PlayerInfo
    {
        public string bestPlayer;
        public int highestScore;
        public KeyCode savedKey;
    }

    private void SingletonCode()
    {
        int indexOfScene = SceneManager.GetActiveScene().buildIndex;
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else if (indexOfScene == 0)
        {
            DontDestroyOnLoad(gameObject);
        }
        Instance = this;


    }

    public void AssignButtonFunction()
    {
        menuButtons[0] = GameObject.Find("Start").GetComponent<Button>();
        menuButtons[1] = GameObject.Find("Exit").GetComponent<Button>();
        menuButtons[2] = GameObject.Find("Settings").GetComponent<Button>();

        menuButtons[0].onClick.AddListener(delegate { LoadScene(true); });
        menuButtons[1].onClick.AddListener(delegate { Exit(); });
        menuButtons[2].onClick.AddListener(delegate { LoadScene(false); });
    }
    public void LoadScene(bool startButton)
    {
        if (nameInput.text != string.Empty && HasLetters() == true && startButton == true)
        {
            savedPlayerName = nameInput.text;
            
            SceneManager.LoadScene(1);
        }
        else if (startButton == false) SceneManager.LoadScene(2);
   
        else { throw new Exception("The player name can´t have numbers & can´t be empty"); }
       
    }

    public void Exit()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();

        #else 
            Application.Quit();

        #endif
    }

    private bool HasLetters()
    {
        if (nameInput.text.All(char.IsLetter)) { return true; }
        else { return false; }
    }


    public void SaveHighscoreInfo(int score, string player)
    {
        PlayerInfo playerData = new PlayerInfo();
        playerData.highestScore = score;
        playerData.bestPlayer = player;

        string json = JsonUtility.ToJson(playerData);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        
    }

    public void LoadHighscoreInfo()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerInfo info = JsonUtility.FromJson<PlayerInfo>(json);

            MainManager.highScore = info.highestScore;
            MainManager.bestName = info.bestPlayer;
        }
    }

    public void SaveSettings(PlayerInfo playerData)
    {
        string path = System.IO.Path.Combine(Application.persistentDataPath, "bandtest.json");
        string json = JsonUtility.ToJson(playerData);

        File.WriteAllText(path, json);
    }

    public void LoadSettings(KeyCode playerKey)
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerInfo info = JsonUtility.FromJson<PlayerInfo>(json);

            playerKey = info.savedKey;
        }
    }
}
