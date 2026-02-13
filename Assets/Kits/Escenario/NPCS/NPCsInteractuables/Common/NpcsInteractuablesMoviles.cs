using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NpcsInteractuablesMoviles : NpcAnimalBase
{

    [SerializeField] GameObject pedirBotonInteractuar;
    [SerializeField] InputActionReference interactuar;
    private bool pedirBotonMostrandose;

    public FraseDialogo fraseDialogo;

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
            if (pedirBotonMostrandose && !mostrandoDialogo)
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


    private void CerrarDialogo()
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


    protected override void Update()
    {
        base.Update();

        //Si detecta al player cerca, le muestra el mensaje para inicial el diálogo
        bool estaCerca = Physics2D.OverlapCircle(transform.position, radioDeteccion, personaje);

        if (estaCerca) {
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
