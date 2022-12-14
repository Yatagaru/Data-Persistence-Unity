using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SceneController;

public class SettingsManager : MonoBehaviour
{
    private Dictionary<string, KeyCode> controls = new Dictionary<string, KeyCode>();

    private string controlsKey = "Space";
    private KeyCode currentKey;
    private int controlsIndex;
    [SerializeField] private Text changeKeysButtonText;

    void Start()
    {
        controls.Add("Space", KeyCode.Space);
        controls.Add("Mouse", KeyCode.Mouse1); 
        controls.Add("Enter", KeyCode.Return);
       
    }

    // Update is called once per frame
    void Update()
    {
        currentKey = controls[controlsKey];
    }


    public void ChangeControls()
    {
        if (controlsIndex < 2) controlsIndex++;
        else { controlsIndex = 0; }


        switch (controlsIndex)
        {
            case 0: controlsKey = "Space"; break;
            case 1: controlsKey= "Mouse"; break;
            case 2: controlsKey = "Enter"; break;
        }

       changeKeysButtonText.text = controlsKey;
    }

    public void ReturnToMenu ()
    {
        SceneController.Instance.savedSettingsKey = currentKey;
        PlayerInfo settings = new PlayerInfo();

        settings.savedKey = SceneController.Instance.savedSettingsKey;
        SceneController.Instance.SaveSettings(settings);
#pragma warning disable CS0618 // Type or member is obsolete
        Application.LoadLevel(0);
#pragma warning restore CS0618 // Type or member is obsolete
    }
}
