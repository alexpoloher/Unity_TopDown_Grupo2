using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorPlayer : MonoBehaviour
{

    public static GestorPlayer Instance;
    private GameObject player;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }


    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.FindWithTag("Player");
    }
    public void ImpedirMovimiento()
    {
        if (player != null) {
            player.GetComponent<PlayerCharacter>().ImpedirMovimientos();
        }

    }
    public void PermitirMovimiento()
    {
        if (player != null) {
            player.GetComponent<PlayerCharacter>().Permitirmovimientos();
        }

    }

    public bool ComprobarSiJugadorTieneLlave()
    {
        return player.GetComponent<PlayerCharacter>().JugadorTieneLlaves();
    }

    public void ConsumirObjeto(DropDefinition.enumTipoObjeto tipoObjeto, int cantidad)
    {
        player.GetComponent<PlayerCharacter>().ConsumirObjeto(tipoObjeto, cantidad);
    }

}
