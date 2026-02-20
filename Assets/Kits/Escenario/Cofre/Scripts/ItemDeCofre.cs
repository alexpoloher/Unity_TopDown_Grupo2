using UnityEngine;

public class ItemDeCofre : MonoBehaviour
{

    [SerializeField] float velocidad = 1.5f;
    [SerializeField] float tiempoParaDesaparecer = 2f;
    private Vector3 posEnElInicio;

    private void OnEnable()
    {
        posEnElInicio = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + Vector3.up  * Time.deltaTime * velocidad;
    }
}
