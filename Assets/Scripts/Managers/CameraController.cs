using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera cam;
    public Transform target;

    void Start()
    {
        cam = GetComponent<Camera>();

        ZoomIn();
    }

    void LateUpdate()
    {
        // zajištění pohybu kamery za hráčem
        if (target != null)
        {
            Vector3 newPos = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = newPos;
        }
    }

    public void ZoomIn()
    {
        cam.orthographicSize = 5;
    }

    public void ZoomOut()
    {
        cam.orthographicSize = 7;
    }
}
    