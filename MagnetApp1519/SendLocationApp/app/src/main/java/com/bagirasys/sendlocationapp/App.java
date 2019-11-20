package com.bagirasys.sendlocationapp;

import android.app.Application;
import android.app.Notification;
import android.app.NotificationChannel;
import android.app.NotificationManager;
import android.os.Build;

public class App extends Application {
    public static final String CHANNEL_ID="serviceChannel";

    @Override
    public void onCreate() {
        super.onCreate();
        createNotoficationChannel();
    }

    private void createNotoficationChannel() {
        if(Build.VERSION.SDK_INT>=Build.VERSION_CODES.O){
            NotificationChannel serviceChannel= new NotificationChannel(
                    CHANNEL_ID,"Magnet App", NotificationManager.IMPORTANCE_DEFAULT
            );
            NotificationManager notificationManager=getSystemService(NotificationManager.class);
            notificationManager.createNotificationChannel(serviceChannel);
        }

    }
}
