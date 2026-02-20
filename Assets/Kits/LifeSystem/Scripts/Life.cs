using System;
using UnityEngine;
using UnityEngine.Events;

public class Life : MonoBehaviour
{
    [SerializeField] float startingLife = 1f;


    [Header("Debug")]
    [SerializeField] bool debugReceiveHit;
    [SerializeField] float debugHitDamage = 0.1f;

    public float currentLife;

    [SerializeField] public UnityEvent<float> onLifeChanged;
    [SerializeField] public UnityEvent onDeath;

    //Para probar desde el editor que funciona esto antes de conectar el evento
    private void OnValidate()
    {
        if (debugReceiveHit)
        {
            debugReceiveHit = false;
            OnHitReceived(debugHitDamage);
        }
    }

    private void Awake()
    {
        currentLife = startingLife;
    }

    public void OnHitReceived(float damage)
    {
        if (currentLife > 0f) {
            currentLife -= damage;
            onLifeChanged.Invoke(currentLife);
            if (currentLife <= 0f)
            {
                //Muerto
                onDeath.Invoke();
            }
        }

    }
    internal void RecoverHealth(float healthRecovery)
    {
        if (currentLife > 0f)
        {
            currentLife += healthRecovery;
            currentLife = Mathf.Clamp01(currentLife);   //Para que no se pase de 1
            onLifeChanged.Invoke(currentLife);
        }
    }
}
