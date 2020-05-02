using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    Camera cam;
    [SerializeField]GameObject image;
    public Transform cameraTransform;
    //Set it to whatever value you think is best
    public float distanceFromCamera;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 resultingPosition = cameraTransform.position + cameraTransform.forward * distanceFromCamera;
        image.transform.position = new Vector3(cam.transform.position.x,cam.transform.position.y,image.transform.position.z);
    }
}
