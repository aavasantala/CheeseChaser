using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class OutZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player out of zone");
            LevelManager.Instance.onPlayerOutOfZone?.Invoke();
        }
    }
    
}
