 using Custom.MonoSingleton;
 using UnityEngine;
using UnityEngine.Audio;

namespace _01.Script.KM.Sound
{
    public class SoundMananger2 : MonoSingleton<SoundMananger2>
    {
        [field:SerializeField] public AudioMixer AudioMixer { get; private set; }
        private int a = 12;


        
        public void MasterChange(float value)
        {
            AudioMixer.SetFloat("Master", Mathf.Log10(value) * 40);
            Debug.Log(value);
        }
        public void SFXChange(float value)
        {
            AudioMixer.SetFloat("SFX", Mathf.Log10(value) * 40);
        }
        public void BackGroundChange(float value)
        {
            AudioMixer.SetFloat("BackGround", Mathf.Log10(value) * 40); 
        }
    }
}

