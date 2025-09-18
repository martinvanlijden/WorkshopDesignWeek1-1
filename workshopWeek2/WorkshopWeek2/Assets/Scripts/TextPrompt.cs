using UnityEngine;
using TMPro;
public class TextPrompt : MonoBehaviour
{
    public TMP_Text messageText; // Assign in Inspector
    public string messageToShow = "You entered the area!";
    public float messageDuration = 3f;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered trigger");
        if (other.CompareTag("Player"))
        {
            Debug.Log("recognizes player");
            StopAllCoroutines();
            StartCoroutine(ShowMessage());
        }
    }

    private System.Collections.IEnumerator ShowMessage()
    {
        Debug.Log("showmessage is called");
        messageText.text = messageToShow;
        messageText.enabled = true;

        yield return new WaitForSeconds(messageDuration);

        messageText.enabled = false;
    }
}

