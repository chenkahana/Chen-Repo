package com.bagirasys.sendlocationapp.Activities;

import android.content.Intent;
import android.os.Build;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import android.widget.Toast;

import androidx.annotation.RequiresApi;
import androidx.appcompat.app.AppCompatActivity;

import com.bagirasys.sendlocationapp.Location;
import com.bagirasys.sendlocationapp.R;
import com.google.android.material.textfield.TextInputEditText;

@RequiresApi(api = Build.VERSION_CODES.O)

public class LoginActivity extends AppCompatActivity {
    private TextInputEditText userNameEditText;
    public static MainActivity mainActivity;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
        userNameEditText=findViewById(R.id.userNameEditText);
        userNameEditText.setFocusedByDefault(true);
    }

    public static void setMainActivity(MainActivity mainActivity1){
        mainActivity=mainActivity1;
    }
    @Override
    protected void onResume() {
        super.onResume();
        userNameEditText=findViewById(R.id.userNameEditText);
        userNameEditText.setFocusedByDefault(true);
    }

    public void login(View view) {
        try {
            InputMethodManager imm = (InputMethodManager) getSystemService(INPUT_METHOD_SERVICE);
            imm.hideSoftInputFromWindow(new View(this).getWindowToken(), 0);


            if (userNameEditText == null) {
                userNameEditText = findViewById(R.id.userNameEditText);
            }
            String str = userNameEditText.getText().toString();
            if (!(str.length()<1)) {
                Location.setID(str);
                if(mainActivity!=null){
                    startActivity(new Intent(this, mainActivity.getClass()));
                }
                startActivity(new Intent(this, MainActivity.class));
            } else {
                 Toast.makeText(this, "No Username was entered", Toast.LENGTH_SHORT).show();
            }
        }catch(Exception ex){
            Log.e("Throws this error: ",ex.getMessage());
            Toast.makeText(this, "Problem With Logging in, enter a 3 characters long username", Toast.LENGTH_SHORT).show();
        }
    }
}
