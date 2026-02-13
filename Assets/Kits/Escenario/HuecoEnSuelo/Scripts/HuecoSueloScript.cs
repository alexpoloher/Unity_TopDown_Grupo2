using UnityEngine;

public class HuecoSueloScript : MonoBehaviour
{

    Collider2D colliderHueco;
    [SerializeField] Transform posCaida;

    private void Awake()
    {
        colliderHueco = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (elOtro.CompareTag("PiesPlayer"))
        {
            PlayerCharacter player = elOtro.gameObject.GetComponentInParent<PlayerCharacter>();
            if (player != null) {

                Vector3 puntoEnElCollider = colliderHueco.ClosestPoint(elOtro.transform.position);
                
                //Se calcula la posición del punto desde el que se entra respecto al centro del hueco
                /*Vector3 direccionEntrada = (puntoEnElCollider - transform.position);

                float distSeguridad = 0.5f;
                Vector3 posRespawn = puntoEnElCollider;



                print(direccionEntrada.y);


                Vector3 diferencia = elOtro.transform.position - transform.position;
                print(puntoEnElCollider.x);
                print(colliderHueco.bounds.min.x);
                float rangoAdmision = 0.2f;
                if(Mathf.Abs(puntoEnElCollider.x - colliderHueco.bounds.min.x) < rangoAdmision || Mathf.Abs(puntoEnElCollider.x - colliderHueco.bounds.max.x) < rangoAdmision)
                {
                    if (direccionEntrada.x > 0)
                    {
                        posRespawn.x = colliderHueco.bounds.max.x + distSeguridad;
                    }
                    else if (direccionEntrada.x < 0)
                    {
                        posRespawn.x = colliderHueco.bounds.min.x - distSeguridad;
                    }

                }
                else
                {
                    if (direccionEntrada.y > 0)
                    {
                        posRespawn.y = colliderHueco.bounds.max.y + distSeguridad;
                    }
                    else if (direccionEntrada.y < 0)
                    {
                        posRespawn.y = colliderHueco.bounds.min.y - distSeguridad;
                    }

                }*/

                player.TocarHueco(posCaida.position);
            }

        }
    }
}
