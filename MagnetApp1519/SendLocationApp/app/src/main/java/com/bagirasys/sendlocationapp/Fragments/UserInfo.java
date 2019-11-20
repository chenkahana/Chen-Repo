package com.bagirasys.sendlocationapp.Fragments;

import android.os.Build;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.annotation.RequiresApi;
import androidx.fragment.app.Fragment;

import com.bagirasys.sendlocationapp.Activities.MainActivity;
import com.bagirasys.sendlocationapp.R;
import com.bagirasys.sendlocationapp.User;

@RequiresApi(api = Build.VERSION_CODES.LOLLIPOP)
public class UserInfo extends Fragment {

    private EditText userNameEditText;
    private EditText idEditText;
    private EditText entityNameEditText;
    private EditText dmgEditText;
    private EditText metIdEditText;
    private User user;
    private MainActivity mainActivity;
    public static UserInfo userInfoInstance;
    private View view;


    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        super.onCreateView(inflater, container, savedInstanceState);
        view = inflater.inflate(R.layout.activity_user_info, container, false);
        userNameEditText = view.findViewById(R.id.nameEditText);
        idEditText = view.findViewById(R.id.userIdEditText);
        entityNameEditText = view.findViewById(R.id.entityNameEditText);
        dmgEditText = view.findViewById(R.id.entityDamageEditText);
        metIdEditText = view.findViewById(R.id.entityMetIdEditText);
        mainActivity=MainActivity.getInstance();

        try {
            user = mainActivity.user;
            //userName
            userNameEditText.setText(user.getName());
            //unique
            idEditText.setText(user.getId());
            //entityName
            entityNameEditText.setText(user.getEntityName());
            //dmg
            dmgEditText.setText(user.getDmg()+" %");
            //metID
            metIdEditText.setText(user.getMetId()+"");
        }catch (Exception e){
            System.out.println("Exception was: "+e.getMessage());
        }finally {
            userNameEditText.setEnabled(false);
            idEditText.setEnabled(false);
            entityNameEditText.setEnabled(false);
            dmgEditText.setEnabled(false);
            metIdEditText.setEnabled(false);
        }
        userInfoInstance=this;
        return view;


    }
    public void update(){
        user = MainActivity.user;

        //
        userNameEditText = view.findViewById(R.id.nameEditText);
        idEditText = view.findViewById(R.id.userIdEditText);
        entityNameEditText = view.findViewById(R.id.entityNameEditText);
        dmgEditText = view.findViewById(R.id.entityDamageEditText);
        metIdEditText = view.findViewById(R.id.entityMetIdEditText);
        if (mainActivity == null) {
            mainActivity=MainActivity.getInstance();
        }
        mainActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                //userName
                userNameEditText.setText(user.getName());
                //unique
                idEditText.setText(user.getId());
                //entityName
                entityNameEditText.setText(user.getEntityName());
                //dmg
                dmgEditText.setText(user.getDmg()+" %");
                //metID
                metIdEditText.setText(user.getMetId()+"");
            }
        });
    }

    public static UserInfo getInstance(){
        return userInfoInstance;
    }
}


//    @Override
//    protected void onCreate(Bundle savedInstanceState) {
//        super.onCreate(savedInstanceState);
//        setContentView(R.layout.activity_user_info);
//        userNameEditText = findViewById(R.id.nameEditText);
//        idEditText = findViewById(R.id.userIdEditText);
//        entityNameEditText = findViewById(R.id.entityNameEditText);
//        dmgEditText = findViewById(R.id.entityDamageEditText);
//        metIdEditText = findViewById(R.id.entityMetIdEditText);
//
//        bottomNavigationView = findViewById(R.id.bottomNavigationMain);
//        bottomNavigationView.setOnNavigationItemSelectedListener(this);
//
//        try {
//            user = MainActivity.user;
//            //userName
//            userNameEditText.setText(user.getName());
//            //unique
//            idEditText.setText(user.getId());
//            //entityName
//            entityNameEditText.setText(user.getEntityName());
//            //dmg
//            dmgEditText.setText(user.getDmg()+"%");
//            //metID
//            metIdEditText.setText(user.getMetId());
//        }catch (Exception e){
//            System.out.println("Exception was: "+e.getMessage());
//        }finally {
//            userNameEditText.setEnabled(false);
//            idEditText.setEnabled(false);
//            entityNameEditText.setEnabled(false);
//            dmgEditText.setEnabled(false);
//            metIdEditText.setEnabled(false);
//        }
//
//
//
//    }



