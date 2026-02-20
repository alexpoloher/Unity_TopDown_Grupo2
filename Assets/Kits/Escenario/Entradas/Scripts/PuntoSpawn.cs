using System.Collections;
using UnityEngine;

public class PuntoSpawn : MonoBehaviour
{
    [Header("ID de este punto (Ej: SalidaCasa1)")]
    public string idConexion;

    IEnumerator Start()
    {
        // 1. ESPERAMOS un frame (o un poquito de tiempo) para asegurar que el Player ha llegado
        yield return new WaitForEndOfFrame();
        // Si sigue fallando, prueba a cambiar la línea de arriba por: yield return new WaitForSeconds(0.1f);

        if (GestorPlayer.Instance != null)
        {
            // CHIVATO 1: Para ver si los nombres coinciden realmente (Cuidado con los espacios en blanco)
            Debug.Log($"[SPAWN] Evaluando... Mi ID es '{idConexion}' y el Manager busca '{GestorPlayer.Instance.idPuertaDestino}'");

            if (GestorPlayer.Instance.idPuertaDestino == idConexion)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");

                if (player != null)
                {
                    // CHIVATO 2: ¡Lo encontró!
                    Debug.Log($"[SPAWN] ¡Jugador encontrado! Teletransportando a {transform.position}");

                    // Lo movemos
                    player.transform.position = transform.position;

                    GestorPlayer.Instance.PermitirMovimiento();

                    // Limpiamos el destino para que no se teletransporte por error luego
                    GestorPlayer.Instance.idPuertaDestino = "";
                }
                else
                {
                    Debug.LogError("[SPAWN ERROR] Los IDs coinciden, pero no encuentro ningún objeto con el Tag 'Player'. ¿Seguro que tiene el Tag puesto?");
                }
            }
        }
        else
        {
            Debug.LogError("[SPAWN ERROR] No encuentro el GestorPlayer.Instance.");
        }
    }
}