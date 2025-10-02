using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMananger : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    public static SoundMananger Instacne;
    private void Start()
    {
        if (Instacne == null)
        {
            Instacne = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void MasterChange(float value)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(value) * 40);
        Debug.Log(value);
    }
    public void SFXChange(float value)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(value) * 40);
    }
    public void BackGroundChange(float value)
    {
        audioMixer.SetFloat("BackGround", Mathf.Log10(value) * 40); 
    }
}
