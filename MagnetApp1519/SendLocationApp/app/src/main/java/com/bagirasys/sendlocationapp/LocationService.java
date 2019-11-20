package com.bagirasys.sendlocationapp;

import android.Manifest;
import android.app.Notification;
import android.app.PendingIntent;
import android.app.Service;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Build;
import android.os.Bundle;
import android.os.IBinder;
import android.view.MotionEvent;
import android.view.View;
import android.widget.Toast;

import androidx.annotation.RequiresApi;
import androidx.core.app.ActivityCompat;
import androidx.core.app.NotificationCompat;
import androidx.viewpager.widget.ViewPager;

import com.bagirasys.sendlocationapp.Activities.MainActivity;
import com.bagirasys.sendlocationapp.Fragments.MainFragment;
import com.bagirasys.sendlocationapp.Fragments.MySettings;
import com.bagirasys.sendlocationapp.Fragments.SecondSocket;
import com.bagirasys.sendlocationapp.Fragments.UserInfo;

import static com.bagirasys.sendlocationapp.App.CHANNEL_ID;

@RequiresApi(api = Build.VERSION_CODES.M)
public class LocationService extends Service {

    private MainActivity mainActivity;
    private LocationManager locationmanager;
    private LocationListener locationListener;
    private SectionsPagerAdapter mSectionsPagerAdapter;
    private ViewPager mViewPager;
    private MainFragment mainFragment;
    private MySettings settingsFragment;
    private SecondSocket secondFragment;
    private UserInfo userFragment;

    public void updateFragments() {
        mainFragment = MainFragment.getInstance();
        if (mainFragment == null) {
            mainFragment = new MainFragment();
        }
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
    public IBinder onBind(Intent intent) {
        return null;
    }

    @Override
    public void onCreate() {
        super.onCreate();

        locationmanager = (LocationManager) getSystemService(LOCATION_SERVICE);
        locationListener = new LocationListener() {
            @Override
            public void onLocationChanged(android.location.Location location) {
                if (mainActivity.user.getDmg() < 100) {
                    synchronized (mainActivity.flagLocation) {
                        mainActivity.myLocation.setLat(location.getLatitude());
                        mainActivity.myLocation.setLon(location.getLongitude());
                        mainActivity.myLocation.setAlt(location.getAltitude());
                        mainActivity.myLocation.setSpeed(location.getSpeed());
                        if (mainActivity.isSub && !(mainActivity.listOfClients.isEmpty())) {
                            mainActivity.sendingTheard.run();
                            //Toast.makeText(LocationService.this, "sent Coordinates: "+mainActivity.myLocation.getLat()+", "+mainActivity.myLocation.getLon(), Toast.LENGTH_SHORT).show();
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

            }

        };

    }
    private void setupViewPager(ViewPager viewPager) {
        SectionsPagerAdapter adapter = new SectionsPagerAdapter(mainActivity.getSupportFragmentManager());
        viewPager.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                return true;
            }
        });
        adapter.addFragment(userFragment);
        adapter.addFragment(mainFragment);
        adapter.addFragment(settingsFragment);
        adapter.addFragment(secondFragment);
        viewPager.setAdapter(adapter);
    }


    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {
        mainActivity = MainActivity.getInstance();
        Intent notificationIntent= new Intent(this, MainActivity.class);
        notificationIntent.putExtra("view",intent.getIntExtra("view",1));
        PendingIntent pendingIntent= PendingIntent.getActivity(this,0, notificationIntent,0);
        if(!(mainActivity.isSub)){
            Notification notification=new NotificationCompat.Builder(this,CHANNEL_ID )
                    .setContentTitle("Magnet App")
                    .setContentText("Welcome to the Magnet app by Bagira Systems!")
                    .setSmallIcon(R.drawable.ic_notification)
                    .setContentIntent(pendingIntent)
                    .build();
            startForeground(1,notification);
        }else{
        Notification notification=new NotificationCompat.Builder(this,CHANNEL_ID )
                .setContentTitle("Magnet App")
                .setContentText("current location: "+mainActivity.myLocation.getLat()+", "+mainActivity.myLocation.getLon())
                .setSmallIcon(R.drawable.ic_notification)
                .setContentIntent(pendingIntent)
                .build();
        startForeground(1,notification);
        }


        if (ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
            mainActivity.requestPermissions(new String[]{
                    Manifest.permission.ACCESS_COARSE_LOCATION, Manifest.permission.ACCESS_FINE_LOCATION,
                    Manifest.permission.INTERNET
            }, 10);
        }


        if(!(mainActivity.listOfClients.isEmpty())){
            locationmanager.requestLocationUpdates("gps", 0, MySettings.distance, locationListener);
            //Toast.makeText(this, "Service Started with clients", Toast.LENGTH_SHORT).show();
        }
        return START_REDELIVER_INTENT;
    }

    @Override
    public void onDestroy() {
        Toast.makeText(this, "Magnet App Stopped", Toast.LENGTH_SHORT).show();
    }

}
