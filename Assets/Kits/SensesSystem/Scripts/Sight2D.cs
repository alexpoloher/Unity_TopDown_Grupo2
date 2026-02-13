using UnityEngine;

public class Sight2D : MonoBehaviour
{

    [SerializeField] float radius = 5f;
    [SerializeField] float checkFrequency = 5f; //Checkea 5 veces por segundo
    [Space]
    [SerializeField] IVisible2D.Side[] perceivedSides;

    Transform closestTarget;
    float distanceToClosestTarget;
    int priorityOfClosestTarget;

    float lastCheckedTime = 0.0f;
    Collider2D[] colliders;

    // Update is called once per frame
    void Update()
    {
        //Así se llama 5 veces por segundo en lugar de 1 vez por fotograma
        if (Time.time - lastCheckedTime >= (1f/checkFrequency))
        {
            lastCheckedTime = Time.time;
            colliders = Physics2D.OverlapCircleAll(transform.position, radius);

            closestTarget = null;
            distanceToClosestTarget = Mathf.Infinity;
            priorityOfClosestTarget = -1;

            for (int i = 0; i < colliders.Length; i++) {
                IVisible2D visible = colliders[i].GetComponent<IVisible2D>();
                if (visible != null && CanSee(visible)) {

                    float distanceToPlayer = Vector3.Distance(transform.position, colliders[i].transform.position);
                    //En caso de que se encuentre otro player en rango (o un ayudante), si está más cerca el otro, se cambia de target
                    if ((visible.GetPriority() > priorityOfClosestTarget) || ((visible.GetPriority() == priorityOfClosestTarget) && (distanceToPlayer < distanceToClosestTarget) )) {
                        closestTarget = colliders[i].transform;
                        distanceToClosestTarget = distanceToPlayer;
                        priorityOfClosestTarget = visible.GetPriority();
                    }

                }
            }
        }
    }

    private bool CanSee(IVisible2D visible) {
        bool canSee = false;
        for (int i = 0; i < perceivedSides.Length && !canSee; i++) { 
            canSee = visible.GetSide() == perceivedSides[i];    //Calcula si lo puede ver. Porque en perceveidSides se pone cuáles son visibles por mi
        }
        return canSee;
    }

    public Transform GetClosestTarget()
    {
        return closestTarget;
    }

    public bool IsPlayerInSight()
    {
        bool valorRetorno = false;
        for (int i = 0; colliders != null && (i < colliders.Length) && !valorRetorno; i++)
        {
            if (colliders[i].CompareTag("Player")){
                valorRetorno = true;
            }
        }
        return valorRetorno;
    }
}
