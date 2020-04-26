using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {
    public GameObject PausePanel;
    public Canvas PauseCanvas;
    public Button StartButton, SettingsButton, ExitButton;

    public bool isPaused, canMove;

    void Start() {
        StartButton.onClick.AddListener(ResumeGame);
        ExitButton.onClick.AddListener(CloseGame);
        SettingsButton.onClick.AddListener(OpenSettings);
    }

    void Update() {
        if (Input.GetKeyUp("p") || Input.GetKeyUp(KeyCode.Escape)) {
            TogglePause();
        }

    }

    private void TogglePause() {
        isPaused = !isPaused;
        if (isPaused) {
            PauseGame();
        }
        if (!isPaused) {
            ResumeGame();
        }
    }

    public void PauseGame() {
        Time.timeScale = 0f;
        PauseCanvas.enabled = true;
        canMove = false;
    }

    public void ResumeGame() {
        Time.timeScale = 1.0f;
        PauseCanvas.enabled = false;
        canMove = true;
    }

    public void OpenSettings() {


    }

    public void RestartGame() {
        ResumeGame();

    }

    public void CloseGame() {
        SceneManager.LoadScene(sceneName: "MainMenu");
    }
}
