using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour{
Rigidbody rb;

public float speed;//移動の速さ
int count;//アイテム獲得数
public Text CountText;//スコア表示
AudioSource getSE;//アイテム取得時のSE

int uni =Select.selector;//キャラクター選択の配列番号

GameObject[] chara;//キャラクターのオブジェクトの配列


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count=0;
        SetCountText();
        getSE = GetComponent<AudioSource>();

        //キャラクター配列にキャラクターをいれる       
        chara=new GameObject[DataManager.Instance.chara.Length];
        

        //一度キャラクターの表示を消したあとセレクトされたものだけアクティブにする
        for(int i= 0;i<chara.Length;i++){
            chara[i]=DataManager.Instance.chara[i];
            chara[i].SetActive(false);
            }
        chara[uni].SetActive(true);     

        //選択されたプレイヤーマテリアルの実装
        this.GetComponent<Renderer>().material=DataManager.Instance.materials[Select.selector_m];


           
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //カーソルキーの入力を取得する
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");
        //もしもゴールしたらキーの入力を取得しないようにする
        if(DataManager.Instance.flag==true){
            moveH=0;
            moveV=0;
        }

        //カーソルキーの入力を力を加える方向に設定して力を加える
        Vector3 move = new Vector3(moveH, 0, moveV);
        rb.AddForce(move*speed);

        //カーソルキーの入力方向に合わせてキャラクターの向きを変える
        if(moveH>0)
         chara[uni].transform.localEulerAngles= new Vector3(0,90,0);
        else if(moveH<0)
         chara[uni].transform.localEulerAngles= new Vector3(0,-90,0);

        if(moveV>0)
         chara[uni].transform.localEulerAngles= new Vector3(0,0,0);
        else if(moveV<0)
         chara[uni].transform.localEulerAngles= new Vector3(0,180,0);

        //ジャンプの床に触れたときその方向にジャンプさせる
         if(DataManager.Instance.flag2==true){
                Vector3 force = new Vector3 (0.0f,400.0f,500.0f);    // 力を設定
                rb.AddForce (force, ForceMode.Force);            // 力を加える          

              DataManager.Instance.flag2=false;
         }




               
    }
    private void OnTriggerEnter(Collider other){
        //アイテムに触れたときの設定
        if(other.gameObject.CompareTag("Item")){
         other.gameObject.SetActive(false);//アイテムオブジェクトを消す
         count++;//カウントを増やす
         SetCountText();//テキストに文字列を追加する
         getSE.Play();//アイテム取得SEを流す

        }
        //下に落下して落下判定用の床に触れたらシーンの初めに戻る
        else if(other.gameObject.CompareTag("Bottom")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }

    }

//スコア表示テキストに設定するセッター
    void SetCountText(){
        CountText.text  ="ゲット数："+count.ToString();
    
    }

    
}
