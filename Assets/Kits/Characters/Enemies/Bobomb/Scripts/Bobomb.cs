using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bobomb : EnemyBase
{
    [SerializeField] Transform[] patrolRoute;
    [SerializeField] float chasingSpeed = 2f;
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
                    animator.SetTrigger("triggerChase");
                    chargeTimer = chargeCountdown;
                    linearSpeed = chasingSpeed;
                }
                else
                {
                    Vector3 movementDirection = (nextPointPosition - transform.position).normalized;
                    Move(movementDirection);
                    updateRoute();
                }
                break;

            case BobombStatus.Chasing:
                chargeTimer -= Time.deltaTime;
                if (chargeTimer < 0f)
                {
                    doCharge();
                }

                if (isAggro)
                {
                    Vector3 playerPosition = sight.GetClosestTarget().position;
                    Vector3 movementDirection = (playerPosition - transform.position).normalized;
                    Move(movementDirection);
                }
                else
                {
                    Move(Vector3.zero);
                }
                break;

            case BobombStatus.Charging:
                explosionTimer -= Time.deltaTime;
                if (explosionTimer < 0f)
                {
                    this.killEnemy();
                }
                break;

            case BobombStatus.Exploding:
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

    private void doCharge()
    {
        status = BobombStatus.Charging;
        animator.SetTrigger("triggerCharge");
        explosionTimer = explosionCountdown;
        Move(Vector3.zero);
    }
    protected override void recieveDamage(float dmg)
    {
        doCharge();
    }

    protected override void killEnemy()
    {
        status = BobombStatus.Exploding;
        animator.SetTrigger("triggerDeath");
        Destroy(gameObject, 0.75f); //La animacion de muerte dura 45/60 fotogramas
    }
}
