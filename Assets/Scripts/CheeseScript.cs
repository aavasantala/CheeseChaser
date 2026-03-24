using UnityEngine;

public class CheeseScript : MonoBehaviour
{
    public LevelCompletedUI levelCompletedUI;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.Instance.CurrentGameState = GameManager.GameState.GameEnd;
        }
    }

}
