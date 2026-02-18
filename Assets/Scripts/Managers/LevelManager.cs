using System;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public int MushroomNumber
    {
        get => _mushroomNumber;
        set
        {
            _mushroomNumber = value < 0 ? 0 : value;
            onMushroomNumberChange?.Invoke(value);
        }
    }

    public Vector3 PlayerStartPosition = Vector3.zero;

    public UnityAction<int> onMushroomNumberChange;
    public UnityAction onMushroomTaken;
    public UnityAction onBoxInRightPlace;
    public UnityAction onPlayerOutOfZone;
    
    
    private int _mushroomNumber;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(PlayerStartPosition, 0.3f);
    }
}
