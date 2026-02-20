using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class ScriptPalanca : MonoBehaviour
{

    [SerializeField] MonoBehaviour[] elementosQueActiva;
    [SerializeField] CinemachineCamera camaraPlayer;
    [SerializeField] CinemachineCamera camaraBoton;
    [SerializeField] float tiempoCambioCamara;

    Animator animator;
    [SerializeField] AudioClip sonidoActivar;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    //La palanca se activa al ser golpeada
    public void NotifyHit()
    {
        animator.SetTrigger("Activar");
        GestorSonido.Instance.EjecutarSonido(sonidoActivar);
        if (camaraBoton != null && camaraPlayer != null)
        {
            camaraPlayer.Priority = 0;
            camaraBoton.Priority = 10;
            StartCoroutine(VolverACamaraJugador());
        }

        foreach (MonoBehaviour elemento in elementosQueActiva)
        {
            if (elemento is IActivable elementoActivable)
            {
                elementoActivable.ActivarElemento();
            }

        }
    }

    IEnumerator VolverACamaraJugador()
    {
        GestorPlayer.Instance.ImpedirMovimiento();
        yield return new WaitForSeconds(tiempoCambioCamara);
        camaraBoton.Priority = 0;
        camaraPlayer.Priority = 10;
        yield return new WaitForSeconds(1.5f);
        GestorPlayer.Instance.PermitirMovimiento();
    }
}
