using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private AudioSource mergeSource;

    [Header("Sounds ")]
    [SerializeField] private AudioClip[] mergeClips;
    private void Awake()
    {
        MergeManager.onMergeProcessed += MergeProcessedCallback;
        SettingsManager.onSFXValueChanged += SFXValueChangedCallback;
    }
    private void OnDestroy()
    {
        MergeManager.onMergeProcessed -= MergeProcessedCallback;
        SettingsManager.onSFXValueChanged -= SFXValueChangedCallback;
    }
    // Start is called before the first frame update    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void MergeProcessedCallback(FruitType fruitType, Vector2 mergePos)
    {
        PlayMergeSound();
    }
    private void PlayMergeSound()
    {
        //mergeSource.pitch = Random.Range(0.9f, 1.1f);
        mergeSource.clip = mergeClips[Random.Range(0, mergeClips.Length)];
        mergeSource.Play();
    }
    private void SFXValueChangedCallback(bool sfxActive)
    {
        mergeSource.mute = !sfxActive;
    }

}
