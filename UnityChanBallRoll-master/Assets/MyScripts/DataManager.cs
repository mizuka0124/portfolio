using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{

     public GameObject[] chara;//キャラクターのオブジェクトの配列
     public GameObject unityChan;//キャラクターSDユニティちゃん
     public GameObject unityChan2;//キャラクター　ユニティちゃん
     public GameObject[] player ;//キャラクターが載ってるボールの配列
     public GameObject ball;//ＳＤユニティちゃんが乗ってるボール
     public GameObject ball2; //ユニティちゃんが載ってるボール   

     public Material[] materials;//ボールデザインのマテリアルの配列


    public bool flag;//ゴールしたかしてないかのフラグ
    public bool flag2;//ジャンプボードに触れたか触れてないか

    private static DataManager instance;
    public static DataManager Instance{
     get{
        if(null== instance){
            instance = (DataManager)FindObjectOfType(typeof(DataManager));
            if(null== instance){
                Debug.Log("Datamanager error");
            }

        }
        return instance;
     }
    }

    // void Awake(){
	// 	GameObject[] obj = GameObject.FindGameObjectsWithTag("DataManager");
	// 	if( 1 < obj.Length ){
	// 		// 既に存在しているなら削除
	// 		Destroy( gameObject );
	// 	}else{
	// 		// シーン遷移では破棄させない
	// 		DontDestroyOnLoad( gameObject );
	// 	}
	// }




    // Start is called before the first frame update
    void Start()
    {

         player= new GameObject[]{ball,ball2};
         chara= new GameObject[]{unityChan,unityChan2};

        flag=false;
        flag2=false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
