using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pinch : MonoBehaviour
{
    float vMin = 0.02f;
    float vMax = 0.1f;
　　private float backDist = 0.0f;
    //初期値

    float v ;
    Vector3 uni ;
    float ritu ;

         
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {if(DataManager.Instance.target.tag!="dammy"){
       if (Input.touchCount >= 2)
        {
            v=DataManager.Instance.target.transform.localScale.x;
            
            if(DataManager.Instance.Judgeprefub!=null)
            uni=DataManager.Instance.Judgeprefub.transform.localScale;

            Touch t1 = Input.GetTouch (0);
            Touch t2 = Input.GetTouch (1);

            if (t2.phase == TouchPhase.Began)
            {
                backDist = Vector2.Distance (t1.position, t2.position);
            }
             else if (t1.phase == TouchPhase.Moved && t2.phase == TouchPhase.Moved)
            {
                // タッチ位置の移動後、長さを再測し、前回の距離からの相対値を取る。
                float newDist = Vector2.Distance (t1.position, t2.position);
                float vs=v;
                v =v+(newDist - backDist) / 200000.0f;
                

                // 限界値をオーバーした際の処理
                if(v > vMax)
                {
                    v = vMax;
                }
                else if(v < vMin)
                {
                    v = vMin;
                }
                 ritu=v/vs;

                // 相対値が変更した場合、カメラに相対値を反映させる
                if(v != 0)
                {
                    DataManager.Instance.target.transform.localScale = new Vector3(v, v, v);
                    if(DataManager.Instance.target.tag=="Player"){
                    

                    
                    DataManager.Instance.Judgeprefub.transform.localScale = new Vector3(uni.x*ritu, uni.y*ritu, uni.z*ritu);

                    if (ritu<1){
    
                     DataManager.Instance.Judgeprefub.transform.position = new Vector3(DataManager.Instance.Judgeprefub.transform.position.x,DataManager.Instance.Judgeprefub.transform.position.y-0.0008f/ritu,DataManager.Instance.Judgeprefub.transform.position.z);   
                                     }
                     }
                    
                    
                }

                
            }
        }
    }
    }


        
        
    
}