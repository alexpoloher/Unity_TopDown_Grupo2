using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyBase : BaseCharacter
{
    protected Sight2D sight;
    protected SpriteRenderer sprite;
    protected AudioSource audioSource;
    protected Life life;

    protected bool isAggro;
    protected override void Awake()
    {
        base.Awake();
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        sight = GetComponent<Sight2D>();
        life = GetComponent<Life>();
        isAggro = false;
    }

    protected void OnEnable()
    {
        life.onDeath.AddListener(killEnemy);
        life.onLifeChanged.AddListener(recieveDamage);
    }

    protected void OnDisable()
    {
        life.onDeath.RemoveListener(killEnemy);
        life.onLifeChanged.RemoveListener(recieveDamage);
    }

    protected override void Update()
    {
        base.Update();
        if (sight.GetClosestTarget() != null)
            isAggro = true;
        else
            isAggro = false;
    }

    protected virtual void recieveDamage(float dmg)
    {
        animator.SetTrigger("triggerDamage");
    }

    protected virtual void killEnemy()
    {
        animator.SetTrigger("triggerDeath");
        gameObject.GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 2f/3f); //La animacion de muerte dura 40/60 fotogramas
    }
}
