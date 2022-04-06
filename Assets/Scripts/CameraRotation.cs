using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float speed =5f;
    public Transform rotator;
    private void Start()
    {
        rotator = GetComponent<Transform>();
    }
    private void Update()
    {
        rotator.Rotate(0, speed * Time.deltaTime, 0);
    }
}
