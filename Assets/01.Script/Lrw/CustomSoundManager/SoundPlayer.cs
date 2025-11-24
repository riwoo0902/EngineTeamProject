using UnityEngine;

namespace _01.Script.Lrw.CustomSoundManager
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] private SoundType soundType;
        [SerializeField] private AudioClip[] audioClips;
        [SerializeField, Range(0f, 1f)] private float volume = 1;
        [SerializeField] private float randomPitchRange = 0;
        private float _basePitch;

        private AudioSource _audioSource;
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _basePitch = _audioSource.pitch;

        }

        private void Update()
        {
            _audioSource.volume = volume * SoundManager.Instance.GetVolume(soundType);
        }

        [ContextMenu("Play")]
        public void SoundPlay()
        {
            _audioSource.pitch = _basePitch + Random.Range(-randomPitchRange, randomPitchRange);
            _audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
        }

    }
}