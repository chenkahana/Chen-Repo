package com.bagirasys.sendlocationapp.Fragments;

import android.content.SharedPreferences;
import android.graphics.Color;
import android.graphics.drawable.Drawable;
import android.hardware.Sensor;
import android.hardware.SensorManager;
import android.location.LocationManager;
import android.os.Build;
import android.os.Bundle;
import android.provider.Settings;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.FrameLayout;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;
import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.annotation.RequiresApi;
import androidx.fragment.app.Fragment;
import androidx.viewpager.widget.ViewPager;
import com.bagirasys.sendlocationapp.Activities.MainActivity;
import com.bagirasys.sendlocationapp.Location;
import com.bagirasys.sendlocationapp.R;
import com.bagirasys.sendlocationapp.SectionsPagerAdapter;
import com.bagirasys.sendlocationapp.User;

@RequiresApi(api = Build.VERSION_CODES.LOLLIPOP)
public class MainFragment extends Fragment {
    public static MainFragment mainFragment;

    public TextView androidIdTextView;
    public Location myLocation;
    public LocationManager locationmanager;
    public Button getButton;
    public Button postButton;
    public TextView connectionTextView;
    public String androidId;
    public String androidUnique;
    public TextView androUniqueIdTextView;
    public ImageView connectionImageView;
    public static String metID;
    public static int damage;
    public static String entityName;

    public String startLocation = "Send Location";
    public String stopLocation = "Stop Sending";
    public static User user = new User();
    public LinearLayout layout;
    public Drawable background;
    public Button fireButton;
    public boolean isImageGreen = false;
    public static boolean firstSocket = false;
    public boolean getButtonToggle = true;
    public MainActivity base;
    public static final Object flagConnection = new Object();
    public static com.bagirasys.sendlocationapp.Activities.MainActivity mainActivity;
    public SensorManager mSensorManager;
    public Sensor mAccelerometer;
    public SectionsPagerAdapter mSectionsPagerAdapter;
    public ViewPager mViewPager;
    public FrameLayout mainFrameLayout;
    public LayoutInflater inflater;
    public View view;
    private SharedPreferences sharedPreferences;
    private SharedPreferences.Editor editor;
    public boolean connectionState;
    private boolean sendindgState;

    @Override
    public void onResume() {
        super.onResume();
//        try {
//            connectionState = sharedPreferences.getBoolean("connectionState", false);
//            if (connectionState) {
//                connctionGood();
//            } else {
//                connctionBad();
//            }
//            sendindgState = sharedPreferences.getBoolean("sendindgState", false);
//            changeSendingStatus(sendindgState);
//        } catch (Exception e) {
//            connectionState = false;
//            sendindgState = false;
//        }
    }

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if(savedInstanceState!=null){
            connectionState=savedInstanceState.getBoolean("connectionState");
            if(connectionState){
                connctionGood();
            }else{
                connctionBad();
            }
        }
    }
    private FrameLayout frameLayout;
    @Override
    public void onActivityCreated(@Nullable Bundle savedInstanceState) {
        super.onActivityCreated(savedInstanceState);
        frameLayout=base.findViewById(R.id.mainFrameLayout);
    }

    @RequiresApi(api = Build.VERSION_CODES.LOLLIPOP)
    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        super.onCreateView(inflater, container, savedInstanceState);
        base = MainActivity.getInstance();
        mainFragment = this;
        //mainRelevantLayout=base.findViewById(R.id.mainRelevantLayout);
        view = inflater.inflate(R.layout.main_fragment, container, false);
        connectionImageView = view.findViewById(R.id.connectionImageView);
        myLocation = new Location();
        connectionTextView = view.findViewById(R.id.connectionTextView);
        androidIdTextView = view.findViewById(R.id.androidIdTextView);
        postButton = view.findViewById(R.id.postLocationButton);
        postButton.setEnabled(false);
        postButton=base.findViewById(R.id.postLocationButton);
        getButton = base.findViewById(R.id.getLocationButton);
        getButton.setEnabled(false);
        androidId = Location.getID();
        androidUnique = Settings.Secure.getString(getActivity().getContentResolver(), Settings.Secure.ANDROID_ID);
        user.setName(androidId);
        user.setId(androidUnique);
        myLocation.setUniqueID(androidUnique);
        androUniqueIdTextView = view.findViewById(R.id.androUniqueIdTextView);
        androUniqueIdTextView.setText(androUniqueIdTextView.getText() + androidUnique);
        androidIdTextView.setText(androidIdTextView.getText() + androidId);
        locationmanager = (LocationManager) getActivity().getSystemService(getContext().LOCATION_SERVICE);
        layout = view.findViewById(R.id.mainLayout);
        background = getActivity().getDrawable(R.drawable.background);
        fireButton = view.findViewById(R.id.fireButton);
        fireButton.setEnabled(false);
        sharedPreferences = this.getActivity().getPreferences(getContext().MODE_PRIVATE);
        editor = sharedPreferences.edit();
        fireButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                base.fireNow(v);
            }
        });
        getButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                base.getLocation(v);
            }
        });
        postButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                base.postLocation(v);
            }
        });
        if(savedInstanceState!=null) {
            connectionState = savedInstanceState.getBoolean("connectionState");
            if (connectionState) {
                connctionGood();
            } else {
                connctionBad();
            }
        }
        base.checkConnection();
        return view;
    }

    @Override
    public void onSaveInstanceState(@NonNull Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putBoolean("connectionState",connectionState);
    }

    public static MainFragment getInstance() {
        return mainFragment;
    }


    public void connctionGood() {
        if (!(isImageGreen)) {
            if (connectionImageView == null) {
                connectionImageView = view.findViewById(R.id.connectionImageView);
            }
            getButton=view.findViewById(R.id.getLocationButton);
            getButton.setText("Disconnect");
            getButton=base.findViewById(R.id.getLocationButton);
            connectionImageView.setImageResource(R.drawable.ic_wifi_on);
            connectionTextView.setText("Connected");
            connectionTextView.setTextColor(Color.WHITE);
            isImageGreen = true;
            editor.putBoolean("connectionState", true);
            editor.commit();
            postButton=view.findViewById(R.id.postLocationButton);
            postButton.setEnabled(true);
            postButton=base.findViewById(R.id.postLocationButton);
            fireButton=view.findViewById(R.id.fireButton);
            fireButton.setEnabled(true);
            fireButton=base.findViewById(R.id.fireButton);


        }
    }
    public void init(){
        try{
            connectionState=sharedPreferences.getBoolean("connectionState",false);
            if(connectionState){
                connctionGood();
                return;
            }
            connctionBad();
        }catch(Exception ex){
            Log.e("Init Main Fragment", " "+ex.getMessage());
        }
    }

    public void connctionBad() {
        if (isImageGreen) {
            if (connectionImageView == null) {
                connectionImageView = view.findViewById(R.id.connectionImageView);
            }
            getButton=view.findViewById(R.id.getLocationButton);
            getButton.setText("Connect");
            getButton=base.findViewById(R.id.getLocationButton);
            connectionImageView.setImageResource(R.drawable.ic_wifi_off);
            connectionTextView.setText("Not Connected");
            connectionTextView.setTextColor(Color.WHITE);
            isImageGreen = false;
            editor.putBoolean("connectionState", false);
            editor.commit();
            postButton=view.findViewById(R.id.postLocationButton);
            postButton.setEnabled(false);
            changeSendingStatus(false);
            postButton=base.findViewById(R.id.postLocationButton);
            fireButton=view.findViewById(R.id.fireButton);
            fireButton.setEnabled(false);
            fireButton=base.findViewById(R.id.fireButton);
        }
    }


    public void changeSendingStatus(boolean condition) {
        postButton=view.findViewById(R.id.postLocationButton);
        postButton.setText(condition ? stopLocation : startLocation);
        postButton=base.findViewById(R.id.postLocationButton);
        editor.putBoolean("sendindgState", condition);
        editor.commit();
    }


}

