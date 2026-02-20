using System.Collections;
using UnityEngine;

public class PuntoSpawn : MonoBehaviour
{
    [Header("ID de este punto (Ej: SalidaCasa1)")]
    public string idConexion;

    IEnumerator Start()
    {
        // Le damos 0.1 segundos de cortes�a para que Unity cargue todo bien
        yield return new WaitForSeconds(0.1f);

        if (GestorPlayer.Instance != null)
        {
            if (GestorPlayer.Instance.idPuertaDestino == idConexion)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");

                if (player != null)
                {
                    Debug.Log($"[SPAWN] �Jugador encontrado! Teletransportando a {transform.position}");

                    // --- EL TRUCO DE LAS F�SICAS ---
                    Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        // Le decimos a la f�sica directamente d�nde tiene que ir
                        rb.position = transform.position;
                        rb.linearVelocity = Vector2.zero; // Frenamos cualquier inercia que trajera
                    }

                    // Movemos el transform por si acaso
                    player.transform.position = transform.position;

                    GestorPlayer.Instance.PermitirMovimiento();
                    GestorPlayer.Instance.idPuertaDestino = "";
                }
            }
        }
    }
}