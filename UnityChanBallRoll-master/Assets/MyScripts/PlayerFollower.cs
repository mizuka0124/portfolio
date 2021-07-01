using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    public GameObject player;//プレイヤーのオブジェクト
    Vector3 offset;//プレイヤーとキャラクターのオブジェクトの距離を入れる変数


    // Start is called before the first frame update
    void Start()
    {
        //キャラクターの大きさに合わせてカメラの位置を設定する
        Camera.main.transform.position=new Vector3(Camera.main.transform.position.x,Select.charaHight[Select.selector].y+2.5f,Camera.main.transform.position.z);
        
        //キャラクターのプレイヤーの距離を測って変数にいれる　
        offset = this.transform.position -player.transform.position ;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        //プレイヤーに追従させるため現在の位置をプレイヤーの距離にoffsetを足したものにする
        this.transform.position = player.transform.position+offset;
    }

    

}
