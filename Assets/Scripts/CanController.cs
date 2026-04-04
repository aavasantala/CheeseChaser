using UnityEngine;

public class CanController : MonoBehaviour
{
    public GameObject can;
    Collider m_ObjectCollider;

    void Start()
    {
        m_ObjectCollider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            MakeCanFall();
        }


    }

    void MakeCanFall()
    {

        Vector3 canPos = Vector3.zero;
        Vector3 canRot = Vector3.zero;
        canPos.x = 8.727f; canPos.y = 1.842f; canPos.z = 7.27f;
        canRot.x = 0f; canRot.y = 0f; canRot.z = 90f;


        this.transform.position = canPos;
        this.transform.Rotate(canRot);
        m_ObjectCollider.isTrigger = false;


    }
}
