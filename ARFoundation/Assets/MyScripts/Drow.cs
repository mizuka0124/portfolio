using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
public class Drow : MonoBehaviour
{   
    [SerializeField] GameObject LineObjectPrefab;//生成するプレハブの元

    //現在描画中のLineObject;
    private GameObject CurrentLineObject = null;
    LineRenderer render ;
    

     Pose hitPose ;//Raycastの衝突した位置を格納する変数
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if(DataManager.Instance.drow.activeSelf)//お絵描きモードの時
        {     
            if(Input.touchCount == 1 && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))//UIボタンをタッチしてないとき
             {
                Touch touch = Input.GetTouch(0);
                 if(touch.phase==TouchPhase.Moved)//タップが移動したとき
                {    
                    if(CurrentLineObject == null)
                     {
                       //PrefabからLineObjectを生成
                         CurrentLineObject = Instantiate(LineObjectPrefab, new Vector3(0, 0, 0), new Quaternion(0,0,0,0));
                      }
                                       
                    //ゲームオブジェクトからLineRendererコンポーネントを取得
                    render = CurrentLineObject.GetComponent<LineRenderer>();                   
                    int NextPositionIndex = render.positionCount;//LineRendererからPositionsのサイズを取得
                    render.positionCount = NextPositionIndex + 1;//LineRendererのPositionsのサイズを増やす
                    
                    Vector3 posi= touch.position;//タップしたポイントを取得
                    posi.z=0.2f;//z座標を設定　20ｃｍくらい前に線を生成する
                    posi= Camera.main.ScreenToWorldPoint(posi);//スクリーン座標をワールド座標に変換
                
                     render.SetPosition(NextPositionIndex, posi);//LineRendererのPositionsにタッチした位置を追加         
                }

                else if(touch.phase == TouchPhase.Ended)//指が離れたとき
                {
                    if(CurrentLineObject != null)
                     {
                      //現在描画中の線があったらnullにして次の線を描けるようにする。
                     CurrentLineObject = null;
                     }
                }
            
            }
        }
    }

    public void onclick(int a){

        switch(a){
            case 0:render.startColor=Color.red;
                    break;
            case 1:render.startColor=Color.blue;
                    break;
            case 2:render.startColor=Color.black;
                    break;

        }
    }
}
