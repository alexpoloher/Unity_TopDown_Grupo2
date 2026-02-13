using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class NpcsInteractuablesQuietos : MonoBehaviour
{

    [SerializeField] GameObject pedirBotonInteractuar;
    [SerializeField] InputActionReference interactuar;
    private bool pedirBotonMostrandose;
    [SerializeField] float radioDeteccion = 0.5f;
    [SerializeField] LayerMask personaje;
    protected PlayerCharacter playerRef;

    [SerializeField] protected FraseDialogo fraseDialogo;
    private bool abriendoDialogo = false;
    private float delayDialogoInicio = 0.5f;
    private bool mostrandoDialogo = false;
    [SerializeField] Image imagenDialogo;
    private void OnEnable()
    {
        interactuar.action.Enable();
        interactuar.action.started += OnHablar;
    }

    private void OnDisable()
    {
        interactuar.action.started -= OnHablar;
    }

    private void OnHablar(InputAction.CallbackContext context)
    {
        if (!abriendoDialogo)
        {
            if(pedirBotonMostrandose && !mostrandoDialogo)
            {
                pedirBotonMostrandose = false;
                pedirBotonInteractuar.gameObject.SetActive(false);


                imagenDialogo.gameObject.SetActive(true);
                Object.FindFirstObjectByType<ControladorDialogos>().ActivarCartel(fraseDialogo);
                StartCoroutine(AbriendoDialogo());
                mostrandoDialogo = true;
                Object.FindFirstObjectByType<ControladorDialogos>().cerrarCuadro += CerrarDialogo;
                GestorPlayer.Instance.ImpedirMovimiento();
            }
            else if (mostrandoDialogo == true) //Se entra aquí si está mostrando ay el diálogo para pasar a la siguiente frase
            {
                Object.FindFirstObjectByType<ControladorDialogos>().VerSiguienteFrase();
            }

        }
    }


    protected virtual void CerrarDialogo()
    {
        Object.FindFirstObjectByType<ControladorDialogos>().cerrarCuadro -= CerrarDialogo;
        GestorPlayer.Instance.PermitirMovimiento();
        mostrandoDialogo = false;

        pedirBotonMostrandose = true;
        pedirBotonInteractuar.gameObject.SetActive(true);

    }

    IEnumerator AbriendoDialogo()
    {
        abriendoDialogo = true;
        yield return new WaitForSeconds(delayDialogoInicio);
        abriendoDialogo = false;
    }


    // Update is called once per frame
    void Update()
    {
        //Si detecta al player cerca, le muestra el mensaje para inicial el diálogo
        bool estaCerca = Physics2D.OverlapCircle(transform.position, radioDeteccion, personaje);


        if (estaCerca && !mostrandoDialogo)
        {
            if(playerRef == null)
            {
                Collider2D player = Physics2D.OverlapCircle(transform.position, radioDeteccion, personaje);
                playerRef = player.gameObject.GetComponentInParent<PlayerCharacter>();
            }
            pedirBotonMostrandose = true;
            pedirBotonInteractuar.gameObject.SetActive(true);
        }
        else
        {
            pedirBotonMostrandose = false;
            pedirBotonInteractuar.gameObject.SetActive(false);
        }
    }
}
