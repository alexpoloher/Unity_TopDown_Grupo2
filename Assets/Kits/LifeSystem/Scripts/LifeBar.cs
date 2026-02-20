using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    [SerializeField] Image imageFill;
    private Life life;


    private void OnEnable()
    {
        life = FindFirstObjectByType<Life>();
        if (life == null)
        {
            Debug.LogWarning("LifeBar: No se encontró Life en la escena."); //En el menu inicial no hay barra de vida
            return;
        }

        life.onLifeChanged.AddListener(OnLifeChanged);
        life.onDeath.AddListener(OnDeath);


        OnLifeChanged(life.currentLife);
    }
    private void OnDisable()
    {
        if (life == null) return;

        life.onLifeChanged.RemoveListener(OnLifeChanged);
        life.onDeath.RemoveListener(OnDeath);
    }

    public void OnLifeChanged(float newLife) {
        imageFill.fillAmount = newLife;
    }

    void OnDeath() {
        Destroy(gameObject);
    }

}
