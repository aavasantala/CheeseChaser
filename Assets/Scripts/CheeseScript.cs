using System.Collections;
using UnityEngine;

public class CheeseScript : MonoBehaviour
{
    public LevelCompletedUI levelCompletedUI;
    public AudioSource source;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(CheeseSound());
        }
    }

    public IEnumerator CheeseSound()
    {
        source.Play();
        yield return new WaitForEndOfFrame();
        GameManager.Instance.CurrentGameState = GameManager.GameState.GameEnd;
    }

}
