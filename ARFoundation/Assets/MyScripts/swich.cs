using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


public class swich : MonoBehaviour
{
public GameObject canvas;
public GameObject canvas2;
public GameObject canvas3;
public GameObject ARmanager;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void btnDraw(){//お絵描きボタンを押したとき使わないボタンを非表示にし、オクルージョンマネージャーを切る、ARボタンを表示させる
        canvas.SetActive(false);
        canvas2.SetActive(false);
        canvas3.SetActive(false);
        DataManager.Instance.drow.SetActive(true);
       Camera.main.GetComponent<AROcclusionManager>().enabled=false;

    }
        public void btnAR(){//ARボタンを押したとき使うボタンを表示させ、オクルージョンマネージャーを入れて、ARボタンを非表示にする
        canvas.SetActive(true);
        canvas2.SetActive(true);
        canvas3.SetActive(true);
        DataManager.Instance.drow.SetActive(false);
       Camera.main.GetComponent<AROcclusionManager>().enabled=true;

    }
}
