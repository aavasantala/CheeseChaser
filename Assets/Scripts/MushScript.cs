using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class MushScript : MonoBehaviour
{
    [SerializeField, Range(1f, 30f)] private float coolDown = 15f;
    [SerializeField] private GameObject mushroomModel;

    [Header("UI")] 
    [SerializeField] private Image timerFill;
    
    private Collider _collider;
    private bool _startCoolDown = false;
    private float _currentTimeCoolDown;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        timerFill.enabled = false;
    }

    private void Update()
    {
        if (!_startCoolDown) return;
        
        timerFill.fillAmount = (float) _currentTimeCoolDown / coolDown;
        _currentTimeCoolDown -= Time.deltaTime;
        if (_currentTimeCoolDown <= 0)
        {
            SpawnMushroom();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DispawnMushroom();
        } 
    }

    private void DispawnMushroom()
    {
        _currentTimeCoolDown = coolDown;
        _startCoolDown = true;
        _collider.enabled = false;
        
        mushroomModel.SetActive(false);

        timerFill.enabled = true;
        LevelManager.Instance.MushroomNumber++;
        LevelManager.Instance.onMushroomTaken?.Invoke();
    }

    private void SpawnMushroom()
    {
        _collider.enabled = true;
        timerFill.enabled = false;
        mushroomModel.SetActive(true);
    }
}
