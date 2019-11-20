package com.bagirasys.sendlocationapp;

import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Build;
import android.support.annotation.RequiresApi;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.Toast;
@RequiresApi(api = Build.VERSION_CODES.LOLLIPOP)
public class MySettings extends AppCompatActivity {


    private Button openSecond;
    private CheckBox secondCheckBox;
    public static String IPAddress;
    public static int port;
    public static int interval;
    public static float distance;

    public static String secondIPAddress;
    public static int secondPort;
    public static int secondInterval;
    public static float secondDistance;
    boolean isUsing=false;
    private EditText IPEditText;
    private EditText PortEditText;
    private EditText intervalEditText;
    private EditText metersEditText;
    private SharedPreferences sharedPreferences;
    private SharedPreferences.Editor editor;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_settings);

        //if(IPAddress==null)resetSettings();
        secondCheckBox=findViewById(R.id.secondCheckBox);
        openSecond=findViewById(R.id.openSecond);

        sharedPreferences=getPreferences(MODE_PRIVATE);
        editor=sharedPreferences.edit();

        editor.putBoolean("second",isUsing);

        IPEditText= findViewById(R.id.IPEditText);
        PortEditText= findViewById(R.id.PortEditText);
        intervalEditText= findViewById(R.id.intervalEditText);
        metersEditText= findViewById(R.id.metersEditText);


        try{
            IPEditText.setText(sharedPreferences.getString("IP",""));
            PortEditText.setText(sharedPreferences.getInt("port",0)+"");
            intervalEditText.setText(sharedPreferences.getInt("interval",0)+"");
            metersEditText.setText(sharedPreferences.getFloat("distance",(float) 0.0)+"");
            isUsing=sharedPreferences.getBoolean("second",false);
        }catch(Exception e){
            IPEditText.setText(IPAddress);
            PortEditText.setText(Integer.toString(port));
            intervalEditText.setText(Integer.toString(interval));
            metersEditText.setText(Double.toString(distance));
        }
        secondCheckBox.setChecked(isUsing);
        if(isUsing){
            openSecond.setVisibility(View.VISIBLE);
        }
        else{
            openSecond.setVisibility(View.GONE);
        }



    }

    public void save(View view) {
        try{

            IPAddress= IPEditText.getText().toString();
            port= Integer.parseInt(PortEditText.getText().toString());
            interval= Integer.parseInt(intervalEditText.getText().toString());
            distance= Float.parseFloat(metersEditText.getText().toString());


            editor.putString("IP",IPAddress);
            editor.putInt("port",port);
            editor.putInt("interval",interval);
            editor.putFloat("distance",distance);
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
    public static void resetSettings(){
        IPAddress="0";
        port=0;
        interval=0;
        distance=0;
        secondIPAddress="0";
        secondPort=0;
        secondInterval=0;
        secondDistance=0;

    }

    public void returnToMain(View view) {
        try {
            InputMethodManager imm = (InputMethodManager) getSystemService(INPUT_METHOD_SERVICE);
            imm.hideSoftInputFromWindow(getCurrentFocus().getWindowToken(), 0);
        }catch(Exception e){}
        startActivity(new Intent(this, MainActivity.class));
    }

    public void triggerSecond(View view) {
        isUsing=((CheckBox)view).isChecked();
        MainActivity.isSecondSocket=isUsing;
        if(isUsing){
            openSecond.setVisibility(View.VISIBLE);
        }else{
            openSecond.setVisibility(View.GONE);
        }
        editor.putBoolean("second",isUsing);
        editor.commit();
    }

    public void openSecond(View view) {
        startActivity(new Intent(this, seconedSocket.class));
    }
}
