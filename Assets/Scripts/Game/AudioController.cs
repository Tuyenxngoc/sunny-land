using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource;

    private static AudioController _instance;
    public static AudioController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioController>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(AudioController).Name);
                    _instance = singletonObject.AddComponent<AudioController>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayOneShot(AudioClip audio)
    {
        if (audio && audioSource)
            audioSource.PlayOneShot(audio);
    }
}
