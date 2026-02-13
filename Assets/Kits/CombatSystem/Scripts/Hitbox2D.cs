using UnityEngine;

public class Hitbox2D : MonoBehaviour
{

    [SerializeField] string affectedTag = "Enemy";
    [SerializeField] float damageToDeal;

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (elOtro.CompareTag("Enemy") && affectedTag.Equals("Enemy"))
        {

            /*PlayerController playerController = GetComponentInParent<PlayerController>();
            playerConroller.NotifyHit(this);*/
        }
    }

}
