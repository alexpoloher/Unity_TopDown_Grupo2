using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BloqueLlaveScript : MonoBehaviour
{
    [SerializeField] InputActionReference interactuar;
    [SerializeField] float radioDeteccion = 0.5f;
    [SerializeField] LayerMask personaje;
    protected PlayerCharacter playerRef;

    Animator animator;

    DropDefinition.enumTipoObjeto tipoObjetoConsume = DropDefinition.enumTipoObjeto.Llave;
    private int cantidadConsume;
    private bool puedeIntentarAbrir = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        interactuar.action.Enable();
        interactuar.action.started += OnComprobarAbrir;
    }

    private void OnDisable()
    {
        interactuar.action.started -= OnComprobarAbrir;
    }

    // Update is called once per frame
    void Update()
    {
        //Si detecta al player cerca, le muestra el mensaje para inicial el diálogo
        bool estaCerca = Physics2D.OverlapCircle(transform.position, radioDeteccion, personaje);


        if (estaCerca)
        {
            if (playerRef == null)
            {
                Collider2D player = Physics2D.OverlapCircle(transform.position, radioDeteccion, personaje);
                playerRef = player.gameObject.GetComponentInParent<PlayerCharacter>();
            }
            puedeIntentarAbrir = true;
        }
        else {
            puedeIntentarAbrir = false;
        }

    }

    private void OnComprobarAbrir(InputAction.CallbackContext context)
    {
        bool tieneLlave = false;
        if (puedeIntentarAbrir)
        {
            if (GestorPlayer.Instance != null)
            {
                tieneLlave = GestorPlayer.Instance.ComprobarSiJugadorTieneLlave();
                print(tieneLlave);
            }

            if (tieneLlave)
            {
                animator.SetTrigger("Abrir");
                GestorPlayer.Instance.ConsumirObjeto(tipoObjetoConsume, cantidadConsume);
            }
        }

    }

    public void DestruirBloque()
    {
        Destroy(gameObject);
    }
}
