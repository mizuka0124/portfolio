using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    public Material [] materials;//オブジェクトの色変更マテリアル
    public Material [] materialsColorPanel;//色選択UIのパネル
    public Material [] materialsObjectsPanel;//オブジェクト選択UIのパネル
    public int selectorColor;//色選択の変数
    public int selectorObject;//オブジェクト選択の変数
    public GameObject cube;//生成するオブジェクト
    public GameObject sphere;//生成するオブジェクト
   public GameObject capsule;//生成するオブジェクト
   public GameObject SD_Unitychan;//生成するオブジェクト
   public GameObject Unitychan;//生成するオブジェクト
   public GameObject[] objects;//生成するオブジェクトを格納する配列
   public GameObject judge;//ユニティちゃんの判別状態用のオブジェクトを入れる本体
   public GameObject Judgeprefub;//ユニティちゃんの判別状態用オブジェクトを入れる変数
    public GameObject target;//検知したオブジェクトを格納する
     public GameObject targetDammy;//検知したオブジェクトの選択を外すためのダミーオブジェクト
     public GameObject drow;//描画する用キャンバス
    

    //データの共有に使うインスタンス
	private static DataManager instance;

    //インスタンスを取得できる唯一のプロパティ
	public static DataManager Instance{
		get{
            //nullチェック
			if( null == instance ){
				instance = (DataManager)FindObjectOfType(typeof(DataManager));
				if( null == instance ){
					Debug.Log(" DataManager Instance Error ");
				}
			}
			return instance;
		}
	}


    // Start is called before the first frame update
    void Start()
    {
        objects=new GameObject[]{cube,sphere,capsule,SD_Unitychan,Unitychan};
        target=targetDammy;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target==null)
        target=targetDammy;

    }
}
