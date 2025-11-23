using System;
using System.Collections.Generic;
using Custom.MonoSingleton;
using UnityEngine;
using UnityEngine.UI;

namespace _01.Script.Lrw.CustomSoundManager
{
    public class SoundManager : MonoSingleton<SoundManager>
    {
        //[SerializeField] private AudioData[] audioDatas;
        //private Dictionary<string, (AudioClip, float, AudioType)> _soundDictionary = new();

        [Header("Volume")]
        [Range(0, 1)]
        public float MasterVolume = 1;
        [Range(0, 1)]
        public float BGMVolume = 1;
        [Range(0, 1)]
        public float SFXVolume = 1;
        protected override void Awake()
        {
            base.Awake();
            
            //SetAudioDatas();
        }
        /*private void SetAudioDatas()
        {
            _soundDictionary.Clear();
            if(CheckSoundSetting())
            {
                foreach (AudioData audioData in audioDatas)
                {
                    AddAudioClip(audioData.AudioClipName, (audioData.AudioClip, audioData.Volume, audioData.AudioClipType));
                }
            }
            
        }*/

        // void AddAudioClip(string audioClipName, (AudioClip, float, AudioType) addClip) => _soundDictionary.Add(audioClipName, addClip);
        //private (AudioClip, float, AudioType) GetAudioClip(string getAudioClipName) => _soundDictionary.TryGetValue(getAudioClipName, out (AudioClip, float, AudioType) returnValue) ? returnValue : (null, 0, AudioType.None);
        /*public void PlayAudioClip(string audioClipName)
        {
            (AudioClip, float, AudioType) getAudioClip = GetAudioClip(audioClipName);
            if (getAudioClip.Item1 != null)
            {
                float volume = MasterVolume;
                if (getAudioClip.Item3 == AudioType.BGM) volume *= BGMVolume;
                else if (getAudioClip.Item3 == AudioType.SFX) volume *= SFXVolume;
                audioSource.PlayOneShot(getAudioClip.Item1, getAudioClip.Item2 * volume);
            }
        }*/
        public float GetVolume(AudioType a)
        {
            float volume = MasterVolume;
            if (a == AudioType.BGM) volume *= BGMVolume;
            else if (a == AudioType.SFX) volume *= SFXVolume;
            return volume;
        }

        /*private void OnValidate()
        {
            CheckSoundSetting();
        }
        */

        /*private bool CheckSoundSetting()
        {
            foreach (AudioData audioData in audioDatas)
            {
                if (audioData.AudioClip == null || audioData.AudioClipName == null)
                {
                    Debug.Log("SoundManager Error");
                    return false;
                }
            }
            return true;
        }*/

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
        public void SetMasterVolume(float value)
        {
            MasterVolume = value;
        }
        public void SetBGMVolume(float value)
        {
            BGMVolume = value;
        }
        public void SetSFXVolume(float value)
        {
            SFXVolume = value;
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