using UnityEngine;
using UnityEngine.Audio;

public class EnemyBase : BaseCharacter
{
    protected Sight2D sight;
    protected SpriteRenderer sprite;
    protected AudioSource audioSource;

    protected bool isAggro;
    protected override void Awake()
    {
        base.Awake();
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        sight = GetComponent<Sight2D>();
        isAggro = false;
    }

    protected override void Update()
    {
        base.Update();
        if (sight.GetClosestTarget() != null)
            isAggro = true;
        else
            isAggro = false;
    }

    protected void killEnemy()
    {
        //efecto de sonido, animacion de muerte, etc
        Destroy(gameObject, 0.5f);
    }
}
