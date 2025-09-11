using UnityEngine;

public class EvilDuckyRotation : MonoBehaviour
{
    [SerializeField] private Vector3 rotationSpeed = new Vector3(0f, 0f, 100f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
