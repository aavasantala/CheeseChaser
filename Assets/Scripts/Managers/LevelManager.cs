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

    public UnityAction<int> onMushroomNumberChange;
    public UnityAction onMushroomTaken;
    public UnityAction onBoxInRightPlace;
    
    private int _mushroomNumber;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
}
