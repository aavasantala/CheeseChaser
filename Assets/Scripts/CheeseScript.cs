using UnityEngine;

public class CheeseScript : MonoBehaviour
{
    public LevelCompletedUI levelCompletedUI;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameWin();
        }
    }

    void GameWin()
    {
        Debug.Log("Player won!");
        if (levelCompletedUI != null)
        {
            levelCompletedUI.Show();
        }
    }
}
