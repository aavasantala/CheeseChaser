using Unity.VisualScripting;
using UnityEngine;

public class MushroomScript : MonoBehaviour
{
    public int mushroomCount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collisomn");
        if (other.tag == "Player")
        {
            mushroomCount++;
            this.gameObject.SetActive(false);
            Debug.Log("collisomn");
        }
        
    }




}
