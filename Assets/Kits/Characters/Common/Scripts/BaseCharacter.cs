using UnityEngine;
using static UnityEditor.PlayerSettings.SplashScreen;

public class BaseCharacter : MonoBehaviour, IVisible2D
{

    [SerializeField] int priority = 0;
    [SerializeField] IVisible2D.Side side;

    Animator animator;
    Rigidbody2D rb;

    //Movimiento
    [SerializeField] float linearSpeed = 1f;
    Vector2 lastMoveDirection;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        animator.SetFloat("EjeX", lastMoveDirection.x); //Poniendo rawMove.x, hace que se oriente hacia donde apuntes, da igual hacia donde esté moviendose
        animator.SetFloat("EjeY", lastMoveDirection.y);
    }


    protected void Move(Vector2 direction)
    {
        rb.position += direction * linearSpeed * Time.deltaTime;
        if (direction != Vector2.zero)
        {
            lastMoveDirection = direction;
        }

        if (direction.magnitude > 0f)
        {
            animator.SetBool("IsMoving", true);
        }

        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    protected void Roll(float rollSpeed)
    {
        rb.position += lastMoveDirection * rollSpeed * Time.deltaTime;
    }

    internal void AplicarKnockback(float velocidadKnockback)
    {
        rb.position += lastMoveDirection * velocidadKnockback * Time.deltaTime;
    }

    public void NotifyHit()
    {
        Destroy(gameObject);
    }

    int IVisible2D.GetPriority()
    {
        return priority;
    }

    IVisible2D.Side IVisible2D.GetSide()
    {
        return side;
    }
}
