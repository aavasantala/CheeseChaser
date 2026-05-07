using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class Tutorial : MonoBehaviour
{
    public GameManager manager;
    public UIManager uiManager;
    public IngameUI ingameui;
    public Canvas canvas;
    public UnityEngine.UI.Image tutImage;
    int tutorialPhase = 1;
    public Sprite image1;
    public Sprite image2;
    public Sprite image3;
    public Sprite image4;
    public Sprite image5;
    public Sprite image6;
    public Sprite image7;

    public bool tutActive;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ingameui.gameObject.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (tutActive)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ClickPrevious();
            }

            if(Input.GetKeyDown(KeyCode.E))
            {
                ClickNext();
            }
        }
    }

    public void StartTutorial()
    {
        
        tutActive = true;
        canvas.enabled = true;
        
        
        StartCoroutine(DoFade());
        Time.timeScale = 0f;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
    }
    IEnumerator DoFade()
    {
        Time.timeScale = 0f;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;

        yield return null;
    }

    public void ClickPrevious()
    {
        if (tutorialPhase > 1)
        {
            tutorialPhase--;
        }
        else
        {
            return;
        }

        ImageChanger();
    }
    public void ClickNext()
    {
        if (tutorialPhase <7)
        {
            tutorialPhase++;
        }
        else
        {
            tutActive = false;
            canvas.enabled=false;
            Time.timeScale = 1f;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
            ingameui.gameObject.active = true;
            uiManager.ShowInGameUI(true);
            return;
        }

        ImageChanger();
    }

    public void ImageChanger()
    {
        switch (tutorialPhase)
        {
            case 1:
                tutImage.sprite = image1;
                break;

            case 2:
                tutImage.sprite = image2;
                break;

            case 3:
                tutImage.sprite = image3;
                break;

            case 4:
                tutImage.sprite = image4;
                break;

            case 5:
                tutImage.sprite = image5;
                break;

            case 6:
                tutImage.sprite = image6;
                break;

            case 7:
                tutImage.sprite = image7;
                break;
        }
    }
}
