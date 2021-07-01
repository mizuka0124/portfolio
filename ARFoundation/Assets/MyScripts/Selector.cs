using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selector : MonoBehaviour
{
    public GameObject colorPanel;
    public GameObject objectPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //色変えボタンのメソッド
    public void ColorChangeRight()//右→を押したときの挙動　最大値まで行ったらループするようにしてる
    {
        DataManager.Instance.selectorColor++;
        if(DataManager.Instance.selectorColor==DataManager.Instance.materialsColorPanel.Length)
            DataManager.Instance.selectorColor=0;

       //パネルを変える
       colorPanel.GetComponent<Image>().material=DataManager.Instance.materialsColorPanel[DataManager.Instance.selectorColor];

    }
        public void ColorChangeLeft()//左←を押したときの挙動　最小値まで行ったらループするようにしてる
    {
         DataManager.Instance.selectorColor--;
        if(DataManager.Instance.selectorColor==-1)
            DataManager.Instance.selectorColor=DataManager.Instance.materialsColorPanel.Length-1;
       //パネルを変える
       colorPanel.GetComponent<Image>().material=DataManager.Instance.materialsColorPanel[DataManager.Instance.selectorColor];

    }

    public void ButtonClick(int number)//真ん中のボタンを押したときに色が変わる
     {

            DataManager.Instance.target.GetComponent<Renderer>().material=DataManager.Instance.materials[DataManager.Instance.selectorColor];
            var color = DataManager.Instance.target.GetComponent<Renderer>().material.color;
            DataManager.Instance.target.GetComponent<Renderer>().material.color=new Color(color.r,color.g,color.b,0.5f);
   
    }


    //オブジェクトの形を変えるボタンのメソッド
    
    public void ObjectChangeRight()//オブジェクト選択右→
    {
        DataManager.Instance.selectorObject++;
        if(DataManager.Instance.selectorObject==DataManager.Instance.materialsObjectsPanel.Length)
            DataManager.Instance.selectorObject=0;
        objectPanel.GetComponent<Image>().material=DataManager.Instance.materialsObjectsPanel[DataManager.Instance.selectorObject];
    }
    
        public void ObjectChangeLeft()//オブジェクト選択←
    {
               DataManager.Instance.selectorObject--;
        if(DataManager.Instance.selectorObject==-1)
            DataManager.Instance.selectorObject=DataManager.Instance.materialsObjectsPanel.Length-1;
        objectPanel.GetComponent<Image>().material=DataManager.Instance.materialsObjectsPanel[DataManager.Instance.selectorObject];
    }

    //オブジェクト決定　真ん中ボタン
        public void ObjectButtonClick(int number)
     {
        
        if(/*DataManager.Instance.target!=null&&*/DataManager.Instance.target.tag!="dammy")
        {
            Vector3 transform= DataManager.Instance.target.transform.position;//ターゲットの位置を取得
            Quaternion rotation= DataManager.Instance.target.transform.rotation;//ターゲットの向きを取得
            GameObject test= Instantiate(DataManager.Instance.objects[DataManager.Instance.selectorObject], transform, rotation);//ターゲットと同じ位置と向きで生成

            if(DataManager.Instance.Judgeprefub!=null)//ユニティちゃん選択時のオブジェクトがあったら消す
            Destroy(DataManager.Instance.Judgeprefub);


             if(test.tag!="Player")//ユニティちゃんじゃないオブジェクトを生成するとき
            {
             if(DataManager.Instance.target.tag!="Player")    //選択オブジェクトがユニティちゃんじゃないとき
                 test.GetComponent<Renderer>().material=DataManager.Instance.target.GetComponent<Renderer>().material;//生成オブジェクトの色を選択オブジェクトと同じにする
            else//選択オブジェクトがユニティちゃんの時
                test.GetComponent<Renderer>().material=DataManager.Instance.materials[DataManager.Instance.selectorColor];//生成オブジェクトの色を色選択ボタンと色と同じにする
             
             var color =test.GetComponent<Renderer>().material.color;
             test.GetComponent<Renderer>().material.color=new Color(color.r,color.g,color.b,0.5f);//透明度を下げる
            }      
            else//生成するのがユニティちゃんオブジェクトのとき
            {
                Vector3 transforma= DataManager.Instance.target.transform.position;
                Vector3 plus=new Vector3(0.0f,0.05f,0.0f);
                Quaternion rotationa= DataManager.Instance.target.transform.rotation;
                     DataManager.Instance.Judgeprefub= Instantiate(DataManager.Instance.judge, transforma+plus, rotationa);//選択状態のオブジェクトを生成する

            }
    


            Destroy(DataManager.Instance.target);//もともとのオブジェクトを破棄して
            DataManager.Instance.target=test;//新しいオブジェクトを選択する


        }
   
    }


    //削除ボタンを押したら選択したオブジェクトが消える
    public void Delete(){
        if(DataManager.Instance.target.tag!="dammy")
         Destroy(DataManager.Instance.target);
         if(DataManager.Instance.target.tag=="Player")//選択オブジェクトがユニティちゃんだったら選択状態オブジェクトも消す
         Destroy(DataManager.Instance.Judgeprefub);
    }


}
