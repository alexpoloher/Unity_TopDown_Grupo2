using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GestorPlayer : MonoBehaviour
{

    public static GestorPlayer Instance;
    private PlayerCharacter playerScript;
    public string idPuertaDestino = "";

    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegistrarJugador(PlayerCharacter nuevoPlayer)
    {
        playerScript = nuevoPlayer;
    }

    public void ImpedirMovimiento()
    {
        if (playerScript != null) playerScript.ImpedirMovimientos();
    }

    public void PermitirMovimiento()
    {
        if (playerScript != null) playerScript.Permitirmovimientos();
    }

    public bool ComprobarSiJugadorTieneLlave()
    {
        if (playerScript != null)
        {
            return playerScript.JugadorTieneLlaves();
        }
        return false;
    }

    public void ConsumirObjeto(DropDefinition.enumTipoObjeto tipoObjeto, int cantidad)
    {
        if (playerScript != null)
        {
            playerScript.ConsumirObjeto(tipoObjeto, cantidad);
        }
    }
}
