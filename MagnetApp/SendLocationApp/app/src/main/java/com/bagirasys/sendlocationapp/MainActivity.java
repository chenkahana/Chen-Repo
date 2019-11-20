package com.bagirasys.sendlocationapp;

import android.Manifest;
import android.annotation.SuppressLint;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.graphics.Color;
import android.graphics.drawable.Drawable;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.location.LocationListener;
import android.location.LocationManager;
import android.media.MediaPlayer;
import android.os.AsyncTask;
import android.os.Build;
import android.provider.Settings;
import android.support.annotation.NonNull;
import android.support.annotation.RequiresApi;
import android.support.v4.app.ActivityCompat;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import com.google.gson.Gson;


import static com.bagirasys.sendlocationapp.MySettings.resetSettings;
import static java.lang.Math.atan2;
import static java.lang.Math.cos;
import static java.lang.Math.sin;

@RequiresApi(api = Build.VERSION_CODES.LOLLIPOP)
public class MainActivity extends AppCompatActivity implements SensorEventListener {
    private TextView androidIdTextView;
    private Location myLocation;
    private LocationManager locationmanager;
    private LocationListener locationListener;
    private Button getButton;
    private Button postButton;
    private boolean isSub = false;
    private TextView connectionTextView;
    public String androidId;
    public String androidUnique;
    private TextView androUniqueIdTextView;
    private TcpClient mTcpClient;
    private TcpClient mTcpClient2;
    public static boolean connectionSucsses = false;
    private ImageView connectionImageView;
    private int metID;
    private int damage;
    private String entityName;
    private String startLocation = "Send Location";
    private String stopLocation = "Stop Sending";
    public static User user = new User();
    private Thread t;
    public MediaPlayer gettingShot;
    public MediaPlayer shootSound;
    private Thread shot;
    private LinearLayout layout;
    private Drawable background;
    private Button fireButton;
    private SensorManager mSensorManager;
    private Sensor mAccelerometer;
    boolean isFirst=true;
    public static boolean isSecondSocket=false;
    public SendingTheard t1= new SendingTheard(){

        @Override
        public void interrupt() {
            running=false;
        }

        @Override
        public void run() {
            try{
                while(true) {
                    if (running&&mTcpClient!=null) {
                        synchronized (myLocation) {
                            String json = myLocation.getAsJSON();
                            mTcpClient.sendMessage(json);
                        }
                        Thread.sleep(MySettings.interval);
                    }
                }
            }catch(Exception e){
                System.out.println(e.getMessage());
            }
        }

    };

    @SuppressLint("NewApi")
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        mSensorManager = (SensorManager)getSystemService(SENSOR_SERVICE);
        mAccelerometer = mSensorManager.getDefaultSensor(Sensor.TYPE_ACCELEROMETER);

        if (Location.getID() == null) {
            startActivity(new Intent(this, LoginActivity.class));
        } else {
            shootSound=MediaPlayer.create(this, R.raw.gungunshot01);
            gettingShot = MediaPlayer.create(this, R.raw.gettingshot);
            connectionImageView = findViewById(R.id.connectionImageView);
            setContentView(R.layout.activity_main);
            myLocation = new Location();
            connectionTextView = findViewById(R.id.connectionTextView);
            androidIdTextView = findViewById(R.id.androidIdTextView);
            postButton = findViewById(R.id.postLocationButton);
            postButton.setEnabled(false);
            getButton = findViewById(R.id.getLocationButton);
            getButton.setEnabled(false);
            androidId = Location.getID();
            androidUnique = Settings.Secure.getString(getContentResolver(), Settings.Secure.ANDROID_ID);
            user.setName(androidId);
            user.setId(androidUnique);
            myLocation.setUniqueID(androidUnique);
            androUniqueIdTextView = findViewById(R.id.androUniqueIdTextView);
            androUniqueIdTextView.setText(androUniqueIdTextView.getText() + androidUnique);
            androidIdTextView.setText(androidIdTextView.getText() + androidId);
            locationmanager = (LocationManager) getSystemService(LOCATION_SERVICE);
            layout = findViewById(R.id.mainLayout);
            background = getDrawable(R.drawable.background);
            fireButton= findViewById(R.id.fireButton);
            fireButton.setEnabled(false);
            locationListener = new LocationListener() {
                @Override
                public void onLocationChanged(android.location.Location location) {
                   // if (user.getDmg() < 100) {
                        checkConnection("", true);
                        synchronized (myLocation) {
                            myLocation.setLat(location.getLatitude());
                            myLocation.setLon(location.getLongitude());
                            myLocation.setAlt(location.getAltitude());
                            myLocation.setSpeed(location.getSpeed());
//                            if(isFirst){
//                                myLocation.setBearing(location.getBearing());
//                                isFirst=false;
//                            }else if(!(myLocation.getBearing()+50<=location.getBearing()||myLocation.getBearing()-50<=location.getBearing())){
//                                myLocation.setBearing(location.getBearing());
//                            }
//                            if (isSub && mTcpClient != null) {
//                                sendLocation();
//                            }
                        }
                    //}
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
                getButton.setEnabled(true);
            }
            try {

                //new ConnectTask().execute("");
//                if (postButton.isEnabled()) {
//                    checkConnection("", true);
//                } else {
//                    checkConnection("Server Detected", true);
//                }
            } catch (Exception e) {
            }
        }
    }

    @Override
    protected void onStop() {
    super.onStop();
    if(mTcpClient!=null)
        mTcpClient.stopClient();
    }

    @Override
    protected void onResume() {
        super.onResume();
        if(mSensorManager!=null)
        mSensorManager.registerListener(this, mAccelerometer, SensorManager.SENSOR_DELAY_NORMAL);
        else{
            mSensorManager = (SensorManager)getSystemService(SENSOR_SERVICE);
            mAccelerometer = mSensorManager.getDefaultSensor(Sensor.TYPE_ACCELEROMETER);
            mSensorManager.registerListener(this, mAccelerometer, SensorManager.SENSOR_DELAY_NORMAL);
        }
    }

    private float[] mMatrixR = new float[9];
    private float[] mMatrixValues = new float[3];
    private double smoothingFactor=0.9;
    private double lastSin;
    private double lastCos;

    @Override
    public void onSensorChanged(SensorEvent event) {
        try {
//        switch (event.sensor.getType()) {
//            case Sensor.TYPE_ROTATION_VECTOR:
            Thread.sleep(100);
            // Get rotation matrix
            SensorManager.getRotationMatrixFromVector(mMatrixR, event.values);

            SensorManager.getOrientation(mMatrixR, mMatrixValues);

            double bearing=Math.toDegrees(mMatrixValues[0]);

            //bearing=atan2(sin(bearing),cos(bearing));

            // Use this value in degrees
            synchronized (myLocation) {
//                if(isFirst){
//                    myLocation.setBearing(bearing);
//                    isFirst=false;
//                }
//                else if(!(myLocation.getBearing()+100<=bearing||myLocation.getBearing()-100<=bearing)){
             //   lastSin=smoothingFactor*(Math.PI*(myLocation.getBearing())/180)+(1-smoothingFactor)*sin((Math.PI*bearing/180));
            //    lastCos=smoothingFactor*(Math.PI*(myLocation.getBearing())/180)+(1-smoothingFactor)*cos((Math.PI*bearing/180));


                    myLocation.setBearing(bearing);
                //}
            }
        }catch(Exception e){
            //System.out.println("Get Sensor!"+e.getMessage());
        }

        //}
    }

    private double getAngle(){
        return atan2(lastSin,lastCos);
    }


    @Override
    public void onAccuracyChanged(Sensor sensor, int accuracy) {

    }

    public void checkConnection(final String text, final boolean condition) {
        try {
            Thread t = new Thread("CheckingConnectionThread") {
                public void run() {
                    try {
                        while (true) {
                            postButton.setEnabled(connectionSucsses);
                            fireButton.setEnabled(connectionSucsses);
                            if (text.equals("") || text.isEmpty()) {
                                if (connectionSucsses) {
                                    if (connectionImageView == null)
                                        connectionImageView = findViewById(R.id.connectionImageView);
                                    connectionImageView.setImageResource(R.drawable.ic_connected);
                                    connectionTextView.setText("Connected");
                                    connectionTextView.setTextColor(Color.GREEN);
                                    return;
                                } else {
                                    if (connectionImageView == null)
                                        connectionImageView = findViewById(R.id.connectionImageView);
                                    connectionImageView.setImageResource(R.drawable.ic_diconnected);
                                    connectionTextView.setText("Not Connected");
                                    connectionTextView.setTextColor(Color.RED);
                                }
                            } else {
                                if (connectionSucsses == condition) {
                                    if (connectionImageView == null)
                                        connectionImageView = findViewById(R.id.connectionImageView);
                                    connectionTextView.setText(text);
                                    connectionTextView.setTextColor(condition ? Color.GREEN : Color.RED);
                                    connectionImageView.setImageResource(condition ? R.drawable.ic_connected : R.drawable.ic_diconnected);
                                    return;
                                }
                            }
                        }
                    } catch (Exception e) {
                        System.out.println(e.getMessage());
                    }
                }
            };
            t.start();
        } catch (Exception e) {
        }
    }

    @Override
    public void onRequestPermissionsResult(int requestCode, @NonNull String[] permissions, @NonNull int[] grantResults) {
        switch (requestCode) {
            case 10:
                if (grantResults.length > 0 && grantResults[0] == PackageManager.PERMISSION_GRANTED) {
                    getButton.setEnabled(true);
                }
        }
    }

    public void sendLocation() {
        try {
            checkConnection("", true);
            if (connectionSucsses) {
                synchronized (myLocation) {
                    String json = myLocation.getAsJSON();

                //sends the message to the server
                if (mTcpClient != null) {
                    mTcpClient.sendMessage(json);
                }
                if(mTcpClient2!=null){
                    mTcpClient2.sendMessage(json);
                }
                }
            } else {
                Toast.makeText(this, "Connection not successful. try again.", Toast.LENGTH_SHORT).show();
            }
        } catch (Exception e) {
            Toast.makeText(this, "The Problem was:" + e.getMessage(), Toast.LENGTH_SHORT).show();
        }
    }

    public boolean checkIfSending() {
        if (postButton.getText().equals(startLocation)) {
            return false;
        } else if (postButton.getText().equals(stopLocation)) {
            return true;
        }
        return false;
    }

    @SuppressLint("MissingPermission")
    public void postLocation(View view) {

        if (mTcpClient != null) {
            if (!(checkIfSending())) {
                isSub = true;
                postButton.setText(stopLocation);
                sendLocation();
            } else {
                postButton.setText(startLocation);
                isSub = false;
                try {
                    t1.sleep(100);
                    t1.interrupt();
                } catch (Exception e) {
                    System.out.println(e.getMessage());                }
            }
            if(isSub){
                if(t1.running)
                t1.start();
                else{
                    t1.running=true;
                }
            }
        }




        /*if Works delete this*/


//        try {
//            if(mTcpClient!=null&postButton.getText().equals(stopLocation)){
//
//            }
//            checkConnection("", true);
//            if (connectionSucsses) {
//                String json = myLocation.getAsJSON();
//                //sends the message to the server
//                if (mTcpClient != null) {
//                    mTcpClient.sendMessage(json);
//                    postButton.setText(stopLocation);
//                }
//            } else {
//                Toast.makeText(this, "Connection not successful. try again.", Toast.LENGTH_SHORT).show();
//            }
//        } catch (Exception e) {
//            Toast.makeText(this, "The Problem was:" + e.getMessage(), Toast.LENGTH_SHORT).show();
//        }
    }


    @SuppressLint("MissingPermission")
    public void getLocation(View view) {
        try {
            new ConnectTask().execute("");
            Thread.sleep(250);
            if (mTcpClient != null) {
                mTcpClient.update();
                locationmanager.requestLocationUpdates(LocationManager.GPS_PROVIDER, MySettings.interval, MySettings.distance, locationListener);
                if(mTcpClient2!=null){
                    mTcpClient2.update();
                }
                //connectionSucsses = true;
                postButton.setEnabled(connectionSucsses);
                fireButton.setEnabled(connectionSucsses);
            }
            Thread.sleep(250);
            checkConnection("", true);
        } catch (Exception e) {
            e.printStackTrace();
        }
        //Toast.makeText(this, "Location Service Has Been Activated", Toast.LENGTH_SHORT).show();


    }

    public void openSettings(View view) {
        startActivity(new Intent(this, MySettings.class));
    }

    public void resetSystem(View view) {
        if (mTcpClient != null) {
            mTcpClient.stopClient();
        }
        if (mTcpClient2 != null) {
            mTcpClient2.stopClient();
        }
        isFirst=true;
        isSub = false;
        connectionSucsses = false;
        postButton.setEnabled(false);
        resetSettings();
        startActivity(new Intent(this, LoginActivity.class));
        t.interrupt();
    }

    public void openUserInfo(View view) {
        startActivity(new Intent(this, UserInfo.class));

    }



    public void fireNow(View view) {
        if(mTcpClient!=null){
            shootSound.start();
            mTcpClient.sendMessage("firenow");
            if(mTcpClient2!=null){
                mTcpClient2.sendMessage("firenow");
            }
        }
        else{
            Toast.makeText(this, "Client Not Connected yet", Toast.LENGTH_SHORT).show();
        }
    }

    @Override
    public void onPointerCaptureChanged(boolean hasCapture) {

    }


    public class ConnectTask extends AsyncTask<String, String, TcpClient> {

        @Override
        protected TcpClient doInBackground(String... message) {


            //we create a TCPClient object
            if(isSecondSocket) {
                mTcpClient2 = new TcpClient(new TcpClient.OnMessageReceived() {

                    @Override
                    //here the messageReceived method is implemented
                    public void messageReceived(String message) {
                        //this method calls the onProgressUpdate
                        publishProgress(message);
                        try {
                            Gson gson = new Gson();
                            metID = Integer.parseInt(gson.fromJson("\"metID\"", String.class));
                            damage = Integer.parseInt(gson.fromJson("\"damage\"", String.class));
                            if (user.getDmg() < damage) {
                                gotShot();
                            }
                            entityName = gson.fromJson("\"entityName\"", String.class);
                            user.setDmg(damage);
                            user.setEntityName(entityName);
                            user.setMetId(metID + "");

                        } catch (Exception e) {
                            Toast.makeText(MainActivity.this, "Message received from server, could not parse it.", Toast.LENGTH_SHORT).show();
                            System.out.println(e.getMessage());
                        }
                    }
                });
            }
            mTcpClient = new TcpClient(new TcpClient.OnMessageReceived() {

                @Override
                //here the messageReceived method is implemented
                public void messageReceived(String message) {
                    //this method calls the onProgressUpdate
                    publishProgress(message);
                    try {
                        Gson gson = new Gson();
                        metID = Integer.parseInt(gson.fromJson("\"metID\"", String.class));
                        damage = Integer.parseInt(gson.fromJson("\"damage\"", String.class));
                        if (user.getDmg() < damage) {
                            gotShot();
                        }
                        entityName = gson.fromJson("\"entityName\"", String.class);
                        user.setDmg(damage);
                        user.setEntityName(entityName);
                        user.setMetId(metID + "");

                    } catch (Exception e) {
                        Toast.makeText(MainActivity.this, "Message received from server, could not parse it.", Toast.LENGTH_SHORT).show();
                        System.out.println(e.getMessage());
                    }
                }
            });
            //connectionSucsses = true;
            mTcpClient.run();
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

    private void gotShot() {
        shot = new Thread("Shoot") {
            @Override
            public void run() {
                try {
                    gettingShot.start();
                    Drawable hit = getDrawable(R.drawable.hit);
                    layout.setBackground(hit);
                    Thread.sleep(100);
                    layout.setBackground(background);
                } catch (Exception e) {
                    System.out.println("The Thread itself throws an exception: " + e.getMessage());
                }
            }
        };

        shot.start();

    }
}

