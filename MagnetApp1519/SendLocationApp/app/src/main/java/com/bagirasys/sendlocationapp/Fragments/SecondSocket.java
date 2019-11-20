package com.bagirasys.sendlocationapp.Fragments;

import android.content.SharedPreferences;
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
import androidx.fragment.app.Fragment;

import com.bagirasys.sendlocationapp.R;

import static android.content.Context.INPUT_METHOD_SERVICE;
import static com.bagirasys.sendlocationapp.Fragments.MySettings.IPAddress2;
import static com.bagirasys.sendlocationapp.Fragments.MySettings.distance2;
import static com.bagirasys.sendlocationapp.Fragments.MySettings.interval2;
import static com.bagirasys.sendlocationapp.Fragments.MySettings.port2;

public class SecondSocket extends Fragment{
    public static SecondSocket secondInstance;
    public  String IPAddress="";
    public  int port=0;
    public  int interval=0;
    public  float distance=0;

    private EditText IPEditText;
    private EditText PortEditText;
    private EditText intervalEditText;
    private EditText metersEditText;
    private SharedPreferences sharedPreferences;
    private SharedPreferences.Editor editor;
    private Button save2;


    public void updateTCP(){
        try{
            IPAddress2=IPAddress;
            port2=port;
            interval2=interval;
            distance2=distance;
        }catch(Exception e){
            Log.d("Error in Second Socket", e.getMessage());
        }
    }

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        super.onCreateView(inflater, container, savedInstanceState);
        View view =inflater.inflate(R.layout.activity_second_socket,container,false);
        sharedPreferences = this.getActivity().getPreferences(getContext().MODE_PRIVATE);
        editor = sharedPreferences.edit();

        updateTCP();
        save2=view.findViewById(R.id.saveButton);
        save2.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                save2(v);
            }
        });
        IPEditText= view.findViewById(R.id.IPEditText);
        PortEditText= view.findViewById(R.id.PortEditText);
        intervalEditText= view.findViewById(R.id.intervalEditText);
        metersEditText= view.findViewById(R.id.metersEditText);


        try{
            IPEditText.setText(sharedPreferences.getString("IP2",""));
            PortEditText.setText(sharedPreferences.getInt("port2",0)+"");
            intervalEditText.setText(sharedPreferences.getInt("interval2",0)+"");
            metersEditText.setText(sharedPreferences.getFloat("distance2",(float) 0.0)+"");
        }catch(Exception e){
            IPEditText.setText(IPAddress);
            PortEditText.setText(Integer.toString(port));
            intervalEditText.setText(Integer.toString(interval));
            metersEditText.setText(Double.toString(distance));
        }



        secondInstance=this;
        return view;

    }

    //    @Override
//    protected void onCreate(Bundle savedInstanceState) {
//        super.onCreate(savedInstanceState);
//        setContentView(R.layout.activity_second_socket);
//
//        sharedPreferences = getPreferences(MODE_PRIVATE);
//        editor = sharedPreferences.edit();
//
//        bottomNavigationView = findViewById(R.id.bottomNavigationMain);
//        bottomNavigationView.setOnNavigationItemSelectedListener(this);
//
//        updateTCP();
//
//        IPEditText= findViewById(R.id.IPEditText);
//        PortEditText= findViewById(R.id.PortEditText);
//        intervalEditText= findViewById(R.id.intervalEditText);
//        metersEditText= findViewById(R.id.metersEditText);
//
//
//        try{
//            IPEditText.setText(sharedPreferences.getString("IP2",""));
//            PortEditText.setText(sharedPreferences.getInt("port2",0)+"");
//            intervalEditText.setText(sharedPreferences.getInt("interval2",0)+"");
//            metersEditText.setText(sharedPreferences.getFloat("distance2",(float) 0.0)+"");
//        }catch(Exception e){
//            IPEditText.setText(IPAddress);
//            PortEditText.setText(Integer.toString(port));
//            intervalEditText.setText(Integer.toString(interval));
//            metersEditText.setText(Double.toString(distance));
//        }
//    }

    public static SecondSocket getInstance(){
            return secondInstance;
    }

    public void save2(View view) {
        try{

            IPAddress= IPEditText.getText().toString();
            port= Integer.parseInt(PortEditText.getText().toString());
            interval= Integer.parseInt(intervalEditText.getText().toString());
            distance= Float.parseFloat(metersEditText.getText().toString());

            editor.putString("IP2",IPAddress);
            editor.putInt("port2",port);
            editor.putInt("interval2",interval);
            editor.putFloat("distance2",distance);
            editor.commit();

            //Close The Keyboard
            InputMethodManager imm = (InputMethodManager)getActivity().getSystemService(INPUT_METHOD_SERVICE);
            imm.hideSoftInputFromWindow(view.getWindowToken(), 0);

            updateTCP();
            //  startActivity(new Intent(this, MainActivity.class));
            Toast.makeText(view.getContext(), "Saved second socket Successfully", Toast.LENGTH_SHORT).show();

        }catch(Exception ex){
            Toast.makeText(getActivity(), "Could not save your second socket request", Toast.LENGTH_SHORT).show();
            Log.d("Saving Problem", ex.getMessage());
        }

    }



//    public void returnToMain(View view) {
//        try {
////            InputMethodManager imm = (InputMethodManager) getSystemService(INPUT_METHOD_SERVICE);
////            imm.hideSoftInputFromWindow(new View(this).getWindowToken(), 0);
//            this.finish();
//        }catch(Exception e) {
//            //startActivity(new Intent(this, MainActivity.class));
//            System.out.println("sorry, "+e.getMessage());
//        }
//    }

}
