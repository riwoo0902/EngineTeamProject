using _01.Script.Lrw.CustomSoundManager;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class BGMPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        audioSource.volume = SoundManager.Instance.GetVolume(SoundType.BackGround);
    }



}
