package to.msn.wings.yubisuma;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.NumberPicker;

public class MainActivity extends AppCompatActivity {
    Button start;
    NumberPicker numberPicker;

    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        start=findViewById(R.id.start);
        numberPicker = findViewById(R.id.max_y);//人数
        numberPicker.setMaxValue(6);//最高人数
        numberPicker.setMinValue(2);//最低人数




        }







    public void btnNew_onClick(View v){
        Intent i = new Intent(this,to.msn.wings.yubisuma.Game.class);
        i.putExtra("member",numberPicker.getValue());
        startActivity(i);
    }
}