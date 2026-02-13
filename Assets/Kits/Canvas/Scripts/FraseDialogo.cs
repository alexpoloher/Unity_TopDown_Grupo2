using UnityEngine;

[CreateAssetMenu]
public class FraseDialogo : ScriptableObject
{
    [TextArea(2, 4)]    //Indica las líneas mínimas y máximas de un  cuadro de texto
    public string[] textos;
    public OpcionesDialogo[] opciones;
    public FraseDialogo sigFrase;
}
