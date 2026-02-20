using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CambioDeZona : MonoBehaviour
{
    [Header("Configuración de Destino")]
    public string escenaDestino;
    public string idConexion;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si el jugador pisa este trigger invisible...
        if (collision.CompareTag("PiesPlayer"))
        {
            if (GestorPlayer.Instance != null)
            {
                GestorPlayer.Instance.ImpedirMovimiento();
            }

            StartCoroutine(Viajar());
        }
    }

    IEnumerator Viajar()
    {
        yield return new WaitForSeconds(0.5f);

        if (GestorPlayer.Instance != null)
        {
            GestorPlayer.Instance.idPuertaDestino = idConexion;
        }

        SceneManager.LoadScene(escenaDestino);
    }
}
