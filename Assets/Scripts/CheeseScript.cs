using Unity.VisualScripting;
using UnityEngine;

public class CheeseScript : MonoBehaviour
{
    public LevelCompletedUI levelCompletedUI;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            _audioSource.Play();
            GameManager.Instance.CurrentGameState = GameManager.GameState.GameEnd;
        }
    }

}
