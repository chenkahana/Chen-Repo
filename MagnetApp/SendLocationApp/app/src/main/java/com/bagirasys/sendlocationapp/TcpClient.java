package com.bagirasys.sendlocationapp;

import android.os.Build;
import android.support.annotation.RequiresApi;
import android.util.Log;
import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.io.PrintWriter;
import java.net.InetAddress;
import java.net.Socket;

@RequiresApi(api = Build.VERSION_CODES.LOLLIPOP)
public class TcpClient {

    public static String TAG = TcpClient.class.getSimpleName();
    public static String SERVER_IP = MySettings.IPAddress; //server IP address
    public static int SERVER_PORT = MySettings.port;
    public static String sSERVER_IP = MySettings.secondIPAddress; //server IP address
    public static int sSERVER_PORT = MySettings.secondPort;
    // message to send to the server
    private String mServerMessage;
    // sends message received notifications
    private OnMessageReceived mMessageListener = null;
    // while this is true, the server will continue running
    private boolean mRun = false;
    // used to send messages
    private PrintWriter mBufferOut;
    // used to read messages from the server
    public BufferedReader mBufferIn;

    /**
     * Constructor of the class. OnMessagedReceived listens for the messages received from server
     */
    public TcpClient(OnMessageReceived listener) {
        mMessageListener = listener;
    }
    public static void update(){
        SERVER_IP = MySettings.IPAddress;
        SERVER_PORT = MySettings.port;
    }

    /**
     * Sends the message entered by client to the server
     *
     * @param message text entered by client
     */
    public void sendMessage(final String message) {
        Runnable runnable = new Runnable() {
            @Override
            public void run() {
                if (mBufferOut != null) {
                    if(message!="") {
                        Log.d(TAG, "Sending: " + message);
                        mBufferOut.println(message);
                        mBufferOut.flush();
                    }
                }
            }
        };
        Thread thread = new Thread(runnable);
        thread.start();
    }

    /**
     * Close the connection and release the members
     */
    public void stopClient() {

        mRun = false;

        if (mBufferOut != null) {
            mBufferOut.flush();
            mBufferOut.close();
        }

        mMessageListener = null;
        mBufferIn = null;
        mBufferOut = null;
        mServerMessage = null;
    }

    public void run() {
        mRun = true;
        try {
            InetAddress serverAddr = InetAddress.getByName(SERVER_IP);
            Log.e("TCP Client", "C: Connecting...");
   //         if(MainActivity.isSecondSocket){
 //               InetAddress serverAddr2 = InetAddress.getByName(sSERVER_IP);
//                Socket socket2=new Socket(serverAddr2, sSERVER_PORT);
//                try {
//                    mBufferOut = new PrintWriter(socket2.getOutputStream());
//                    Log.e("TCP Client", "C: Sent.");
//                    MainActivity.connectionSucsses=socket2.isConnected();
//                    mBufferIn = new BufferedReader(new InputStreamReader(socket2.getInputStream()));
//                    int charsRead = 0; char[] buffer = new char[2024]; //choose your buffer size if you need other than 1024
//                    while (mRun) {
//                        charsRead = mBufferIn.read(buffer);
//                        mServerMessage = new String(buffer).substring(0, charsRead);
//                        if (mServerMessage != null && mMessageListener != null) {
//                            Log.e("in if---------->>", " Received : '" + mServerMessage + "'");
//                        }
//                        mServerMessage = null;
//                    }
//                    Log.e("-------------- >>", " Received : '" + mServerMessage + "'");
//                } catch (Exception e) {
//                    Log.e("TCP", "S: Error", e);
//                    MainActivity.connectionSucsses = false;
//                }
              //  }
            Socket socket = new Socket(serverAddr, SERVER_PORT);
            try {
                mBufferOut = new PrintWriter(socket.getOutputStream());
                Log.e("TCP Client", "C: Sent.");
                MainActivity.connectionSucsses=socket.isConnected();
                mBufferIn = new BufferedReader(new InputStreamReader(socket.getInputStream()));
                int charsRead = 0; char[] buffer = new char[2024]; //choose your buffer size if you need other than 1024
                while (mRun) {
                    charsRead = mBufferIn.read(buffer);
                    mServerMessage = new String(buffer).substring(0, charsRead);
                    if (mServerMessage != null && mMessageListener != null) {
                        Log.e("in if---------->>", " Received : '" + mServerMessage + "'");
                    }
                    mServerMessage = null;
                }
                Log.e("-------------- >>", " Received : '" + mServerMessage + "'");
            } catch (Exception e) {
                Log.e("TCP", "S: Error", e);
                MainActivity.connectionSucsses=false;

            } finally {
                //the socket must be closed. It is not possible to reconnect to this socket
                // after it is closed, which means a new socket instance has to be created.
                socket.close();
                MainActivity.connectionSucsses=false;
                Log.e("-------------- >>", "Close socket " );
            }

        } catch (Exception e) {
            Log.e("TCP", "C: Error", e);
        }

    }

    //Declare the interface. The method messageReceived(String message) will must be implemented in the Activity
    //class at on AsyncTask doInBackground
    public interface OnMessageReceived {
        public void messageReceived(String message);
    }

}