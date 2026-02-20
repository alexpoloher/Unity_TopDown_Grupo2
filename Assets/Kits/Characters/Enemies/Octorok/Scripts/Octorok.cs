using UnityEngine;

public class Octorok : EnemyBase
{
    [SerializeField] float walkCooldown = 1f;
    [Header("Shoot")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float shootCooldown = 0.5f;

    private OctorokStatus status;

    new void Awake()
    {
        base.Awake();
        status = OctorokStatus.Idle;
    }

    private Vector3 movementDirection;
    private float walkTimer = 0f;
    private float shootTimer = 0f;
    new void Update()
    {
        base.Update();
        switch (status)
        {
            case OctorokStatus.Idle:
                if (isAggro)
                {
                    Vector3 playerPosition = sight.GetClosestTarget().position;
                    //Movimiento en direccion contraria al player
                    movementDirection = (transform.position - playerPosition).normalized;
                    status = OctorokStatus.Walking;
                    walkTimer = walkCooldown;
                }
                break;

            case OctorokStatus.Walking:
                walkTimer -= Time.deltaTime;
                Move(movementDirection);
                if (walkTimer < 0f)
                {
                    Move(Vector2.zero);
                    if (isAggro)
                    {
                        doShoot();
                    }
                    else
                    {
                        status = OctorokStatus.Idle;
                    }
                }
                break;

            case OctorokStatus.Recharging:
                shootTimer -= Time.deltaTime;
                if (shootTimer < 0f)
                {
                    status = OctorokStatus.Idle;
                }
                break;
        }
    }

    private void doShoot()
    {
        animator.SetTrigger("triggerShoot");
        Vector3 playerPosition = sight.GetClosestTarget().position;
        Vector3 towardsPlayerDirection = (playerPosition - transform.position).normalized;
        OctorokProjectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<OctorokProjectile>();
        projectile.setDirection(towardsPlayerDirection);

        shootTimer = shootCooldown;
        status = OctorokStatus.Recharging;
    }
}
