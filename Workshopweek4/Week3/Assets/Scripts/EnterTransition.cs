using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterTransition : MonoBehaviour
{
    public string sceneName = "Main"; //scene name
    public string playerTag = "Player";

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
            SceneManager.LoadScene(sceneName);  
    }
}
