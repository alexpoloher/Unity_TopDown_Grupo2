using UnityEngine;

public class JarronRompibleScript : MonoBehaviour
{

    [SerializeField] GameObject objetoInterior;
    [SerializeField] Transform posicionSpawnItem;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void NotifyHit()
    {
        RomperJarron();
    }

    private void RomperJarron()
    {
        animator.SetTrigger("Romper");
    }


    public void SpawnearItem()
    {
        if (objetoInterior != null)
        {
            Instantiate(objetoInterior, posicionSpawnItem.position, Quaternion.identity);
        }
    }

}
