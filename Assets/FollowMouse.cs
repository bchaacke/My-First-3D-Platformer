using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public Transform playerTransform;
    public Transform cameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform.position = playerTransform.position + new Vector3(-5 * Mathf.Sin(Mathf.Deg2Rad * playerTransform.eulerAngles.y), 3/2, -5 * Mathf.Cos(Mathf.Deg2Rad * playerTransform.eulerAngles.y));
        cameraTransform.eulerAngles = playerTransform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        cameraTransform.eulerAngles -= new Vector3(Input.GetAxis("Mouse Y"), cameraTransform.eulerAngles.y - playerTransform.eulerAngles.y, 0);
        cameraTransform.position = playerTransform.position + new Vector3(-5 * Mathf.Sin(Mathf.Deg2Rad * cameraTransform.eulerAngles.y), 3/2, -5 * Mathf.Cos(Mathf.Deg2Rad * cameraTransform.eulerAngles.y));
    }
}
