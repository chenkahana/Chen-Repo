package com.bagirasys.sendlocationapp;

import org.json.JSONException;
import org.json.JSONObject;

public class Location {

    private static String ID=null;
    private String uniqueID;
    private double lat;
    private double lon;
    private double alt;
    private float speed;
    private double bearing;

    public float getSpeed() {
        return speed;
    }

    public void setSpeed(float speed) {
        this.speed = speed;
    }

    public double getBearing() {
        return bearing;
    }

    public void setBearing(double bearing) {
        this.bearing = bearing;
    }

    public Location() {
    }

    public static String getID(){
        return ID;
    }
    public static void setID(String id1){
        ID=id1;
    }
    public double getAlt(){
        return alt;
    }
    public void setAlt(double alt){
        this.alt=alt;
    }
    public double getLat() {
        return lat;
    }

    public void setLat(double lat) {
        this.lat = lat;
    }

    public double getLon() {
        return lon;
    }

    public void setLon(double lon) {
        this.lon = lon;
    }

    public String getAsJSON()  {
        JSONObject json= new JSONObject();

        try{
            json.put("Code",100+"");
            json.put("name",getID());
            json.put("ID", getUniqueID());
            json.put("Lat",getLat()+"");
            json.put("Long",getLon()+"");
            json.put("Alt",getAlt()+"");
            json.put("Speed", getSpeed()+"");
            json.put("Bearing",getBearing()+"");

        }catch(JSONException e) {
            return e.getMessage();
        }
        return json.toString();
    }

    public String getUniqueID() {
        return uniqueID;
    }

    public void setUniqueID(String uniqueID) {
        this.uniqueID = uniqueID;
    }
}
