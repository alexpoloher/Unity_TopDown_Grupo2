using UnityEngine;

public class Bobomb : EnemyBase
{
    [SerializeField] Transform[] patrolRoute;
    [SerializeField] float chasingSpeed = 20f;
    [SerializeField] float chargeCountdown = 5f;
    [SerializeField] float explosionCountdown = 0.5f;
    [SerializeField] float explosionDistance = 0.1f;

    private BobombStatus status;
    private int nextPointIndex;
    private Vector3 nextPointPosition;
    new void Awake()
    {
        base.Awake();
        status = BobombStatus.Patroling;
        nextPointIndex = 0;
        nextPointPosition = patrolRoute[0].position;
    }

    private float chargeTimer = 0f;
    private float explosionTimer = 0f;
    new void Update()
    {
        base.Update();
        switch (status)
        {
            case BobombStatus.Patroling:
                if (isAggro)
                {
                    status = BobombStatus.Chasing;
                    chargeTimer = chargeCountdown;
                }
                else
                {
                    Vector3 movementDirection = (nextPointPosition - transform.position).normalized;
                    rb.linearVelocity = movementDirection * linearSpeed * Time.deltaTime;
                    updateRoute();
                }
                break;

            case BobombStatus.Chasing:
                chargeTimer -= Time.deltaTime;
                if (chargeTimer < 0f)
                {
                    status = BobombStatus.Charging;
                    explosionTimer = explosionCountdown;
                    rb.linearVelocity = Vector3.zero;
                }

                if (isAggro)
                {
                    Vector3 playerPosition = sight.GetClosestTarget().position;
                    Vector3 movementDirection = (playerPosition - transform.position).normalized;
                    rb.linearVelocity = movementDirection * chasingSpeed * Time.deltaTime;
                }
                else
                {
                    rb.linearVelocity = Vector3.zero;
                }
                break;

            case BobombStatus.Charging:
                explosionTimer -= Time.deltaTime;
                if (explosionTimer < 0f)
                {
                    doExplode();
                }
                break;
        }
    }

    private void updateRoute()
    {
        if (Vector3.Distance(nextPointPosition, transform.position) < 0.1f)
        {
            nextPointIndex++;
            if (nextPointIndex >= patrolRoute.Length)
            {
                nextPointIndex = 0;
            }
            nextPointPosition = patrolRoute[nextPointIndex].position;
        }
    }

    private void doExplode()
    {
        //Daño en area, efecto
        killEnemy();
    }
}
