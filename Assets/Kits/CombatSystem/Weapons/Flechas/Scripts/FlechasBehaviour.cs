using UnityEngine;

public class FlechasBehaviour : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float range = 3f;
    [SerializeField] float damage = 0.2f;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private float currentDistance = 0f;
    void Update()
    {
        if (currentDistance < range)
        {
            transform.position += transform.TransformDirection(Vector3.up) * speed * Time.deltaTime;
            currentDistance += speed * Time.deltaTime;
        } else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Life otherCharacterLife = collision.GetComponent<Life>();

        //Si ha golpeado a un jarrón, este se rompe
        JarronRompibleScript jarron = collision.GetComponent<JarronRompibleScript>();
        if (jarron != null)
        {
            jarron.NotifyHit();
        }

        ScriptPalanca palanca = collision.GetComponent<ScriptPalanca>();
        if (palanca != null)
        {
            palanca.NotifyHit();
        }

        if (!collision.CompareTag("Player") && !collision.CompareTag("PiesPlayer"))  // Si no es el jugador, entonces intenta hacer daño
        {
            otherCharacterLife?.OnHitReceived(damage);
            Destroy(gameObject); // Si choca y no es el jugador, se destruirá la flecha
        }
    }
}
