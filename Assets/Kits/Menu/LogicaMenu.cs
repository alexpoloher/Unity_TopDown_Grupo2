using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.EventSystems;

public class LogicaMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject characterSelectPanel;
    public GameObject pauseMenuPanel;
    public Button continueButton;

    public GameObject characterPreviewPoint;

    private string savePath;

    public Button defaultButton;

    void Start()
    {
        savePath = Application.persistentDataPath + "/save.json";

        mainMenuPanel.SetActive(true);
        characterSelectPanel.SetActive(false);
        characterPreviewPoint.SetActive(false);

        continueButton.interactable = File.Exists(savePath);

        EventSystem.current.SetSelectedGameObject(defaultButton.gameObject);
    }

    public void NewGame()
    {
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        characterSelectPanel.SetActive(true);
        characterPreviewPoint.SetActive(true);
    }

    public void ContinueGame()
    {
        GameManager.Instance.LoadGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
