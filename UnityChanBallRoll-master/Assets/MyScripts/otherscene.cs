using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class otherscene : MonoBehaviour
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
//次のシーンに遷移する
    SceneManager.LoadScene("MiniGame_Complete");


    }
}
