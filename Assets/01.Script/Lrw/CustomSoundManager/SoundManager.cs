using System;
using _01.Script.KM.Sound;
using Custom.MonoSingleton;
using UnityEngine;
using UnityEngine.UI;

namespace _01.Script.Lrw.CustomSoundManager
{
    public class SoundManager : MonoSingleton<SoundManager>
    {

        public float MasterVolume = 1;
        public float BGMVolume = 1;
        public float SFXVolume = 1;

        public float GetVolume(SoundType a)
        {
            float volume = MasterVolume;
            if (a == SoundType.BackGround) volume *= BGMVolume;
            else if (a == SoundType.SFX) volume *= SFXVolume;
            return volume;
        }

    }

    public enum SoundType
    {
        BackGround, SFX, Master
    }
}