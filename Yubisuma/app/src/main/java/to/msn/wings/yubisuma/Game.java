package to.msn.wings.yubisuma;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.NumberPicker;
import android.widget.TextView;

import androidx.appcompat.app.AppCompatActivity;

import java.util.Random;

public class Game extends AppCompatActivity {
    int[][] a ;//各人の手a[0][]があなた
    int sum = 0;//手の合計
    int cnt ;//誰が親か
    int cnt2 = 0;//手が減った数
    int i0= 0, j;//for文の中身
    int yosoku;//予測最大数
    Button btn0;//0ボタン
    Button btn1;//1ボタン
    Button btn2;//2ボタン
    Button reset;//リセットボタン
    NumberPicker max_y;//予測数を選ぶナンバーピッカー
    StringBuilder score_b;//スコア表示用のStringBuilder
    StringBuilder result_b;//結果表示用のStringBuilder
    int member;//参加人数
    TextView score;//スコア表示のテキストビュー
    TextView result;//結果表示のテキストビュー
    Random rand;//コンピュータの手をランダムで決める
    Intent i;//インテント

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_game);

        rand = new Random();
        i = getIntent();
        member = i.getIntExtra("member",0);//人数
        a = new int[member][2];
        score =findViewById(R.id.score);//TV
        result=findViewById(R.id.result);//TV
        reset =findViewById(R.id.reset);//Button
        btn0=findViewById(R.id.button);//Button
        btn1=findViewById(R.id.button2);//Button
        btn2=findViewById(R.id.button3);//Button

        max_y=findViewById(R.id.max_y);//ナンバーピッカー
        max_y.setMaxValue(2 * member - cnt2 );//最高予測数
        max_y.setMinValue(0);//最低予測数



    }

    public void onClick(View view){
        switch(view.getId()){
            case R.id.button :a[0][0]=0;
                                if(a[0][1]!=3)
                               a[0][1]=0;
                             break;
            case R.id.button2 :a[0][0]=1;
                              if(a[0][1]!=3)
                                a[0][1]=0;
                                break;
            case R.id.button3 :
                                a[0][0]=1;
                                a[0][1]=1;
                                break;
        }
        score_b= new StringBuilder();//スコア　テキストビューに表示する用
        result_b= new StringBuilder();//結果　テキストビューに表示する用

        //合計数を予測する
        if (cnt == 0) {

            yosoku = max_y.getValue();
            score_b.append("あなたの予測 "+yosoku+"\n");
        } else {

            yosoku = rand.nextInt(2 * member - cnt2 + 1);//コンピュータの予測　最大値を出すため+1
            score_b.append((cnt+1)+" 人目の予測 "+yosoku+"\n");
        }

        //あなたの手
        score_b.append("   あなたの手  " + a[0][0]);//片手を表示
        if (a[0][1] == 3)//当たっていたらもう片手を表示しない
            score_b.append(" \n");
        else
            score_b.append(" " + a[0][1]+"\n");//もう片手を表示する

        //コンピュータの手を決める
        for (i0 = 1; i0 < a.length; i0++) {//コンピューターの手a[0][j]はあなたなので1開始
            for (j = 0; j < 2; j++) {
                if (a[i0][j] == 3) //一度勝ってるとき片手を飛ばす
                    continue;
                a[i0][j] = rand.nextInt(2);//手をランダムで割り振る
                if(yosoku==2 * member - cnt2&&cnt!=0)//もしも自分の予測数が最大だった場合自分の手を最大にする
                    if(a[cnt][1]!=3)
                    a[cnt][j] = 1;
                    else
                        a[cnt][0] = 1;

                else if(yosoku==2 * member-1 - cnt2+1&&cnt!=0){//もしも自分の予測数が最大-1だった場合自分の手を1か2にするため片方を1に固定する
                    a[cnt][0]=1;
                }

                else if(yosoku==0&&cnt!=0)//もしも自分の予測数が0だった時自分の手を0にする
                    if(a[cnt][1]==3)
                    a[cnt][0] = 0;
                    else
                        a[cnt][j] = 0;

                else if(yosoku==1&&cnt!=0) {//もしも自分の予測数が1だった時2を出さないようにする
                    if(a[cnt][1]!=3)
                        a[cnt][1]=0;
                }
            }

            score_b.append("    "+(i0 + 1) + " 人目の手  " + a[i0][0]);//片手を表示
            if (a[i0][1] == 3)//もう片手を表示
                score_b.append(" \n");//一度当ててたら表示しない
            else
                score_b.append(" " + a[i0][1]+"\n");
        }

        //合計の算出
        for (i0 = 0; i0 < a.length; i0++) {
            for (j = 0; j < 2; j++) {

                if (a[i0][j] == 3)//抜けてる手は飛ばす
                    continue;

                sum += a[i0][j];
            }
        }
        score_b.append("              合計   "+sum+"\n\n");//合計を表示

        //あたり判定
        if (sum == yosoku) {

            //一度当たってるとき
            if (a[cnt][1] == 3) {
                btn0.setEnabled(false);//終了なのでグレーアウトさせる
                btn1.setEnabled(false);
                btn2.setEnabled(false);
                if (cnt == 0) {
                    result_b.append("あなたの勝ちです");
                    a[0][0] = 3;


                } else {
                    result_b.append((cnt+1) + "人目の人の勝ちです");
                    a[cnt][0] = 3;
                }
            }
            //一度も当たってないとき
            else {
                if (cnt == 0) {
                    result_b.append("あなたのあたりです\n");
                    a[cnt][1] = 3;
                    btn2.setEnabled(false);
                    cnt2++;
                } else {
                    result_b.append((cnt+1) + "人目の人あたりです\n");
                    a[cnt][1] = 3;
                    cnt2++;
                }
            }

        } else//はずれたとき
            result_b.append("はずれです\n");
        sum = 0;

        //親を次にする。一周回ったら親を自分に戻す
        cnt++;

        if(a[cnt-1][0]!=3) {
            if (cnt == member)
                cnt = 0;

            //次の親を表示
            if (cnt == 0)
                result_b.append("\n次はあなたが親です");
            else
                result_b.append("\n次は"+(cnt+1) +"人目の人が親です");


        }



        //手が減った時に予測数を減らす
        max_y.setMaxValue(2 * member - cnt2 );//最高予測数

        //ためたテキストを表示する
        score.setText(score_b.toString());
        result.setText(result_b.toString());



    }

    //やり直しボタン
    public void reset (View v){
        finish();
    }

}