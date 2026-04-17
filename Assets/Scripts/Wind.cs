using UnityEngine;
public class Wind : MonoBehaviour
{
    [Header("Chirp Timing")]
    public float minDelay = 10f;
    public float maxDelay = 30f;
    [Header("Audio")]
    public AudioSource source;
   
    void Start()
    {
        StartCoroutine(ChirpLoop());
    }
    private System.Collections.IEnumerator ChirpLoop()
    {
        while (true)
        {
            float waitTime = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(waitTime);
            
            source.Play();
            
        }
    }
}