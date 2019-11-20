package com.bagirasys.sendlocationapp.Activities;

import android.Manifest;
import android.annotation.SuppressLint;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.location.LocationListener;
import android.location.LocationManager;
import android.media.MediaPlayer;
import android.os.AsyncTask;
import android.os.Build;
import android.os.VibrationEffect;
import android.os.Vibrator;
import android.provider.Settings;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.MenuItem;
import android.view.MotionEvent;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.annotation.RequiresApi;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.app.ActivityCompat;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;
import androidx.viewpager.widget.ViewPager;

import com.bagirasys.sendlocationapp.Fragments.MainFragment;
import com.bagirasys.sendlocationapp.Fragments.MySettings;
import com.bagirasys.sendlocationapp.Fragments.SecondSocket;
import com.bagirasys.sendlocationapp.Fragments.UserInfo;
import com.bagirasys.sendlocationapp.Location;
import com.bagirasys.sendlocationapp.LocationService;
import com.bagirasys.sendlocationapp.Messages.FireMessage;
import com.bagirasys.sendlocationapp.Messages.MessageBase;
import com.bagirasys.sendlocationapp.Messages.UserMessage;
import com.bagirasys.sendlocationapp.R;
import com.bagirasys.sendlocationapp.SectionsPagerAdapter;
import com.bagirasys.sendlocationapp.TcpClient;
import com.bagirasys.sendlocationapp.Threads.SendingTheard;
import com.bagirasys.sendlocationapp.User;
import com.google.android.material.bottomnavigation.BottomNavigationView;
import com.google.gson.Gson;

import java.util.ArrayList;

@RequiresApi(api = Build.VERSION_CODES.LOLLIPOP)
public class MainActivity extends AppCompatActivity implements SensorEventListener {
    private BottomNavigationView bottomNavigationView;
    private TextView androidIdTextView;
    public Location myLocation;
    private LocationManager locationmanager;
    public LocationListener locationListener;
    private Button getButton;
    private Button postButton;
    public boolean isSub = false;
    private TextView connectionTextView;
    public String androidId;
    public String androidUnique;
    private TextView androUniqueIdTextView;
    public TcpClient mTcpClient;
    public TcpClient mTcpClient2;
    public static boolean connectionSucsses = false;
    private ImageView connectionImageView;
    public static int metID;
    public static int damage;
    public static String entityName;
    private String startLocation = "Send Location";
    private String stopLocation = "Stop Sending";
    public static User user = new User();
    private Thread t;
    public MediaPlayer gettingShot;
    public MediaPlayer shootSound;
    private Thread shot;
    private LinearLayout layout;
    public final Object flagLocation = new Object();
    private Button fireButton;
    public boolean isImageGreen = false;
    public static boolean firstSocket = false;
    private boolean getButtonToggle = true;
    public static MainFragment mainFragment;
    private MySettings settingsFragment;
    private SecondSocket secondFragment;
    private UserInfo userFragment;
    private Thread firstTcpClient;
    private Thread secondTcpClient;
    private boolean sending;
    private boolean isFirstEntity;

    public ArrayList<TcpClient> listOfClients = new ArrayList<>();
    public SendingTheard sendingTheard = new SendingTheard() {

        @Override
        public void interrupt() {
            running = false;
            sending = false;
        }

        @Override
        public void run() {
            try {

                if (running && (!(listOfClients.isEmpty()))) {
                    sending = true;
                    for (int i = 0; i < listOfClients.size(); i++) {
                        synchronized (flagLocation) {
                            String json = myLocation.getAsJSON();
                            listOfClients.get(i).sendMessage(json);
                        }
                    }
                }

            } catch (Exception e) {
                System.out.println(" " + e.getMessage());
            }
        }

    };

    public static void changeConnection(boolean condition) {
        synchronized (flagConnection) {
            connectionSucsses = condition;
        }
        if (connectionSucsses) {
            mainFragment.connctionGood();
        } else {
            mainFragment.connctionBad();
        }
    }

//    @Override
//    protected void onUserLeaveHint() {
//        mainActivity=this;
//        startService(new Intent(getBaseContext(), LocationService.class));
//    }


    @Override
    protected void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        if (mViewPager != null) {
            outState.putInt("view", mViewPager.getCurrentItem());
        }
        Log.e("Save", "On Save Was Called");
        mainActivity = this;
    }

    @Override
    public void onRestoreInstanceState(Bundle savedInstanceState) {
        super.onRestoreInstanceState(savedInstanceState);
        Log.e("Restore", "OnRestore Was Called");
        int myInt = savedInstanceState.getInt("view");
    }

    @Override
    protected void onStop() {
        super.onStop();
        mainActivity = this;
        Log.e("Stop", "On Stop Was Called");
        Intent serviceIntent = new Intent(getBaseContext(), LocationService.class);
        if (mViewPager != null) {
            serviceIntent.putExtra("view", mViewPager.getCurrentItem());
        }
        startService(serviceIntent);

    }

    public static final Object flagConnection = new Object();
    public static MainActivity mainActivity;
    private SensorManager mSensorManager;
    private Sensor mAccelerometer;
    private SectionsPagerAdapter mSectionsPagerAdapter;
    private ViewPager mViewPager;

    public void updateFragments1() {

        mainFragment = new MainFragment();
        settingsFragment = new MySettings();
        settingsFragment.initSettings();
        secondFragment = new SecondSocket();
        userFragment = new UserInfo();
    }

    public void updateFragments() {
        mainFragment = MainFragment.getInstance();
        if (mainFragment == null) {
            mainFragment = new MainFragment();
        }
        checkConnection();
        settingsFragment = MySettings.getInstance();
        if (settingsFragment == null) {
            settingsFragment = new MySettings();
        }
        settingsFragment.initSettings();
        secondFragment = SecondSocket.getInstance();
        if (secondFragment == null) {
            secondFragment = new SecondSocket();
        }
        userFragment = UserInfo.getInstance();
        if (userFragment == null) {
            userFragment = new UserInfo();
        }

    }

    @Override
    protected void onStart() {
        super.onStart();
        Log.e("Start", "On Start Was Called");
        try {
            mViewPager.setCurrentItem(getIntent().getIntExtra("view", 1));
            mainFragment = new MainFragment();

        } catch (Exception e) {
            Log.e("onStart", e.getMessage() + "");
        }

    }


    @SuppressLint("NewApi")
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        Log.e("Create", "On Create Was Called");


        mSensorManager = (SensorManager) getSystemService(SENSOR_SERVICE);
        mAccelerometer = mSensorManager.getDefaultSensor(Sensor.TYPE_ACCELEROMETER);


        if (Location.getID() == null) {
            startActivity(new Intent(this, LoginActivity.class));
        } else {
            isFirstEntity = true;
            updateFragments1();
            mainActivity = this;
            //Nav Bar Listener

            mViewPager = findViewById(R.id.container1);
            if (mSectionsPagerAdapter == null) {
                setupViewPager(mViewPager);
            }
            int view = getIntent().getIntExtra("view", 1);
            mViewPager.setCurrentItem(view);
            setupBottomNavigationView();
            shootSound = MediaPlayer.create(this, R.raw.gungunshot01);
            gettingShot = MediaPlayer.create(this, R.raw.gettingshot);
            myLocation = new Location();
            androidId = Location.getID();
            androidUnique = Settings.Secure.getString(getContentResolver(), Settings.Secure.ANDROID_ID);
            user.setName(androidId);
            user.setId(androidUnique);
            myLocation.setUniqueID(androidUnique);
            locationmanager = (LocationManager) getSystemService(LOCATION_SERVICE);
            locationListener = new LocationListener() {
                @Override
                public void onLocationChanged(android.location.Location location) {
                    if (user.getDmg() < 100) {
                        synchronized (flagLocation) {
                            //checkConnection("", true);
                            myLocation.setLat(location.getLatitude());
                            myLocation.setLon(location.getLongitude());
                            myLocation.setAlt(location.getAltitude());
                            myLocation.setSpeed(location.getSpeed());
                            if (isSub && !(listOfClients.isEmpty())) {
                                sendingTheard.run();
                            }
                        }
                    }
                }

                @Override
                public void onStatusChanged(String provider, int status, Bundle extras) {

                }

                @Override
                public void onProviderEnabled(String provider) {

                }

                @Override
                public void onProviderDisabled(String provider) {
                    Intent intent = new Intent(Settings.ACTION_LOCATION_SOURCE_SETTINGS);
                    startActivity(intent);
                }
            };
            if (ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
                requestPermissions(new String[]{
                        Manifest.permission.ACCESS_COARSE_LOCATION, Manifest.permission.ACCESS_FINE_LOCATION,
                        Manifest.permission.INTERNET
                }, 10);
                return;
            } else {
                //getButton.setEnabled(true);
            }
        }
    }

    public static MainActivity getInstance() {
        return mainActivity;
    }

    private void setupViewPager(ViewPager viewPager) {
        mSectionsPagerAdapter = new SectionsPagerAdapter(getSupportFragmentManager());
        viewPager.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                return true;
            }
        });
        mSectionsPagerAdapter.addFragment(userFragment);
        mSectionsPagerAdapter.addFragment(mainFragment);
        mSectionsPagerAdapter.addFragment(settingsFragment);
        mSectionsPagerAdapter.addFragment(secondFragment);
        viewPager.setAdapter(mSectionsPagerAdapter);
        viewPager.setOffscreenPageLimit(4);
    }

    @Override
    protected void onResume() {
        super.onResume();
        if (mSensorManager != null) {
            mSensorManager.registerListener(this, mAccelerometer, SensorManager.SENSOR_DELAY_NORMAL);

        } else {
            mSensorManager = (SensorManager) getSystemService(SENSOR_SERVICE);
            mAccelerometer = mSensorManager.getDefaultSensor(Sensor.TYPE_ACCELEROMETER);
            mSensorManager.registerListener(this, mAccelerometer, SensorManager.SENSOR_DELAY_NORMAL);
        }

        try {
            mViewPager.setCurrentItem(1);
        } catch (Exception e) {
            Log.e("onResume", e.getMessage() + "");
        }

    }


    @Override
    public void onRequestPermissionsResult(int requestCode, @NonNull String[] permissions,
                                           @NonNull int[] grantResults) {
        switch (requestCode) {
            case 10:
                if (grantResults.length > 0 && grantResults[0] == PackageManager.PERMISSION_GRANTED) {
                    if (mainFragment.getButton == null) {
                        mainFragment.getButton = findViewById(R.id.getLocationButton);
                    }
                    mainFragment.getButton.setEnabled(true);
                }
        }
    }


    public boolean checkIfSending() {
        if (sending) {
            return true;
        }
        return false;
    }

    @SuppressLint("MissingPermission")
    public void postLocation(View view) {
        if (!(listOfClients.isEmpty())) {
            if (!(checkIfSending())) {
                isSub = true;
                mainFragment.changeSendingStatus(true);
            } else {
                mainFragment.changeSendingStatus(false);
                isSub = false;
                try {
                    sendingTheard.sleep(100);
                    sendingTheard.interrupt();
                } catch (Exception e) {
                    System.out.println(" " + e.getMessage());
                }
            }
            if (isSub) {
                if (sendingTheard.running)
                    sendingTheard.run();
                else {
                    sendingTheard.running = true;
                }
            }
        }
    }


    @SuppressLint("MissingPermission")
    public void getLocation(View view) {
        try {
            if (getButtonToggle) {
                new ConnectTask().execute("");
                Thread.sleep(250);

                if (!(listOfClients.isEmpty())) {
                    for (TcpClient t : listOfClients) {
                        t.update();
                    }

                    locationmanager.requestLocationUpdates("gps", 0, MySettings.distance, locationListener);
                    getButtonToggle = false;
                }

                Thread.sleep(250);
                //checkConnection("", true);
            } else {
                if (mTcpClient != null) {
                    mTcpClient.stopClient();
                    mainFragment.connctionBad();
                    getButtonToggle = true;
                }
                try {
                    firstTcpClient.interrupt();
                    secondTcpClient.interrupt();
                } catch (Exception ex) {
                    Log.e("Interrupt", " " + ex.getMessage());
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        //Toast.makeText(this, "Location Service Has Been Activated", Toast.LENGTH_SHORT).show();


    }


    @Override
    public void onSensorChanged(SensorEvent event) {
        float[] mMatrixR = new float[9];
        float[] mMatrixValues = new float[3];

        SensorManager.getRotationMatrixFromVector(mMatrixR, event.values);

        SensorManager.getOrientation(mMatrixR, mMatrixValues);

        double bearing = Math.toDegrees(mMatrixValues[0]);
        try {
            synchronized (flagLocation) {
                myLocation.setBearing(bearing);
            }
        } catch (Exception e) {
        }

    }

    @Override
    public void onAccuracyChanged(Sensor sensor, int accuracy) {

    }

    private void setupBottomNavigationView() {
        bottomNavigationView = findViewById(R.id.bottomNavigationMain);
        bottomNavigationView.setSelectedItemId(R.id.home);
//        for (int i = 0; i < bottomNavigationView.getMenu().size(); i++) {
//            bottomNavigationView.getMenu().getItem(i).setChecked(false);
//        }
////        bottomNavigationView.getMenu().getItem(2).setCheckable(true);
//        bottomNavigationView.getMenu().getItem(2).setCheckable(true);
        View view = bottomNavigationView.findViewById(R.id.action_home);
        view.performClick();

        //BottomNavigationViewHelper.disableShiftMode(bottomNavigationView);
        bottomNavigationView.setOnNavigationItemSelectedListener(new BottomNavigationView.OnNavigationItemSelectedListener() {
            @RequiresApi(api = Build.VERSION_CODES.O)
            @Override
            public boolean onNavigationItemSelected(@NonNull MenuItem menuItem) {

                switch (menuItem.getItemId()) {
                    case R.id.action_home:
                        mViewPager.setCurrentItem(1);
                        //mainFragment.init();
                        menuItem.setCheckable(true);
                        return true;
                    case R.id.action_settings:
                        mViewPager.setCurrentItem(2);
                        menuItem.setCheckable(true);
                        return true;
                    case R.id.action_userInfo:
                        mViewPager.setCurrentItem(0);
                        menuItem.setCheckable(true);
                        return true;
                    case R.id.action_second:
                        mViewPager.setCurrentItem(3);
                        menuItem.setCheckable(true);
                        return true;
                    case R.id.action_reset:
                        //LoginActivity.setMainActivity(MainActivity.this);
                        //startActivity(new Intent(MainActivity.this, LoginActivity.class));
                        Toast.makeText(MainActivity.this, "Not Implemented Yet", Toast.LENGTH_SHORT).show();
                        return false;
                }
                return false;
            }
        });
    }

    public void checkConnection() {
        if (listOfClients.isEmpty()) {
            mainFragment.connctionBad();
            return;
        }
        mainFragment.connctionGood();
    }

    public class ConnectTask extends AsyncTask<String, String, TcpClient> {

        @Override
        protected TcpClient doInBackground(String... message) {

            try {

                /**
                 Thread that create and connect new TCP clients (Creation)
                 **/
                firstTcpClient = new Thread() {
                    @Override
                    public void interrupt() {
                        try {
                            super.interrupt();
                        } catch (Exception ex) {
                            Log.e("interrupt", " " + ex.getMessage());
                        }
                        mTcpClient.stopClient();
                    }

                    @Override
                    public void run() {
                        super.run();
                        mTcpClient = new TcpClient(new TcpClient.OnMessageReceived() {

                            @Override
                            //here the messageReceived method is implemented
                            public void messageReceived(String message) {
                                //this method calls the onProgressUpdate

                                try {
                                    Gson gson = new Gson();
                                    MessageBase u = gson.fromJson(message, MessageBase.class);
                                    switch (u.getMsgCode()) {
                                        case 102:
                                            u = gson.fromJson(message, UserMessage.class);
                                            if (damage < ((UserMessage) u).getDmg() && !isFirstEntity) {
                                                gotShot();
                                            }
                                            damage = ((UserMessage) u).getDmg();
                                            metID = ((UserMessage) u).getMetId();
                                            entityName = ((UserMessage) u).getEntityName();
                                            user.setDmg(damage);
                                            user.setEntityName(entityName);
                                            user.setMetId(metID);
                                            updateFragments();
                                            userFragment.update();
                                            isFirstEntity = false;
                                            Log.d("New User Info Received", "DMG= " + user.getDmg() + ", Met ID= " + user.getMetId() + ", Entity name= " + user.getEntityName());
                                            break;
                                        default:
                                            Log.e("Error:", "No fitted MSG found");
                                    }
                                } catch (Exception e) {
                                    //Toast.makeText(MainActivity.this, "Message received from server, could not parse it.", Toast.LENGTH_SHORT).show();
                                    Log.e("First Socket", " " + e.getMessage());
                                }
                            }
                        }, 1);
                        listOfClients.add(mTcpClient);
                        mTcpClient.update();
                        mTcpClient.run();
                    }
                };
                //we create a TCPClient object

                secondTcpClient = new Thread() {
                    @Override
                    public void interrupt() {
                        try {
                            super.interrupt();
                        } catch (Exception ex) {
                            Log.e("interrupt", " " + ex.getMessage());
                        }
                        mTcpClient2.stopClient();
                    }

                    @Override
                    public void run() {
                        super.run();
                        mTcpClient2 = new TcpClient(new TcpClient.OnMessageReceived() {

                            @Override
                            //here the messageReceived method is implemented
                            public void messageReceived(String message) {
                                //this method calls the onProgressUpdate

                            }
                        }, 2);
                        listOfClients.add(mTcpClient2);
                        mTcpClient2.update();
                        mTcpClient2.run();
                    }
                };

                /**
                 Starting the proccess of the threads
                 **/
                synchronized (listOfClients) {
                    firstTcpClient.start();
                }
                Log.d("TcpClient 1", "Created Successfully");
                // System.out.println(listOfClients.get(0));
                Thread.sleep(1500);
                if (firstSocket) {
                    synchronized (listOfClients) {
                        try {
                            secondTcpClient.start();
                        } catch (Exception e) {
                            Log.d("Second Socket", " " + e.getMessage());
                        }
                    }
                    Log.d("TcpClient 2", "Created Successfully");
                    //System.out.println(listOfClients.get(1));
                }
                return null;

            } catch (Exception e) {
                Log.d("error in TcpClient init", " " + e.getMessage());
            }
            return null;
        }

        @Override
        protected void onProgressUpdate(String... values) {
            super.onProgressUpdate(values);
            //response received from server
            Log.d("test", "response " + values[0]);
            //process server response here....

        }

    }

    public void gotShot() {
        try {
            gettingShot.start();

            // Vibrate for 500 milliseconds
            Vibrator v = (Vibrator) getSystemService(Context.VIBRATOR_SERVICE);
            if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
                v.vibrate(VibrationEffect.createOneShot(500, VibrationEffect.DEFAULT_AMPLITUDE));
            } else {
                //deprecated in API 26
                v.vibrate(500);
            }

//                mainFragment.layout.setBackgroundResource(R.drawable.hit);
//                Thread.sleep(500);
//                mainFragment.layout.setBackgroundResource(R.drawable.background);
        } catch (Exception e) {
            System.out.println("Shooting throws an exception: " + e.getMessage());
        }
    }


    /***
     Functions for button:
     * Settings Button
     * LogOut Button
     * UserInfo Button
     * FireNow Button
     ***/

    public void fireNow(View view) {
        Log.d("Fire Now", "I got to main activity");
        if (mTcpClient != null) {
            FireMessage fire = new FireMessage();
            shootSound.start();
            mTcpClient.sendMessage(fire.getMsg(user.getMetId()));
        } else {
            Toast.makeText(this, "Client Not Connected yet", Toast.LENGTH_SHORT).show();
        }
    }

}
