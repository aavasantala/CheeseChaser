using UnityEngine;

public class MushScript : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MushroomCollection();
        } 
    }

    private void MushroomCollection()
    {
        Debug.Log("wow");
        LevelManager.Instance.MushroomNumber++;
        Destroy(gameObject);
    }
}
