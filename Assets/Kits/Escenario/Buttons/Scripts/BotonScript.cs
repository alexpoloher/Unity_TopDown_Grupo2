using UnityEngine;
using Unity.Cinemachine;
using System.Collections;
public class BotonScript : MonoBehaviour
{

    private Animator animator;
    private bool estaPulsado = false;

    [SerializeField] MonoBehaviour[] elementosQueActiva;
    [SerializeField] CinemachineCamera camaraPlayer;
    [SerializeField] CinemachineCamera camaraBoton;
    [SerializeField] float tiempoCambioCamara;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("PiesPlayer"))
        {
            animator.SetTrigger("Activado");
            if (!estaPulsado)
            {
                PulsarBoton();
            }
        }
    }

    //Al pulsar un botón se blendeará con la cámara que enfoca al elemento activado
    private void PulsarBoton()
    {
        estaPulsado = true;
        if (camaraBoton != null && camaraPlayer != null)
        {
            camaraPlayer.Priority = 0;
            camaraBoton.Priority = 10;
            StartCoroutine(VolverACamaraJugador());
        }


        foreach (MonoBehaviour elemento in elementosQueActiva)
        {
            //Se comprueba que es un elemento de la intefaz activar, antes de ejecutar esa función
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
