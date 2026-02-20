using System.Collections;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

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
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            //No hay destino
            if (GestorPlayer.Instance.idPuertaDestino == null)
            {
                PuntoSpawn primerSpawn = FindFirstObjectByType<PuntoSpawn>();
                if (primerSpawn != null)
                {
                    Teletransportar(player, primerSpawn.transform.position);
                }
                yield break;
            }

            //Hay destino
            if (GestorPlayer.Instance.idPuertaDestino == idConexion)
            {
                Debug.Log($"[SPAWN] �Jugador encontrado! Teletransportando a {transform.position}");
                
                Teletransportar(player, transform.position);
                GestorPlayer.Instance.idPuertaDestino = "";

            }
        }
    }


    void Teletransportar(GameObject player, Vector3 posicion)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.position = posicion;
            rb.linearVelocity = Vector2.zero;
        }

        player.transform.position = posicion;

        if (GestorPlayer.Instance != null)
            GestorPlayer.Instance.PermitirMovimiento();
    }            
}