using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    private Camera cam;

         
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        if(DataManager.Instance.flag==true){ 
//ゴールについたときカメラをズームする
            float view = cam.fieldOfView - 1.0f ;
            cam.fieldOfView = Mathf.Clamp(value : view, min : 10.0f, max : 45f);

        }
        
    }
}
