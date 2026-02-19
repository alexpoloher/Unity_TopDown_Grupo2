using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LogicaSeleccionCampeon : MonoBehaviour
{
    [Header("Character Prefabs")]
    public GameObject[] characterPrefabs;

    [Header("Spawn Point")]
    public Transform previewPoint;

    private int currentIndex = 0;
    private GameObject currentCharacter;

    public Button defaultButton;

    void Start()
    {
        ShowCharacter();
    }

    void OnEnable()
    {
        StartCoroutine(SelectButtonNextFrame()); 
    }

    IEnumerator SelectButtonNextFrame()
    {
        yield return null;

        EventSystem.current.SetSelectedGameObject(defaultButton.gameObject);
    }


    void ShowCharacter()
    {
        if (currentCharacter != null)
            Destroy(currentCharacter);

        currentCharacter = Instantiate(
            characterPrefabs[currentIndex],
            previewPoint.position,
            previewPoint.rotation,
            previewPoint
        );
    }

    public void NextCharacter()
    {
        currentIndex++;
        if (currentIndex >= characterPrefabs.Length)
            currentIndex = 0;

        ShowCharacter();
    }

    public void PreviousCharacter()
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = characterPrefabs.Length - 1;

        ShowCharacter();
    }

    public void ConfirmCharacter()
    {
        string selectedCharacterId = characterPrefabs[currentIndex].name.Replace("_Menu", "");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.characterId = selectedCharacterId;
            GameManager.Instance.currentLife = 5;
            GameManager.Instance.currentLevel = 1;
        }

        SceneManager.LoadScene(1);
    }
}
