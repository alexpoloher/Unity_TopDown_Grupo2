using UnityEngine;

public class NpcZelda : NpcsInteractuablesMoviles
{

    protected override Transform ElegirDestino()
    {


        int indiceDestino = indiceDestActual;

        Transform posicionActual;
        if (indiceDestActual != -1)
        {
            posicionActual = puntos[indiceDestActual];
        }
        else {
            posicionActual = transform;
        }

        while (indiceDestino == indiceDestActual)
        {
            indiceDestino = Random.Range(0, puntos.Length);
        }

        indiceDestActual = indiceDestino;

        if (posicionActual.position.x - puntos[indiceDestino].position.x < 0.0f)
        {
            //Hacia la Derecha
            animator.SetFloat("HorizontalMovement", 1);
            animator.SetFloat("VerticalMovement", 0);
        } else if (posicionActual.position.x - puntos[indiceDestino].position.x > 0.0f)
        {
            //Hacia la Izquierda
            animator.SetFloat("HorizontalMovement", -1);
            animator.SetFloat("VerticalMovement", 0);
        }
        else if (posicionActual.position.y - puntos[indiceDestino].position.y < 0.0f) {
            //Hacia arriba
            animator.SetFloat("HorizontalMovement", 0);
            animator.SetFloat("VerticalMovement", 1);
        } else if (posicionActual.position.y - puntos[indiceDestino].position.y > 0.0f) {
            //Hacia abajo
            animator.SetFloat("HorizontalMovement", 0);
            animator.SetFloat("VerticalMovement", -1);
        }

        return puntos[indiceDestino];
    }
}
