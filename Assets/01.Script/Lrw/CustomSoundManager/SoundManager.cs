using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lrw_CustomSoundManager
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioData[] audioDatas;
        private Dictionary<string, (AudioClip, float, AudioType)> _soundDictionary = new();
        private AudioSource audioSource;

        [Header("Volume")]
        [Range(0, 1)]
        public float MasterVolume = 1;
        [Range(0, 1)]
        public float BGMVolume = 1;
        [Range(0, 1)]
        public float SFXVolume = 1;
        public static SoundManager instance { get; private set; }
        private void Awake()
        {
            #region Singleton
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            #endregion

            audioSource = GetComponent<AudioSource>();
            SetAudioDatas();
        }
        private void SetAudioDatas()
        {
            _soundDictionary.Clear();
            foreach (AudioData audioData in audioDatas)
            {
                AddAudioClip(audioData.AudioClipName, (audioData.AudioClip, audioData.Volume, audioData.AudioClipType));
            }
        }

        private void AddAudioClip(string audioClipName, (AudioClip, float, AudioType) addClip) => _soundDictionary.Add(audioClipName, addClip);
        private (AudioClip, float, AudioType) GetAudioClip(string getAudioClipName) => _soundDictionary.TryGetValue(getAudioClipName, out (AudioClip, float, AudioType) returnValue) ? returnValue : (null, 0, AudioType.None);
        public void PlayAudioClip(string audioClipName)
        {
            (AudioClip, float, AudioType) getAudioClip = GetAudioClip(audioClipName);
            if (getAudioClip.Item1 != null)
            {
                float volume = MasterVolume;
                if (getAudioClip.Item3 == AudioType.BGM) volume *= BGMVolume;
                else if (getAudioClip.Item3 == AudioType.SFX) volume *= SFXVolume;
                audioSource.PlayOneShot(getAudioClip.Item1, getAudioClip.Item2 * volume);
            }
        }

        private void OnValidate()
        {
            foreach (AudioData audioData in audioDatas)
            {
                if (audioData.AudioClip == null || audioData.AudioClipName == null)
                {
                    Debug.LogError("SoundManager Error");
                    return;
                }
            }
        }

        public void ChangeMasterVolume(Slider slider)
        {
            MasterVolume = slider.value;
        }
        public void ChangeBGMVolume(Slider slider)
        {
            BGMVolume = slider.value;
        }
        public void ChangeSFXVolume(Slider slider)
        {
            SFXVolume = slider.value;
        }
    }

    [Serializable]
    public class AudioData
    {
        [field: SerializeField] public AudioType AudioClipType { get; private set; }
        [field: SerializeField] public string AudioClipName { get; private set; }
        [field: SerializeField] public AudioClip AudioClip { get; private set; }
        [Range(0, 1)]
        [SerializeField] private float volume = 1;
        public float Volume
        {
            get => volume;
            private set
            {
                volume = value;
            }
        }
    }
    public enum AudioType
    {
        BGM, SFX, None
    }
}