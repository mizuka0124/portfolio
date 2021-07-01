using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpTrap : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        //ジャンプ床のフラグを立てる
        DataManager.Instance.flag2=true;
    
    }
}
