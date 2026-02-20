using UnityEngine;

public class GestorSonido : MonoBehaviour
{

    public static GestorSonido Instance;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource audioSourceMusicaFondo;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EjecutarSonido(AudioClip audio)
    {
        audioSource.volume = 1f;
        audioSource.PlayOneShot(audio);
    }

    public void DetenerSonidos()
    {
        audioSource.Stop();
        audioSourceMusicaFondo.Stop();
    }
}
