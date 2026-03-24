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
        LevelManager.Instance.MushroomNumber++;
        LevelManager.Instance.onMushroomTaken?.Invoke();
        Destroy(gameObject);
    }
}
