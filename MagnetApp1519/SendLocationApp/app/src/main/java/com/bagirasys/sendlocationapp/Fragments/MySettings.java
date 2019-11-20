package com.bagirasys.sendlocationapp.Fragments;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Build;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.inputmethod.InputMethodManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.annotation.RequiresApi;
import androidx.fragment.app.Fragment;

import com.bagirasys.sendlocationapp.R;
import com.bagirasys.sendlocationapp.TcpClient;
import com.google.android.material.bottomnavigation.BottomNavigationView;

import static android.content.Context.INPUT_METHOD_SERVICE;

public class MySettings extends Fragment {
    BottomNavigationView bottomNavigationView;


    private Button saveButton;
    private Button secondSocketButton;

    public static String IPAddress;
    public static int port;
    public static int interval;
    public static float distance;

    public static String IPAddress2;
    public static int port2;
    public static int interval2;
    public static float distance2;

    private EditText IPEditText;
    private EditText PortEditText;
    private EditText intervalEditText;
    private EditText metersEditText;
    private SharedPreferences sharedPreferences;
    private SharedPreferences.Editor editor;
    public static MySettings settingsInstance;


    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        super.onCreateView(inflater, container, savedInstanceState);
        View view =inflater.inflate(R.layout.activity_settings,container,false);

        if(IPAddress==null)resetSettings();

        sharedPreferences = this.getActivity().getPreferences(Context.MODE_PRIVATE);
        editor = sharedPreferences.edit();

        saveButton=view.findViewById(R.id.saveButton);
        saveButton.setOnClickListener(new View.OnClickListener() {
            @RequiresApi(api = Build.VERSION_CODES.LOLLIPOP)
            @Override
            public void onClick(View v) {
                save(v);
            }
        });
//        secondSocketButton=view.findViewById(R.id.secondSocketButton);
//        secondSocketButton.setOnClickListener(new View.OnClickListener() {
//            @RequiresApi(api = Build.VERSION_CODES.LOLLIPOP)
//            @Override
//            public void onClick(View v) {
//                openSecond(v);
//            }
//        });


        IPEditText = view.findViewById(R.id.IPEditText);
        PortEditText = view.findViewById(R.id.PortEditText);
        intervalEditText = view.findViewById(R.id.intervalEditText);
        metersEditText = view.findViewById(R.id.metersEditText);


        try {
            IPEditText.setText(sharedPreferences.getString("IP", ""));
            PortEditText.setText(sharedPreferences.getInt("port", 0) + "");
            intervalEditText.setText(sharedPreferences.getInt("interval", 0) + "");
            metersEditText.setText(sharedPreferences.getFloat("distance", (float) 0.0) + "");
        } catch (Exception e) {
            IPEditText.setText(IPAddress);
            PortEditText.setText(Integer.toString(port));
            intervalEditText.setText(Integer.toString(interval));
            metersEditText.setText(Double.toString(distance));
        }
        settingsInstance=this;

        return view;


    }

    public void initSettings(){
        try {
            IPAddress = sharedPreferences.getString("IP", "");
            port = sharedPreferences.getInt("port", 0);
            interval = sharedPreferences.getInt("interval", 0);
            distance = sharedPreferences.getFloat("distance", (float) 0.0);
        }catch(Exception ex){
            Log.e("Init Settings", " "+ex.getMessage());
        }
    }

    //    @Override
//    protected void onCreate(Bundle savedInstanceState) {
//        super.onCreate(savedInstanceState);
//        setContentView(R.layout.activity_settings);
//
//        //if(IPAddress==null)resetSettings();
//
//        sharedPreferences = getPreferences(MODE_PRIVATE);
//        editor = sharedPreferences.edit();
//
//        bottomNavigationView = findViewById(R.id.bottomNavigationMain);
//        bottomNavigationView.setOnNavigationItemSelectedListener(this);
//
//
//        IPEditText = findViewById(R.id.IPEditText);
//        PortEditText = findViewById(R.id.PortEditText);
//        intervalEditText = findViewById(R.id.intervalEditText);
//        metersEditText = findViewById(R.id.metersEditText);
//
//
//        try {
//            IPEditText.setText(sharedPreferences.getString("IP", ""));
//            PortEditText.setText(sharedPreferences.getInt("port", 0) + "");
//            intervalEditText.setText(sharedPreferences.getInt("interval", 0) + "");
//            metersEditText.setText(sharedPreferences.getFloat("distance", (float) 0.0) + "");
//        } catch (Exception e) {
//            IPEditText.setText(IPAddress);
//            PortEditText.setText(Integer.toString(port));
//            intervalEditText.setText(Integer.toString(interval));
//            metersEditText.setText(Double.toString(distance));
//        }
//
//    }

    public static MySettings getInstance(){
        return settingsInstance;
    }

    @RequiresApi(api = Build.VERSION_CODES.LOLLIPOP)
    public void save(View view) {
        try {

            IPAddress = IPEditText.getText().toString();
            port = Integer.parseInt(PortEditText.getText().toString());
            interval = Integer.parseInt(intervalEditText.getText().toString());
            distance = Float.parseFloat(metersEditText.getText().toString());

            editor.putString("IP", IPAddress);
            editor.putInt("port", port);
            editor.putInt("interval", interval);
            editor.putFloat("distance", distance);
            editor.commit();


            //Close the keyboard
            InputMethodManager imm = (InputMethodManager)getActivity().getSystemService(INPUT_METHOD_SERVICE);
            imm.hideSoftInputFromWindow(view.getWindowToken(), 0);
            TcpClient.update();
            //  startActivity(new Intent(this, MainActivity.class));
            Toast.makeText(view.getContext(), "Saved Successfully", Toast.LENGTH_SHORT).show();

        } catch (Exception ex) {
            Log.e("Save in Settings",""+ ex.getMessage());
            Toast.makeText(view.getContext(), "Could not save your request", Toast.LENGTH_SHORT).show();
        }

    }

    public static void resetSettings() {
        IPAddress = "0";
        port = 0;
        interval = 0;
        distance = 0;
    }

//    public void returnToMain(View view) {
//        try {
////            InputMethodManager imm = (InputMethodManager) getSystemService(INPUT_METHOD_SERVICE);
////            imm.hideSoftInputFromWindow(new View(this).getWindowToken(), 0);
//            this.finish();
//        } catch (Exception e) {
//            //startActivity(new Intent(this, MainActivity.class));
//            System.out.println("sorry, " + e.getMessage());
//        }
//    }

    public void openSecond(View view) {
        try {
            InputMethodManager imm = (InputMethodManager)getActivity().getSystemService(INPUT_METHOD_SERVICE);
            imm.hideSoftInputFromWindow(new View(getContext()).getWindowToken(), 0);
            startActivity(new Intent(getActivity(), SecondSocket.class));
        } catch (Exception e) {
            //startActivity(new Intent(this, MainActivity.class));
            System.out.println("sorry, " + e.getMessage());
        }
    }
}
