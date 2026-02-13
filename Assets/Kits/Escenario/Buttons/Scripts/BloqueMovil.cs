using UnityEngine;

public class BloqueMovil : MonoBehaviour, IActivable
{
    private bool estaActivo = false;
    [SerializeField] float speed = 2.0f;
    [SerializeField] Transform posFinal;

    // Update is called once per frame
    void Update()
    {
        //Al activarlo, se desplaza hacia la posición que debe llegar
        if (estaActivo && transform.position != posFinal.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, posFinal.position, speed * Time.deltaTime);
        }
    }

    public void ActivarElemento()
    {
        estaActivo = true;
    }
}
