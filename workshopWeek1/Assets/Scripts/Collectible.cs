using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collectible entered by:" + other.tag);
        if (other.tag == "Player")
        {
            Debug.Log("collected!!");
            CollectibleTracker.collectduck();
            Destroy(this.gameObject);
        }
        
    }
}
