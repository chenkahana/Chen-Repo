package com.bagirasys.sendlocationapp.Messages;

import com.google.gson.annotations.SerializedName;

public class MessageBase {

    @SerializedName("Code")
    private int msgCode=0;

    public int getMsgCode() {
        return msgCode;
    }
}
