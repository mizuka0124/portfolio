using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Select : MonoBehaviour
{
    public static GameObject[] chara;//キャラクターのオブジェクトの配列


    GameObject[] hight;//キャラクターの高さを計測するため用のオブジェクトの配列
    public GameObject unityChanHight;//SDユニティちゃんの高さ
    public GameObject unityChan2Hight;//ユニティちゃんの高さ
    
    public static GameObject[] player ;//キャラクターが載ってるボールの配列
    public GameObject ball;//ＳＤユニティちゃんが乗ってるボール
    public GameObject ball2; //ユニティちゃんが載ってるボール
    
    Vector3[] CamPosition;//カメラの位置
    
    private Camera cam;
   public static int selector =0;//キャラクター選択用のセレクト番号
   public static int selector_m =0;//マテリアル選択用のセレクト番号
   public static Vector3 [] charaHight;//キャラクターの高さのY座標を入れる配列

    public Material[] materials;//ボールデザインのマテリアルの配列
    int decisionFlag=0;//決定ボタンを押した数


 
    // Start is called before the first frame update
    void Start()
    {  //カメラを取得
        cam = GetComponent<Camera>();

        //各オブジェクトを配列に格納する
        player= new GameObject[]{ball,ball2};
        hight= new GameObject[]{unityChanHight,unityChan2Hight};
        CamPosition = new Vector3[hight.Length];
        charaHight = new Vector3[hight.Length];

        //リトライしたときにキャラクターとマテリアルの設定をもどす
        selector=0;
        selector_m=0;
            
        for(int i=0;hight.Length>i;i++){
            //各オブジェクトの位置の高めに設定するためのカメラの位置を格納する
             CamPosition[i]=new Vector3(hight[i].transform.position.x, hight[i].transform.position.y-0.8f,Camera.main.transform.position.z);
             //各オブジェクトの位置の高さを取得して格納する
             charaHight[i]= new Vector3(0.0f,hight[i].transform.position.y-player[i].transform.position.y,0.0f);
             //プレイヤーが動かないようにした後マテリアルを適応させる
             player[i].GetComponent<Rigidbody>().isKinematic=true;
              player[i].GetComponent<Renderer>().material=materials[selector_m];
        }     

    }

    // Update is called once per frame
    void Update()
    {

        if(decisionFlag==0){//決定ボタンを押していない　キャラクター選択

            //キャラクター選択によって見えるようにカメラの位置を変える
             cam.transform.position=CamPosition[selector];
        }

        else if(decisionFlag==1){//決定ボタンを一度押した　マテリアル選択

            //マテリアル選択によって見えるようにカメラの位置を変える
            cam.transform.position = new Vector3(CamPosition[selector].x,0.7f,CamPosition[selector].z);
            
            //選んだマテリアルに変える
            player[selector].GetComponent<Renderer>().material=materials[selector_m];
        }

        else if(decisionFlag==2){//決定ボタンを2回押した　設定を決定してシーンを変える
            //
            SceneManager.LoadScene("Sceane01");

        }
        
    }
//→のカーソルキー
    public void SelectRight(){
      if(decisionFlag==0){
           selector++;
           if(selector==charaHight.Length)
                selector=0;
        }
        else if(decisionFlag==1){
            selector_m++;
           if(selector_m==materials.Length)
                selector_m=0;
        }
    }

//左のカーソルキー
    public void SelectLeft(){
        if(decisionFlag==0){
            selector--;
            if(selector==-1)
                selector=charaHight.Length-1;
        }
        else if(decisionFlag==1){
            selector_m--;
           if(selector_m==-1)
                selector_m=materials.Length-1;
        }
    
    }
//決定キー
    public void SelectDecision(){
          decisionFlag++;
    }
}
