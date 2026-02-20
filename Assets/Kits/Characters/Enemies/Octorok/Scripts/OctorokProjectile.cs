using UnityEngine;

public class OctorokProjectile : BaseCharacter
{
    Vector3 direction = Vector3.left;
    public void setDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    new void Update()
    {
        Move(direction);
    }
}
