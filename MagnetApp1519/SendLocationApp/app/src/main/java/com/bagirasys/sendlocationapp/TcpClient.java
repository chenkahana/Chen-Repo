package com.bagirasys.sendlocationapp;

import android.os.Build;
import android.util.Log;

import androidx.annotation.RequiresApi;

import com.bagirasys.sendlocationapp.Fragments.MainFragment;
import com.bagirasys.sendlocationapp.Fragments.MySettings;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.InetAddress;
import java.net.Socket;

import static com.bagirasys.sendlocationapp.Activities.MainActivity.firstSocket;
import static com.bagirasys.sendlocationapp.Activities.MainActivity.mainActivity;


@RequiresApi(api = Build.VERSION_CODES.LOLLIPOP)


public class TcpClient {
    //private MainActivity mainActivity=new MainActivity();
    public static String TAG = TcpClient.class.getSimpleName();
    public static String SERVER_IP = MySettings.IPAddress; //server IP address
    public static int SERVER_PORT = MySettings.port;
    public static String TAG2 = TcpClient.class.getSimpleName();
    public static String SERVER_IP2 = MySettings.IPAddress; //server IP address
    public static int SERVER_PORT2 = MySettings.port;
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
    public static MySettings settings;
    public MainFragment mainFragment;
    private boolean condition;
    public int numOfSocket = 0;

    /**
     * Constructor of the class. OnMessagedReceived listens for the messages received from server
     */
    public TcpClient(OnMessageReceived listener, int numOfSocket) {
        mMessageListener = listener;
        this.numOfSocket = numOfSocket;
        settings = MySettings.getInstance();
        if (settings == null) {
            settings = new MySettings();
        }
        settings.initSettings();
        mainFragment = MainFragment.getInstance();
        if (mainFragment == null) {
            mainFragment = new MainFragment();
        }
    }

    public static void update() {
        //was Mysetigns.somthing
        SERVER_IP = settings.IPAddress;
        SERVER_PORT = settings.port;
        SERVER_IP2 = settings.IPAddress2;
        SERVER_PORT2 = settings.port2;
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
                    if (message != "") {
                        mBufferOut.println(message);
                        Log.d(TAG, "Sending: " + message);
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
        InetAddress serverAddr;
        Socket socket;
        try {
            switch (numOfSocket) {
                case 1:
                    serverAddr = InetAddress.getByName(SERVER_IP);
                    Log.e("TCP Client", "C: Connecting...");
                    socket = new Socket(serverAddr, SERVER_PORT);
                    try {
                        mBufferOut = new PrintWriter(socket.getOutputStream());
                        Log.e("TCP Client", "C: Sent.");
                        condition = socket.isConnected();
                        mainActivity.runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                if (condition) {
                                    mainFragment.connctionGood();
                                } else {
                                    mainFragment.connctionBad();
                                }
                            }
                        });
                        firstSocket = true;
                        mBufferIn = new BufferedReader(new InputStreamReader(socket.getInputStream()));
                        int charsRead = 0;
                        char[] buffer = new char[2024]; //choose your buffer size if you need other than 1024
                        while (mRun) {
                            charsRead = mBufferIn.read(buffer);
                            mServerMessage = new String(buffer).substring(0, charsRead);

                            if (mServerMessage != null && mMessageListener != null) {
                                try {
                                    mMessageListener.messageReceived(mServerMessage);
                                    Log.e("in if---------->>", " Received : '" + mServerMessage + "'");
                                    mServerMessage = null;

                                } catch (Exception ex) {
                                    Log.e("Parsing Error", "Couldn't Parse MSG. " + ex.getMessage());
                                }
                            }
                            mServerMessage = null;

                        }
                        Log.e("-------------- >>", " Received : '" + mServerMessage + "'");
                    } catch (Exception e) {
                        Log.e("TCP", "S: Error", e);
                        //MainActivity.changeConnection(false);
                        mainActivity.runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                mainFragment.connctionBad();
                            }
                        });
                    } finally {
                        //the socket must be closed. It is not possible to reconnect to this socket
                        // after it is closed, which means a new socket instance has to be created.
                        socket.close();

                        Log.e("-------------- >>", "Close socket ");
                        // MainActivity.changeConnection(false);
                        mainActivity.runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                mainFragment.connctionBad();
                            }
                        });
                    }
                    break;

                case 2:
                    serverAddr = InetAddress.getByName(SERVER_IP2);
                    Log.e("TCP Client2", "C: Connecting...");
                    socket = new Socket(serverAddr, SERVER_PORT2);
                    try {
                        mBufferOut = new PrintWriter(socket.getOutputStream());
                        Log.e("TCP Client2", "C: Sent.");
                        mBufferIn = new BufferedReader(new InputStreamReader(socket.getInputStream()));
                        int charsRead = 0;
                        char[] buffer = new char[2024]; //choose your buffer size if you need other than 1024
                        while (mRun) {
                            charsRead = mBufferIn.read(buffer);
                            mServerMessage = new String(buffer).substring(0, charsRead);

                            if (mServerMessage != null && mMessageListener != null) {
                                try {
                                    mMessageListener.messageReceived(mServerMessage);
                                    Log.e("in if---------->>2", " Received : '" + mServerMessage + "'");
                                    mServerMessage = null;

                                } catch (Exception ex) {
                                    Log.e("Parsing Error", "Couldn't Parse MSG. " + ex.getMessage());
                                }
                            }
                            mServerMessage = null;

                        }
                        Log.e("-------------- >>", " Received : '" + mServerMessage + "'");
                    } catch (Exception e) {
                        Log.e("TCP2", "S: Error", e);
                    } finally {
                        //the socket must be closed. It is not possible to reconnect to this socket
                        // after it is closed, which means a new socket instance has to be created.
                        socket.close();
                    }
                    break;
                default:
                    Log.d("TCP Clinet", "Wasn't defiend well");
            }

        } catch (Exception e) {
            Log.e("TCP", "C: Error: " + e.getMessage());
        }

    }

    //Declare the interface. The method messageReceived(String message) will must be implemented in the Activity
    //class at on AsyncTask doInBackground
    public interface OnMessageReceived {
        public void messageReceived(String message);
    }

}