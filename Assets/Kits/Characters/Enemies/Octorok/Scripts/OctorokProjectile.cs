using UnityEngine;

public class OctorokProjectile : BaseCharacter
{
    [SerializeField] float speed = 50f;
    [SerializeField] Vector3 direction = Vector3.left;

    public void setDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    // Update is called once per frame
    new void Update()
    {
        rb.linearVelocity = direction * speed * Time.deltaTime;
    }
}
