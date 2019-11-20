package com.bagirasys.sendlocationapp;

import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Build;
import android.support.annotation.RequiresApi;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.Toast;

import static com.bagirasys.sendlocationapp.MySettings.secondDistance;
import static com.bagirasys.sendlocationapp.MySettings.secondIPAddress;
import static com.bagirasys.sendlocationapp.MySettings.secondInterval;
import static com.bagirasys.sendlocationapp.MySettings.secondPort;
@RequiresApi(api = Build.VERSION_CODES.LOLLIPOP)
public class seconedSocket extends AppCompatActivity {

    private LinearLayout secondLayout;
    private EditText IPEditText;
    private EditText PortEditText;
    private EditText intervalEditText;
    private EditText metersEditText;
    private SharedPreferences sharedPreferences;
    private SharedPreferences.Editor editor;



    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        setContentView(R.layout.activity_seconed_socket);
        secondLayout=findViewById(R.id.secondLayout);
        IPEditText=findViewById(R.id.IPEditText);
        PortEditText=findViewById(R.id.PortEditText);
        intervalEditText=findViewById(R.id.intervalEditText);
        metersEditText=findViewById(R.id.metersEditText);
        sharedPreferences=getPreferences(MODE_PRIVATE);
        editor=sharedPreferences.edit();

        try{
            IPEditText.setText(sharedPreferences.getString("secondIP",""));
            PortEditText.setText(sharedPreferences.getInt("secondPort",0)+"");
            intervalEditText.setText(sharedPreferences.getInt("secondInterval",0)+"");
            metersEditText.setText(sharedPreferences.getFloat("secondDistance",(float) 0.0)+"");
        }catch(Exception e) {
            IPEditText.setText(secondIPAddress);
            PortEditText.setText(Integer.toString(secondPort));
            intervalEditText.setText(Integer.toString(secondInterval));
            metersEditText.setText(Double.toString(secondDistance));
        }
    }



    public void save(View view) {
        try{
            MySettings.secondIPAddress= IPEditText.getText().toString();
            MySettings.secondPort= Integer.parseInt(PortEditText.getText().toString());
            MySettings.secondInterval= Integer.parseInt(intervalEditText.getText().toString());
            MySettings.secondDistance= Float.parseFloat(metersEditText.getText().toString());

            editor.putString("secondIP",secondIPAddress);
            editor.putInt("secondPort",secondPort);
            editor.putInt("secondInterval",secondInterval);
            editor.putFloat("secondDistance",secondDistance);
            editor.commit();

            //Close The Keyboard
            InputMethodManager imm = (InputMethodManager)getSystemService(INPUT_METHOD_SERVICE);
            imm.hideSoftInputFromWindow(getCurrentFocus().getWindowToken(), 0);
            TcpClient.update();
            startActivity(new Intent(this, MainActivity.class));
        }catch(Exception ex){
            Toast.makeText(this, "Could not save your request", Toast.LENGTH_SHORT).show();
        }
    }

    public void returnToMain(View view) {
        startActivity(new Intent(this, MySettings.class));
    }
}
