using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player State")]
    public string characterId;
    public float currentLife;
    public int currentLevel;
    public Vector3 currentPlayerPosition;
    public int currentArrowAmount;
    public int currentBombAmount;
    public int currentKeyAmount;
    public bool nowHasEspada;
    public bool nowHasArco;

    private string SavePath => Application.persistentDataPath + "/save.json";

    private bool isLoadingFromSave = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentLevel = scene.buildIndex;

        if (currentLevel == 0)
            return;

        if (isLoadingFromSave)
        {
            isLoadingFromSave = false;
            ApplyToPlayer();
            Debug.Log("Estoy cargando el nivel guardado");
            Debug.Log(currentPlayerPosition);
            return;
        }

        SaveGame();
        Debug.Log("He guardado el nivel " + currentLevel);
    }

    public void SaveGame()
    {
        RefreshFromPlayer();

        SaveData data = new SaveData
        {
            characterId = characterId,
            scene = currentLevel,
            life = currentLife,
            playerPosition = currentPlayerPosition,
            arrowAmount = currentArrowAmount,
            bombAmount = currentBombAmount,
            keyAmount = currentKeyAmount,
            hasEspada = nowHasEspada,
            hasArco = nowHasArco,
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
    }

    public void LoadGame()
    {
        if (!File.Exists(SavePath))
        {
            Debug.Log("No save file found");
            return;
        }

        string json = File.ReadAllText(SavePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        characterId = data.characterId;
        currentLife = data.life;
        currentLevel = data.scene;
        currentPlayerPosition = data.playerPosition;
        currentArrowAmount = data.arrowAmount;
        currentBombAmount = data.bombAmount;
        currentKeyAmount = data.keyAmount;
        nowHasEspada = data.hasEspada;
        nowHasArco = data.hasArco;

        if (currentLevel == 0)
        {
            Debug.LogWarning("Save points to menu. Loading first playable level instead.");
            currentLevel = 1;
        }

        isLoadingFromSave = true;

        SceneManager.LoadScene(currentLevel);
    }


    public void LoadNextLevel()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(currentLevel);
    }

    private bool RefreshFromPlayer()
    {
        PlayerCharacter player = FindFirstObjectByType<PlayerCharacter>();
        if (player == null) return false;

        Life life = player.GetComponent<Life>();
        if (life == null) return false;

        currentPlayerPosition = player.transform.position;
        currentLife = life.currentLife;

        nowHasEspada = player.tieneEspada;
        nowHasArco = player.tieneArco;

        currentArrowAmount = player.cantidadFlechas;
        currentBombAmount = player.cantidadBombas;
        currentKeyAmount = player.cantidadLlaves;
        return true;
    }

    private bool ApplyToPlayer()
    {
        PlayerCharacter player = FindFirstObjectByType<PlayerCharacter>();
        if (player == null) return false;

        Life life = player.GetComponent<Life>();
        if (life == null) return false;

        player.transform.position = currentPlayerPosition;
        life.currentLife = currentLife;

        player.tieneEspada = nowHasEspada;
        player.tieneArco = nowHasArco;
        player.cantidadFlechas = currentArrowAmount;
        player.cantidadBombas = currentBombAmount;
        player.cantidadLlaves = currentKeyAmount;
        return true;
    }
}
