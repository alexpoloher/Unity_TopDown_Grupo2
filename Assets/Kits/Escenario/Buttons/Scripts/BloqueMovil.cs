using UnityEngine;

public class BloqueMovil : MonoBehaviour, IActivable
{
    private bool estaActivo = false;
    [SerializeField] float speed = 2.0f;
    [SerializeField] Transform posFinal;
    [SerializeField] Transform posInicial;
    private bool haciaPos2 = false;


    // Update is called once per frame
    void Update()
    {
        //Al activarlo, se desplaza hacia la posición que debe llegar

        if (haciaPos2 == true)
        {
            if (estaActivo && transform.position != posFinal.position)
            {
                transform.position = Vector2.MoveTowards(transform.position, posFinal.position, speed * Time.deltaTime);
            }
        }
        else{
            if (estaActivo && transform.position != posInicial.position)
            {
                print(posInicial.position);
                transform.position = Vector2.MoveTowards(transform.position, posInicial.position, speed * Time.deltaTime);
            }
        }

    }

    public void ActivarElemento()
    {
        estaActivo = true;
        haciaPos2 = !haciaPos2;
    }
}
