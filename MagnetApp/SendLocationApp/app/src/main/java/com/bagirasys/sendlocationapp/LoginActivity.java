package com.bagirasys.sendlocationapp;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import android.widget.EditText;
import android.widget.Toast;

public class LoginActivity extends AppCompatActivity {
    private EditText userNameEditText;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
        userNameEditText=findViewById(R.id.userNameEditText);
    }

    public void login(View view) {
        InputMethodManager imm = (InputMethodManager)getSystemService(INPUT_METHOD_SERVICE);
        imm.hideSoftInputFromWindow(getCurrentFocus().getWindowToken(), 0);
        String str=userNameEditText.getText().toString();
        if(!str.equals("")&&!str.isEmpty()){
            Location.setID(str);
            startActivity(new Intent(this, MainActivity.class));
        }else{
            Toast.makeText(this, "No Username was entered", Toast.LENGTH_SHORT).show();
        }
    }
}
