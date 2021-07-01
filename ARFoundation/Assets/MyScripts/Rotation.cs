using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    float count;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DataManager.Instance.target.transform.Rotate(0,count,0);
    }

    public void Right(){
        count=-3.0f;    }
    public void Left(){
        count=3.0f;
    }
        public void Stop(){
        count=0.0f;
    }

}
