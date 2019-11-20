package com.bagirasys.sendlocationapp;

public class User {
    private String name;
    private String id;
    private String entityName;
    private int dmg;
    private String metId;


    public User(){
        name="not set yet";
        id="not set yet";
        entityName="not set yet";
        metId="not set yet";
        dmg=0;
    }


    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getEntityName() {
        return entityName;
    }

    public void setEntityName(String entityName) {
        this.entityName = entityName;
    }

    public int getDmg() {
        return dmg;
    }

    public void setDmg(int dmg) {
        this.dmg = dmg;
    }

    public String getMetId() {
        return metId;
    }

    public void setMetId(String metId) {
        this.metId = metId;
    }
}
