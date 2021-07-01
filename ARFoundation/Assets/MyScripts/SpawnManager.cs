using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;


public class SpawnManager : MonoBehaviour
{     
 
     
    private ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();    
     Pose hitPose ;//Raycastの衝突した位置を格納する変数
     GameObject test;   //生成したオブジェクトを格納する
     Material[] materials;//色のマテリアルの配列
    public Camera arCamera;
    public LayerMask layerMask;//cubeだけを検知するレイヤーマスク
 
 //   public static GameObject targetDammy;//オブジェクトの選択を外すときのダミーオブジェクト
    
    public GameObject canvas;
    Vector3 scale;
    Vector3 idou;


 
    void Start()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        materials=new Material[DataManager.Instance.materials.Length];//Datamanagerにある配列と同じ数の配列を生成
        for(int i = 0; i<DataManager.Instance.materials.Length;i++)//Datamanagerの配列をいれる
            materials[i]=DataManager.Instance.materials[i];

    }
    
    void Update()
    {

        if(!DataManager.Instance.drow.activeSelf){
        
        //タップしたときの挙動
        if(Input.touchCount == 1 && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))//UIボタンをタッチしてないとき
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began )　　//画面に指が触れた時に処理する
            {  
                //ターゲットがnullじゃないとき透明度を戻してtargetをダミーに変える。選択してるオブジェクトの解除
                if(DataManager.Instance.target!=null){
                    if(DataManager.Instance.target.tag!="Player")
                    {
                　      var color = DataManager.Instance.target.GetComponent<Renderer>().material.color;
                        　DataManager.Instance.target.GetComponent<Renderer>().material.color=new Color(color.r,color.g,color.b,1.0f);
                    }
                    else
                    {
                       Destroy(DataManager.Instance.Judgeprefub);
                    }
                　DataManager.Instance.target=DataManager.Instance.targetDammy;
        
                }

                //レイを飛ばした先にオブジェクトがあったらそれを格納する オブジェクトの選択
                var ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if( Physics.Raycast(ray, out hit,Mathf.Infinity,layerMask)){
                   DataManager.Instance.target= hit.collider.gameObject;//選んだオブジェクトをtargetに格納
                   if(DataManager.Instance.target.tag!="Player")//普通のモデルは色と透明度を変える
                    {
                        var color = DataManager.Instance.target.GetComponent<Renderer>().material.color;
                        DataManager.Instance.target.GetComponent<Renderer>().material.color=new Color(color.r,color.g,color.b,0.5f);//透明度を下げる 
                   
                    }   
                    else//ユニティちゃん選択時は半透明なオブジェクトを生成する
                    {
                        Vector3 transform= DataManager.Instance.target.transform.position;//位置はターゲットの位置
                        Vector3 plus=new Vector3(0.0f,0.05f,0.0f);//高さを少し高く
                        Quaternion rotation= DataManager.Instance.target.transform.rotation;//向きをターゲットと同じに
                        DataManager.Instance.Judgeprefub= Instantiate(DataManager.Instance.judge, transform+plus, rotation);
                     
                        float ritu=DataManager.Instance.target.transform.localScale.x/scale.x;//生成時の大きさとの変化率を求める
                         //変化率に合わせて大きさも変える
                        DataManager.Instance.Judgeprefub.transform.localScale = new Vector3(DataManager.Instance.Judgeprefub.transform.localScale.x*ritu, DataManager.Instance.Judgeprefub.transform.localScale.y*ritu, DataManager.Instance.Judgeprefub.transform.localScale.z*ritu);

                        if (ritu<1) //縮小したとき小さくなると足元が出るのでそれの調整
                        {
                            DataManager.Instance.Judgeprefub.transform.position = new Vector3(DataManager.Instance.Judgeprefub.transform.position.x,DataManager.Instance.Judgeprefub.transform.position.y-0.01f/ritu,DataManager.Instance.Judgeprefub.transform.position.z);   
                            idou=DataManager.Instance.Judgeprefub.transform.position;  }
                            else{//小さくしなければそのまま。
                            idou=DataManager.Instance.Judgeprefub.transform.position; 
                        }
                    }
                }


                //レイを飛ばして平面に当たったらそこにオブジェクトを生成する
                else if (arRaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))//タッチしたところにRayを発射　hitsを取得　
                {
                    hitPose = hits[0].pose;　　//RayとARPlaneが衝突しところのPose Pose型は3D位置でのpositionとrotationを格納するhit[0]は衝突した一番近い場所hits[hits.Count-1]が一番遠い

                    test=Instantiate(DataManager.Instance.objects[DataManager.Instance.selectorObject], hitPose.position,Quaternion.Euler(0,180,0));
                                       
                    if(test.tag!="Player")//ユニティちゃんじゃないとき
                    test.GetComponent<Renderer>().material=DataManager.Instance.materials[DataManager.Instance.selectorColor];//選んだ色に変える

                    scale=test.transform.localScale;  //生成したときの大きさを取得         
                
                }


            }
            //タップを離さないままだと生成したオブジェクトを動かせる
            else if(touch.phase == TouchPhase.Moved)
                     {
                //生成したオブジェクトを好きな場所に配置できる
                 if (arRaycastManager.Raycast(touch.position+touch.deltaPosition, hits, TrackableType.PlaneWithinPolygon))//deltaPositisionは移動先のポジションではなくタップした場所からの移動量。
                {
                    hitPose = hits[0].pose;　
                    test.transform.position=hitPose.position;

                }

                //選択したオブジェクトを動かせる
                Vector3 plus=new Vector3(0.0f,0.05f,0.0f);
                if(DataManager.Instance.target.tag!="dammy")  //ダミー以外の時は本体を動かす
                    DataManager.Instance.target.transform.position=hitPose.position;

                if(DataManager.Instance.target.tag=="Player")//ユニティちゃん選択時は選択状態のオブジェクトもうごかす
                    DataManager.Instance.Judgeprefub.transform.position =new Vector3(hitPose.position.x,idou.y,hitPose.position.z);//y座標を調整してユニティちゃんにあわせる
                
                
            }
            //タップ離した後も生成したオブジェクトを動かせるのを防止
             else if(touch.phase == TouchPhase.Ended)
            {
                 if (test!=null)
                    test=DataManager.Instance.targetDammy;
            }

        
        
        //ユニティちゃん選択時とパネルでユニティちゃん選択時は色変更ボタンを非表示にする
    　　 if(DataManager.Instance.selectorObject>2||DataManager.Instance.target.tag=="Player")
        canvas.SetActive(false);
        else
        canvas.SetActive(true);
            
        }  

        } 

    }
 
}


