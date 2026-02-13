using System.Collections;
using UnityEngine;

public class HerreroScript : NpcsInteractuablesQuietos
{

    private bool espadaEntregada;
    [SerializeField] FraseDialogo fraseTrasEntregarEspada;
    [SerializeField] DropDefinition espadaAEntregar;
    [SerializeField] GameObject refItemEspada;
    private float tiempoOcultarItem = 1.25f;

    protected override void CerrarDialogo()
    {
        if (!espadaEntregada)
        {
            espadaEntregada = true;
            fraseDialogo = fraseTrasEntregarEspada;
            if(playerRef != null)
            {
                playerRef.RecibirItemPlayer(espadaAEntregar);
            }
            StartCoroutine(MostrarEspadaEntregada());
        }
        base.CerrarDialogo();
    }

    IEnumerator MostrarEspadaEntregada()
    {
        refItemEspada.SetActive(true);
        yield return new WaitForSeconds(tiempoOcultarItem);
        refItemEspada.SetActive(false);
    }

}
