using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsScript : MonoBehaviour
{

    private Button btnBack;
    private Slider musicVol;
    private Slider soundVol;

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

    void OnMusicValueChanged(ChangeEvent<float> changeEvent) 
    {
        Debug.Log("Music changed!" + changeEvent);
    }

    void OnSoundValueChanged(ChangeEvent<float> changeEvents)
    {
        Debug.Log("Sound changed!" + changeEvents);
    }

}
