using UnityEngine;

public class MushScript : MonoBehaviour
{
    public int mushroomCount;
    public UIManager manager;
    

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter");
        if (other.tag == "Player")
        {
            Debug.Log("enter player");
            MushCollection();
        }
    }

    void MushCollection()
    {
        Debug.Log("wow");
        mushroomCount++;
        manager.UpdateMushroomCount(mushroomCount);
        Destroy(gameObject);

    }
}
