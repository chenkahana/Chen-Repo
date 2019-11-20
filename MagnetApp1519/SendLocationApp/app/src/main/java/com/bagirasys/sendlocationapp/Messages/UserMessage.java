package com.bagirasys.sendlocationapp.Messages;

import com.google.gson.annotations.SerializedName;

public class UserMessage extends MessageBase {

        @SerializedName("EntityName")
        private String entityName;
        @SerializedName("Damage")
        private int dmg;
        @SerializedName("MetID")
        private int metId;


        public UserMessage(){

            entityName="not set yet";
            metId=0;
            dmg=0;
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

        public int getMetId() {
            return metId;
        }

        public void setMetId(int metId) {
            this.metId = metId;
        }


}
