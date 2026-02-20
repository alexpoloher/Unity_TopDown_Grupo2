using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject panelPause;


    public void Show()
    {
        panelPause.SetActive(true);
    }

    public void Hide()
    {
        panelPause.SetActive(false);
    }

    public void CtnGame()
    {
        UIManager.Instance.Resume();
    }

    public void SaveGameNow()
    {
        GameManager.Instance.SaveGame();
    }

    public void ExitToMenu()
    {
        UIManager.Instance.QuitToMainMenu();
    }
}
