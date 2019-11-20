package com.bagirasys.sendlocationapp.Messages;

import android.util.Log;

import org.json.JSONObject;

public class FireMessage extends MessageBase {

    public String getMsg(int metId){
        JSONObject json= new JSONObject();
        try{
            json.put("Code",101+"");
            json.put("MetID",metId);
        }catch(Exception e){
            Log.d("Json Error",e.getMessage());
        }
        return json.toString();
    }

}
