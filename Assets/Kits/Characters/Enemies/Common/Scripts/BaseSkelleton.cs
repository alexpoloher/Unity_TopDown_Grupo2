using UnityEngine;

public class BaseSkelleton : BaseCharacter
{

    Sight2D sight;


    protected override void Awake()
    {
        base.Awake();
        sight = GetComponent<Sight2D>();
    }

    protected override void Update()
    {
        base.Update();
        Transform closestTarget = sight.GetClosestTarget();
        //Se mueve hacia la dirección en la que está el objetivo
        if (closestTarget != null) {
            Move((closestTarget.position - transform.position).normalized);
        }
    }
}
