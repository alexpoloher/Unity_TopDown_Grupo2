using UnityEngine;

public class Chuchu : EnemyBase
{
    [Header("Jump")]
    [SerializeField] float jumpSpeed = 50f;
    [SerializeField] float jumpDuration = 0.5f;
    [SerializeField] float jumpCooldown = 1f;
    [SerializeField] float jumpDistance = 5f;

    private ChuchuStatus status;
    new void Awake()
    {
        base.Awake();
        status = ChuchuStatus.Hiding;
    }

    private Vector3 jumpDirection;
    private float idleTimer = 0f;
    private float jumpTimer = 0f;
    new void Update()
    {
        base.Update();
        switch (status)
        {
        case ChuchuStatus.Hiding:
            if (isAggro)
            {
                status = ChuchuStatus.Idle;
                idleTimer = 0.2f;
                print("Animacion rise");
            }
            break;

        case ChuchuStatus.Walking:
            if (isAggro)
            {
                //Movimiento normal
                Vector3 playerPosition = sight.GetClosestTarget().position;
                Vector3 towardsPlayerDirection = (playerPosition - transform.position).normalized;
                rb.linearVelocity = towardsPlayerDirection * linearSpeed * Time.deltaTime;

                float distanceToPlayer = Vector3.Distance(playerPosition, transform.position);
                if (distanceToPlayer < jumpDistance)
                {
                    jumpDirection = towardsPlayerDirection;
                    status = ChuchuStatus.Jumping;
                    jumpTimer = jumpDuration;
                    print("Animacion jump");
                }
            }
            else
            {
                doIdleStart();
            }

            break;

        case ChuchuStatus.Jumping:
            jumpTimer -= Time.deltaTime;
            //Movimiento por salto
            rb.linearVelocity = jumpDirection * jumpSpeed * Time.deltaTime;
            if (jumpTimer < 0f)
            {
                doIdleStart();
                print("Animacion land");
            }
            break;

        case ChuchuStatus.Idle:
            idleTimer -= Time.deltaTime;
            if (idleTimer < 0f)
            {
                if (isAggro)
                {
                    status = ChuchuStatus.Walking;
                    print("Animacion walk");
                }
                else
                {
                    status = ChuchuStatus.Hiding;
                    print("Animacion hide");
                }
            }
            break;
        }
    }

    void doIdleStart()
    {
        status = ChuchuStatus.Idle;
        rb.linearVelocity = Vector3.zero;
        idleTimer = jumpCooldown;
    }
}
