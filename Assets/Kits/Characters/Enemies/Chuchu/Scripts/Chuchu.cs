using UnityEngine;

public class Chuchu : EnemyBase
{
    [Header("Jump")]
    [SerializeField] float jumpSpeed = 3f;
    [SerializeField] float jumpDuration = 0.5f;
    [SerializeField] float jumpCooldown = 1f;
    [SerializeField] float jumpDistance = 5f;

    private ChuchuStatus status;
    private float originalSpeed;
    new void Awake()
    {
        base.Awake();
        status = ChuchuStatus.Hiding;
        originalSpeed = linearSpeed;
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
                Move(towardsPlayerDirection);

                float distanceToPlayer = Vector3.Distance(playerPosition, transform.position);
                if (distanceToPlayer < jumpDistance)
                {
                    linearSpeed = jumpSpeed;
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
            Move(jumpDirection);
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
                    linearSpeed = originalSpeed;
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
