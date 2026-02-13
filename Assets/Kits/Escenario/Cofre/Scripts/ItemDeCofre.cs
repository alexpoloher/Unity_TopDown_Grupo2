using UnityEngine;

public class ItemDeCofre : MonoBehaviour
{

    [SerializeField] float velocidad = 1.5f;
    [SerializeField] float tiempoParaDesaparecer = 2f;
    private Vector3 posEnElInicio;

    private void OnEnable()
    {
        print("enable");
        posEnElInicio = transform.position;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + Vector3.up  * Time.deltaTime * velocidad;
    }
}
