using UnityEngine;
using static UnityEditor.PlayerSettings.SplashScreen;

public class BaseCharacter : MonoBehaviour, IVisible2D
{

    [SerializeField] int priority = 0;
    [SerializeField] IVisible2D.Side side;

    protected Animator animator;
    protected Rigidbody2D rb;

    //Movimiento
    [SerializeField] protected float linearSpeed = 1f;
    Vector2 lastMoveDirection;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        animator.SetFloat("HorizontalVelocity", lastMoveDirection.x); //Poniendo rawMove.x, hace que se oriente hacia donde apuntes, da igual hacia donde esté moviendose
        animator.SetFloat("VerticalVelocity", lastMoveDirection.y);
    }


    protected void Move(Vector2 direction)
    {
        rb.position += direction * linearSpeed * Time.deltaTime;
        lastMoveDirection = direction;
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
