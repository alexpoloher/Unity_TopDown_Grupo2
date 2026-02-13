using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControladorDialogos : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI textoPantalla;
    private Animator animator;
    private Queue<string> colaDialogos = new Queue<string>();   //Una cola para gestionar por orden los textos de los diálogos
    Textos texto;
    public Action cerrarCuadro;
    private bool escribiendoTexto = false;

    private int indiceTextoActual = 0;
    private FraseDialogo fraseActual;

    [SerializeField] GameObject botonesOpcionesEnCanvas;
    [SerializeField] GameObject botonOpcion1;
    [SerializeField] Button[] botonesOpciones;

    private bool estaAbierto = false;
    //private bool mostrandoOpciones = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    private void OnEnable()
    {
        botonesOpciones[0].onClick.RemoveAllListeners();
        botonesOpciones[1].onClick.RemoveAllListeners();
        //mostrandoOpciones = false;
    }

    public void ActivarCartel(FraseDialogo fraseInicial)
    {
        colaDialogos.Clear();
        fraseActual = fraseInicial;
        EventSystem.current.SetSelectedGameObject(null);

        print(fraseActual.opciones.Length);
        if (!estaAbierto) {
            animator.SetBool("AbrirCartel", true);
        }
        estaAbierto = true;

        indiceTextoActual = 0;
        //mostrandoOpciones = false;
        MostrarSigFrase();

    }

    private void MostrarTexto()
    {
        //fraseActual = texto.frases[indiceTextoActual];
        StartCoroutine(MostrarCaracteres(fraseActual.textos[indiceTextoActual]));
    }

    public void MostrarSigFrase()
    {

        if (escribiendoTexto == false)
        {
            if (fraseActual.opciones.Length > 0)
            {
                /*if (mostrandoOpciones == false)
                {*/
                    MostrarTexto();
                    MostrarOpciones(fraseActual.opciones);
                /*}*/
            }
            else
            {
                botonesOpcionesEnCanvas.gameObject.SetActive(false);
                //Si hay más frases en este objeto de 
                if (indiceTextoActual < fraseActual.textos.Length)
                {
                    MostrarTexto();
                    indiceTextoActual++;
                }
                else
                {
                    if(fraseActual.sigFrase == null)
                    {
                        CerrarCuadroDialogo();
                    }
                    else
                    {
                        ActivarCartel(fraseActual.sigFrase);
                    }

                }

            }
        }


    }


    public void VerSiguienteFrase()
    {
        //Solo se detecta el Enter del plyaer si no se está esperando a que pulse un botón
        if(fraseActual.opciones.Length == 0 || fraseActual.opciones == null)
        {
            MostrarSigFrase();
        }
    }

    private void MostrarOpciones(OpcionesDialogo[] opciones) {

        //mostrandoOpciones = true;

        for (int i = 0; i < opciones.Length; i++)
        {
            botonesOpciones[i].onClick.RemoveAllListeners();
        }

            botonesOpcionesEnCanvas.gameObject.SetActive(true);
        for (int i = 0; i < opciones.Length; i++) {
            botonesOpciones[i].GetComponentInChildren<TextMeshProUGUI>().text = opciones[i].textoOpcion;
            
            OpcionesDialogo opcionEscogida = opciones[i];

            botonesOpciones[i].onClick.AddListener(() => {
                ActivarCartel(opcionEscogida.siguienteFrase);
                print("lo pulsa");
            });
        
        
        }


    } 


    public void CerrarCuadroDialogo()
    {
        animator.SetBool("AbrirCartel", false);
        cerrarCuadro?.Invoke();
        estaAbierto = false;
    }

    IEnumerator MostrarCaracteres(string texto)
    {
        textoPantalla.text = "";
        escribiendoTexto = true;
        foreach (char character in texto.ToCharArray())
        {
            textoPantalla.text += character;
            yield return new WaitForSeconds(0.02f);
        }
        escribiendoTexto = false;
    }
}
