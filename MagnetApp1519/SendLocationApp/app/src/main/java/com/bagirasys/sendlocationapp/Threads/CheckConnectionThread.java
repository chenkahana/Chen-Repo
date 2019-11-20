package com.bagirasys.sendlocationapp.Threads;

public class CheckConnectionThread extends Thread {

    private String text;
    private boolean condition;

    public String getText() {
        return text;
    }

    public void setText(String text) {
        this.text = text;
    }

    public boolean isCondition() {
        return condition;
    }

    public void setCondition(boolean condition) {
        this.condition = condition;
    }

    public CheckConnectionThread(String text, boolean condition){
        setCondition(condition);
        setText(text);
    }
    public CheckConnectionThread(){

    }





}
