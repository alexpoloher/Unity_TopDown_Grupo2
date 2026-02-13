using UnityEngine;

[CreateAssetMenu]
public class DropDefinition : ScriptableObject
{
    public float healthRecovery;
    public int bullets;
    public int cantidadBombas;

    public enum enumTipoObjeto
    {
        Vida,
        Balas,
        Espada,
        Llave,
        Bombas,
        Arco
    }

    public enumTipoObjeto tipoDeObjeto;
}
