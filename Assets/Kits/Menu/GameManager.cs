using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player State")]
    public string characterId;
    public int currentLife;
    public int currentLevel;

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
            return;
        }

        SaveGame();
        Debug.Log("He guardado el nivel " + currentLevel);
    }

    public void SaveGame()
    {
        SaveData data = new SaveData
        {
            characterId = characterId,
            level = currentLevel,
            life = currentLife
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
        currentLevel = data.level;

        if (currentLevel == 0)
        {
            Debug.LogWarning("Save points to menu. Loading first playable level instead.");
            currentLevel = 1;
        }

        isLoadingFromSave = true;

        Debug.Log($"Loaded save -> Character: {characterId}, Life: {currentLife}, Level: {currentLevel}");

        SceneManager.LoadScene(currentLevel);
    }


    public void LoadNextLevel()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(currentLevel);
    }
}
