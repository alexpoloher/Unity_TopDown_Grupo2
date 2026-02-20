using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CofreScript : MonoBehaviour
{

    [SerializeField] GameObject pedirBotonInteractuar;
    [SerializeField] InputActionReference interactuar;
    [SerializeField] LayerMask personaje;
    [SerializeField] float radioDeteccion = 1f;
    [SerializeField] Vector2 tamanioOverlap = new Vector2(0.35f, 0.75f);

    [SerializeField] DropDefinition itemContenido;

    [SerializeField] GameObject refItemContenido;
    [SerializeField] float tiempoParaDesaparecerItem = 2f;

    [SerializeField] private bool tieneQueEsperar = false;

    [SerializeField] AudioClip sonidoAbrir;

    private bool pedirBotonMostrandose;
    private bool estaEnRango;
    private bool estaAbierto = false;
    Animator animator;

    private PlayerCharacter playerRef;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        interactuar.action.Enable();
        interactuar.action.started += OnInteractuar;
    }

    private void OnDisable()
    {
        interactuar.action.started -= OnInteractuar;
    }


    private void OnInteractuar(InputAction.CallbackContext context)
    {
        if (!estaAbierto && pedirBotonMostrandose == true)
        {
            animator.SetBool("Abierto", true);
            estaAbierto = true;
            GestorSonido.Instance.EjecutarSonido(sonidoAbrir);

            pedirBotonMostrandose = false;
            estaEnRango = false;
            pedirBotonInteractuar.gameObject.SetActive(false);

            //Se muestra el contenido del cofre, y tras un rato, se le aplica el efecto al jugador
            if (!tieneQueEsperar)
            {
                refItemContenido.SetActive(true);
                StartCoroutine(DarObjetoAlPlayer());
            }

        }

    }


    IEnumerator DarObjetoAlPlayer()
    {

        yield return new WaitForSeconds(tiempoParaDesaparecerItem);
        refItemContenido.SetActive(false);
        if (playerRef != null)
        {
            playerRef.RecibirItemPlayer(itemContenido);
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (!estaAbierto)
        {
            //estaEnRango = Physics2D.OverlapCircle(transform.position, radioDeteccion, personaje);
            estaEnRango = Physics2D.OverlapBox(transform.position, tamanioOverlap, 0f, personaje);
            if (estaEnRango)
            {
                //Collider2D player = Physics2D.OverlapCircle(transform.position, radioDeteccion, personaje);
                Collider2D player = Physics2D.OverlapBox(transform.position, tamanioOverlap, 0f, personaje);
                playerRef = player.gameObject.GetComponentInParent<PlayerCharacter>();
                if (player.transform.position.y < transform.position.y)
                {
                    pedirBotonMostrandose = true;
                    pedirBotonInteractuar.gameObject.SetActive(true);
                }
                else
                {
                    pedirBotonMostrandose = false;
                    pedirBotonInteractuar.gameObject.SetActive(false);
                }

            }
            else
            {
                pedirBotonMostrandose = false;
                pedirBotonInteractuar.gameObject.SetActive(false);
            }
        }

    }

    //Si no se hizo antes porque debía esperar. Esste se llama por la animación
    public void EntregarObjeto()
    {
        refItemContenido.SetActive(true);
        StartCoroutine(DarObjetoAlPlayer());
    }
}
