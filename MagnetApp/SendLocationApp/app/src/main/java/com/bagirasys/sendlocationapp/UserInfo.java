package com.bagirasys.sendlocationapp;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.TextView;

public class UserInfo extends AppCompatActivity {

    private EditText userNameEditText;
    private EditText idEditText;
    private EditText entityNameEditText;
    private EditText dmgEditText;
    private EditText metIdEditText;
    private User user;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_user_info);
        userNameEditText = findViewById(R.id.nameEditText);
        idEditText = findViewById(R.id.userIdEditText);
        entityNameEditText = findViewById(R.id.entityNameEditText);
        dmgEditText = findViewById(R.id.entityDamageEditText);
        metIdEditText = findViewById(R.id.entityMetIdEditText);



        try {
            user = MainActivity.user;
            //userName
            userNameEditText.setText(user.getName());
            //unique
            idEditText.setText(user.getId());
            //entityName
            entityNameEditText.setText(user.getEntityName());
            //dmg
            dmgEditText.setText(user.getDmg()+"%");
            //metID
            metIdEditText.setText(user.getMetId());
        }catch (Exception e){
            System.out.println("Exception was: "+e.getMessage());
        }finally {
            userNameEditText.setEnabled(false);
            idEditText.setEnabled(false);
            entityNameEditText.setEnabled(false);
            dmgEditText.setEnabled(false);
            metIdEditText.setEnabled(false);
        }



    }

    public void returnToMain(View view) {
        startActivity(new Intent(this, MainActivity.class));

    }
}
