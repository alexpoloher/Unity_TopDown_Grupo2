using System.Collections;
using UnityEngine;

public class NpcAnimalBase : MonoBehaviour
{

    [SerializeField] protected Transform[] puntos;
    [SerializeField] float velocidad = 1f;
    [SerializeField] float tiempoEsperaPatrulla = 2f;
    [SerializeField] bool miraDerechaIni;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    private Transform proxDestino;
    protected int indiceDestActual = -1;

    private bool esperandoPatrulla = false;

    [Header("Deteccion Jugador")]
    private bool jugadorCerca;
    private bool esperaQueJugadorSeAleje = false;
    [SerializeField] protected float radioDeteccion = 0.5f;
    [SerializeField] protected LayerMask personaje;


    protected Animator animator;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        proxDestino = ElegirDestino();
        animator.SetBool("Andando", true);
    }


    protected virtual Transform ElegirDestino()
    {
        int indiceDestino = indiceDestActual;

        while (indiceDestino == indiceDestActual) { 
            indiceDestino = Random.Range(0, puntos.Length);
        }
        indiceDestActual = indiceDestino;

        if (transform.position.x - puntos[indiceDestino].position.x < 0.0f)
        {
            if (!miraDerechaIni)
            {
                spriteRenderer.flipX = true;
            }
            else {
                spriteRenderer.flipX = false;
            }

        }
        else {
            if (!miraDerechaIni)
            {
                spriteRenderer.flipX = false;
            }
            else {
                spriteRenderer.flipX = true;
            }

        }

            return puntos[indiceDestino];
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        jugadorCerca = Physics2D.OverlapCircle(transform.position, radioDeteccion, personaje);
        if (jugadorCerca)
        {
            if (!esperaQueJugadorSeAleje)
            {
                animator.SetBool("Andando", false);
            }
            esperaQueJugadorSeAleje = true;
        }
        else
        {
            esperaQueJugadorSeAleje = false;
            if (!esperandoPatrulla)
            {
                animator.SetBool("Andando", true);
            }

        }

        if (!esperandoPatrulla && !esperaQueJugadorSeAleje)
        {
            MoverseADestino();
        }
    }

    private void MoverseADestino()
    {
        Vector2 nuevaPos = Vector2.MoveTowards(rb.position, proxDestino.position, velocidad * Time.deltaTime);
        rb.MovePosition(nuevaPos);
        if (Vector2.Distance(rb.position, proxDestino.position) < 0.1f)
        {
            StartCoroutine(EsperarEnElDestino());
        }



    }

    IEnumerator EsperarEnElDestino()
    {
        animator.SetBool("Andando", false);
        esperandoPatrulla = true;
        yield return new WaitForSeconds(tiempoEsperaPatrulla);
        esperandoPatrulla = false;
        proxDestino = ElegirDestino();
        if (!esperaQueJugadorSeAleje)
        {
            animator.SetBool("Andando", true);
        }


    }

}
