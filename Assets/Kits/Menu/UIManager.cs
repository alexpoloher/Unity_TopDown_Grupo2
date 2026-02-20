using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Prefabs")]
    [SerializeField] private PauseMenu pauseMenuPrefab;

    [Header("Input")]
    [SerializeField] private InputActionReference pauseAction;

    private PauseMenu pauseMenu;
    private bool isPaused;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        CreatePauseMenu();
    }

    private void OnEnable()
    {        
        pauseAction.action.Enable();
        pauseAction.action.performed += OnPause;
    }

    private void OnDisable()
    {
        pauseAction.action.Disable();
        pauseAction.action.performed -= OnPause;
       
    }

    private void CreatePauseMenu()
    {
        if (pauseMenu != null) return;

        pauseMenu = Instantiate(pauseMenuPrefab);
        DontDestroyOnLoad(pauseMenu.gameObject);
        pauseMenu.Hide(); 
    }

    private void OnPause(InputAction.CallbackContext ctx)
    {
        if (isPaused) Resume();
        else Pause();
    }


    public void Pause()
    {
        if (isPaused) return;
        isPaused = true;

        Time.timeScale = 0f;
        AudioListener.pause = true;

        CreatePauseMenu();
        pauseMenu.Show();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        if (!isPaused) return;
        isPaused = false;

        pauseMenu.Hide();

        Time.timeScale = 1f;
        AudioListener.pause = false;

        Cursor.visible = true; 
        Cursor.lockState = CursorLockMode.None;
    }

    public void QuitToMainMenu()
    {
        if (isPaused) Resume();

        SceneManager.LoadScene("MainMenu");
    }
}
