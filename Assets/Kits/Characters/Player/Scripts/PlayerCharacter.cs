using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : BaseCharacter
{


    //Controles

    [Header("Controles")]
    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference punch;
    [SerializeField] InputActionReference dash;
    Vector2 rawMove;
    bool mustPunch;
    Vector2 punchDirection = Vector2.down;
    [Header("Punch data")]
    [SerializeField] float punchRadius = 0.3f;
    [SerializeField] float punchRange = 1f;

    [Header("Dash data")]
    [SerializeField] float dashVelocity = 5f; //O lo que veais



    Life life;

    private Vector2 lastDir;

    private Animator anim;

    private bool tieneEspada = false;
    private bool tieneArco = false;
    private int cantidadFlechas = 0;    //Flechas que tiene el player. En el GestorPLayer que permanece entre escenas, habrá que guardar esta info y rellenar eset campo al cargar una escena
    private int numMaxFlechas = 20;


    private int cantidadLlaves = 0; //Llaves que tiene el player. También debe guardar el gestor esto entre escenas

    private int cantidadBombas = 0;    //Bombas que tiene el player. En el GestorPLayer que permanece entre escenas, habrá que guardar esta info y rellenar eset campo al cargar una escena
    private int numMaxBombas = 10;

    private bool estaCayendo = false;

    private Vector3 ultimaPosEnSuelo;
    private float tiempoUltimaComprobacion;

    protected override void Awake()
    {
        base.Awake();
        life = GetComponent<Life>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        move.action.Enable();
        move.action.started += OnMove;
        move.action.performed += OnMove;
        move.action.canceled += OnMove;

        punch.action.Enable();
        punch.action.performed += OnPunch;

        dash.action.Enable();
        dash.action.performed += OnDash;
    }


    protected override void Update()
    {
        base.Update();
        Move(rawMove);

        if (mustPunch && tieneEspada)
        {
            mustPunch = false;
            PerformPunch();
        }

        GuardarPosSuelo();
    }

    private void GuardarPosSuelo()
    {
        if (!estaCayendo)
        {
            tiempoUltimaComprobacion += Time.deltaTime;

            if (tiempoUltimaComprobacion >= 0.5f)
            {
                tiempoUltimaComprobacion = 0.0f;
                ultimaPosEnSuelo = transform.position;
            }

        }

    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        Drop drop = elOtro.GetComponent<Drop>();
        if (drop != null) {
            if (drop.dropDefinition.tipoDeObjeto.Equals(DropDefinition.enumTipoObjeto.Vida)) {
                life.RecoverHealth(drop.dropDefinition.healthRecovery);
            }else if (drop.dropDefinition.tipoDeObjeto.Equals(DropDefinition.enumTipoObjeto.Balas))
            {
                cantidadFlechas += drop.dropDefinition.bullets;
                if (cantidadFlechas > numMaxFlechas)
                {
                    cantidadFlechas = numMaxFlechas;
                }
            }else if (drop.dropDefinition.tipoDeObjeto.Equals(DropDefinition.enumTipoObjeto.Llave))
            {
                //Pongo esto pero en principio la idea es que las llaves solo las saques de cofres
                cantidadLlaves++;
            }else if (drop.dropDefinition.tipoDeObjeto.Equals(DropDefinition.enumTipoObjeto.Bombas))
            {
                cantidadBombas += drop.dropDefinition.cantidadBombas;
                if (cantidadBombas > numMaxBombas)
                {
                    cantidadBombas = numMaxBombas;
                }

            }

            drop.NotifyPickUp();
        }
    }

    void PerformPunch() {
        //En lugar de usar colliders con trigger para los puñetazos
        //Lanza un círculo para comprobar si hay enemigo a la hora de haber ehcho el golpeo
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, punchRadius, punchDirection * punchRange);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider)
            {
                BaseCharacter otherBaseCharacter = hit.collider.GetComponent<BaseCharacter>();
                if(otherBaseCharacter != this)  //This es la referencia a este mismo
                {
                    otherBaseCharacter?.NotifyHit();
                }

                //Si ha golpeado a un jarrón, este se rompe
                JarronRompibleScript jarron = hit.collider.GetComponent<JarronRompibleScript>();
                if(jarron != null)
                {
                    jarron.NotifyHit();
                }

                ScriptPalanca palanca = hit.collider.GetComponent<ScriptPalanca>();
                if(palanca != null)
                {
                    palanca.NotifyHit();
                }


            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, punchDirection * punchRange);
    }


    private void OnDisable()
    {
        move.action.Disable();
        move.action.started -= OnMove;
        move.action.performed -= OnMove;
        move.action.canceled -= OnMove;

        punch.action.Disable();
        punch.action.performed -= OnPunch;

        dash.action.Disable();
        dash.action.performed -= OnDash;
    }


    private void OnMove(InputAction.CallbackContext context)
    {
        rawMove = context.action.ReadValue<Vector2>();  //Lee le valor de la acción que lo ha llamado, indicando que esperamos leer un Vector2

        ////En caso de que te muevas y no estes quieto (lo de 0f), se guarda a qué pos es la última a la que te moviste, para saber a donde está mirando el personaje
        //if (rawMove.magnitude > 0f)
        //{
        //    Vector2 punchDirection = rawMove.normalized;
        //}
        //Innecesario, está en el BaseCharacter
    }

    private void OnPunch(InputAction.CallbackContext context)
    {
        //Se indica que debe golpear
        mustPunch = true;
        anim.SetBool("mustPunch", true);
    }


    private void OnDash(InputAction.CallbackContext context)
    {
        anim.SetBool("Dash", true);
    }


    //Al abrir un cofre o que un Npc te de un objeto, se llama a este método
    public void RecibirItemPlayer(DropDefinition item)
    {
        print("recibe item");
        switch (item.tipoDeObjeto)
        {
            case DropDefinition.enumTipoObjeto.Vida:
                life.RecoverHealth(item.healthRecovery);
                break;
            case DropDefinition.enumTipoObjeto.Balas:
                cantidadFlechas += item.bullets;
                if (cantidadFlechas > numMaxFlechas)
                {
                    cantidadFlechas = numMaxFlechas;
                }
                break;
            case DropDefinition.enumTipoObjeto.Espada:
                tieneEspada = true;
                break;
            case DropDefinition.enumTipoObjeto.Llave:
                print("coge llave");
                cantidadLlaves++;
                break;
            case DropDefinition.enumTipoObjeto.Bombas:
                print("Bombas");
                cantidadBombas += item.cantidadBombas;
                if (cantidadBombas > numMaxBombas)
                {
                    cantidadBombas = numMaxBombas;
                }
                break;
            case DropDefinition.enumTipoObjeto.Arco:
                print("Recibe arco");
                tieneArco = true;
                break;
        }
    }



    public void ImpedirMovimientos()
    {
        move.action.Disable();
        punch.action.Disable();
    }

    public void Permitirmovimientos()
    {
        move.action.Enable();
        punch.action.Enable();

    }

    public bool JugadorTieneLlaves()
    {
        if(cantidadLlaves > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ConsumirObjeto(DropDefinition.enumTipoObjeto tipoObjeto, int cantidadConsumida)
    {
        switch (tipoObjeto)
        {
            case DropDefinition.enumTipoObjeto.Llave:
                cantidadLlaves = cantidadLlaves - cantidadConsumida;
                break;
        }
    }

    public void TocarHueco(Vector3 posHueco)
    {
        StartCoroutine(CaerPorHueco(posHueco));
    }

    IEnumerator CaerPorHueco(Vector3 posHueco)
    {
        estaCayendo = true;
        GestorPlayer.Instance.ImpedirMovimiento();
        //cayendo = true;
        transform.position = new Vector3(posHueco.x, posHueco.y, transform.position.z); //Se desplaza al jugador a la posición del hueco para dar mejor efecto
        //gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;

        life.OnHitReceived(0.25f);


        Vector3 escalaOriginal = transform.localScale;
        //Cada 0.1 segs, se va reduciendo la escala hasta que desaparezca
        for (float nuevaEscala = 1f; nuevaEscala >= 0; nuevaEscala = nuevaEscala - 0.1f)
        {
            transform.localScale = new Vector3(nuevaEscala, nuevaEscala, nuevaEscala);
            yield return new WaitForSeconds(0.1f);
        }
        transform.localScale = Vector3.zero;
        yield return new WaitForSeconds(2f);
        Reaparecer(escalaOriginal);

    }

    private void Reaparecer(Vector3 escalaOriginal)
    {
        estaCayendo = false;
        transform.position = ultimaPosEnSuelo;
        transform.localScale = escalaOriginal;
        GestorPlayer.Instance.PermitirMovimiento();
    }

}
