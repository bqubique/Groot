using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    Camera cam;
    [SerializeField]GameObject image;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        image.transform.position = new Vector3(cam.transform.position.x,cam.transform.position.y,cam.transform.position.z);
    }
}
