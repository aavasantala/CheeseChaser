using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class SettingsScript : MonoBehaviour
{

    private Button btnBack;
    private Slider musicVol;
    private Slider soundVol;
    

    public AudioMixer audioMixer;
    public AudioMixerGroup[] group;

    public UIDocument mainMenuDoc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        
        var uiDocument = GetComponent<UIDocument>();
        if (uiDocument == null)
        {
            Debug.LogError("UIDocument component missing!");
            return;
        }
        var root = uiDocument.rootVisualElement;
        btnBack = root.Q<Button>("backButton");
        musicVol = root.Q<Slider>("musicSlider");
        soundVol = root.Q<Slider>("sfxSlider");

        if (btnBack != null)
            btnBack.clicked += BackMain;
        else
            Debug.LogError("btnBack not found!");
        musicVol.RegisterValueChangedCallback(OnMusicValueChanged);
        soundVol.RegisterValueChangedCallback(OnSoundValueChanged);

        //volume change stuff, here it takes the volume from the mixer
        float soundVolume;
        float musicVolume;

        bool getMusic = audioMixer.GetFloat("musicVol", out musicVolume);
        bool getSound = audioMixer.GetFloat("foleyVol", out soundVolume);

        musicVol.value = musicVolume;
        soundVol.value = soundVolume;
    }

    private void OnDisable()
    {
        musicVol.UnregisterValueChangedCallback(OnMusicValueChanged);
        soundVol.UnregisterValueChangedCallback(OnSoundValueChanged);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BackMain()
    {
        mainMenuDoc.gameObject.SetActive(true);
        Debug.Log("WOW! BACK");
        this.gameObject.SetActive(false);
    }

    //and here applies the setting changes
    void OnMusicValueChanged(ChangeEvent<float> changeEvent) 
    {
        Debug.Log("Music changed!" + changeEvent);
        Debug.Log(musicVol.value);
        audioMixer.SetFloat("musicVol", musicVol.value);
    }

    void OnSoundValueChanged(ChangeEvent<float> changeEvents)
    {
        Debug.Log("Sound changed!" + changeEvents);
        Debug.Log(soundVol.value);
        audioMixer.SetFloat("foleyVol", soundVol.value);
        audioMixer.SetFloat("ambientVol", soundVol.value);
    }

}
