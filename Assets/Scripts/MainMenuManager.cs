using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private const string OverworldSceneName = "MainGame";

    [Header("Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        bool saveExists = File.Exists(SaveManager.SaveFilePath);
        resumeButton.interactable = saveExists;

        startButton.onClick.AddListener(OnNewGame);
        resumeButton.onClick.AddListener(OnResume);
        exitButton.onClick.AddListener(OnExit);
    }

    private void OnNewGame()
    {
        SaveManager.DeleteSave();
        SceneManager.LoadScene(OverworldSceneName);
    }

    private void OnResume()
    {
        SceneManager.LoadScene(OverworldSceneName);
    }

    private void OnExit()
    {
        Application.Quit();
    }
}
