using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalChecker : MonoBehaviour
{

    public AudioSource gameBgm;//ゲームのBGM
    public AudioSource goalBgm;//ゴールした時のBGM
    public GameObject retryButton;//リトライボタン    
    public GameObject goalPaul;//ゴール時邪魔になるオブジェクト
    GameObject[] chara;//キャラクターのオブジェクトを入れる配列



    // Start is called before the first frame update
    void Start()
    {
       // goalWall.SetActive(true);
        goalPaul.SetActive(true);//開始時1週目で消したオブジェクトを復活させる
        retryButton.SetActive(false);//リトライボタンを消す

        //キャラクターのオブジェクトをデータマネージャーから取り出し配列に入れる
        chara=new GameObject[DataManager.Instance.chara.Length];              
        for(int i= 0;i<chara.Length;i++)
            chara[i]=DataManager.Instance.chara[i];
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnTriggerEnter(Collider other){
        //ゴールのフラグを立てる
        DataManager.Instance.flag = true;

        // 物理演算を切る
        other.gameObject.GetComponent<Rigidbody>().isKinematic=true;
        
        // オブジェクトの向きを変える。引数の方向を見る
        chara[Select.selector].transform.LookAt(Camera.main.transform);

        //オブジェクトに設定されてるアニメーションを切り替える
        chara[Select.selector].GetComponent<Animator>().SetTrigger("Goal");           

        //BGMを切り替える
        gameBgm.Stop();
        goalBgm.Play();

        //ゴールしたら邪魔なオブジェクトをけす
        goalPaul.SetActive(false);

        //retryボタンを出す
        retryButton.SetActive(true);

    }

    public void RetryStage(){
        //リトライボタンを押したときシーンを最初に戻す
       SceneManager.LoadScene("start");

    }
}
