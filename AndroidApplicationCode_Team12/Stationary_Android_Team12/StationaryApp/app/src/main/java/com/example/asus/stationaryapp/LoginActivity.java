package com.example.asus.stationaryapp;

import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.preference.PreferenceManager;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.example.asus.stationaryapp.Activity.DisbursementDetailActivity;

public class LoginActivity extends AppCompatActivity {
    SharedPreferences pref;
    Button btnLogin;
    EditText t1, t2;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login2);

        pref = PreferenceManager.getDefaultSharedPreferences(getApplicationContext());

        t1 = (EditText) findViewById(R.id.username);
        t1.setText(pref.getString("username", ""));
        t2 = (EditText) findViewById(R.id.password);
        t2.setText(pref.getString("password", ""));

        btnLogin=(Button)findViewById(R.id.button_login);

        btnLogin.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                SharedPreferences.Editor editor = pref.edit();
                editor.putString("username", t1.getText().toString());
                editor.putString("password", t2.getText().toString());
                editor.commit();



             new AsyncTask<Void,Void,Void>() {
                    @Override
                    protected Void doInBackground(Void... v) {
                        Employee.Login(PreferenceManager.
                                getDefaultSharedPreferences(getApplicationContext()));

                        return null;
                    }
                    @Override
                    protected void onPostExecute(Void aVoid) {
                        if(JSONParser.access_token.isEmpty()){

                            runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    //UI related code

                                    android.app.AlertDialog.Builder builder = new android.app.AlertDialog.Builder(LoginActivity.this);
                                    builder.setMessage("Login is not successful !!! \n\n" +
                                            "Please enter your username and password again ")
                                            .setCancelable(false)
                                            .setPositiveButton("OK", new DialogInterface.OnClickListener() {
                                                public void onClick(DialogInterface dialog, int id) {
                                                    //do things


                                                }
                                            });
                                    android.app.AlertDialog alert = builder.create();
                                    alert.show();

                                }
                            });


                        }
                        else{

                            Intent intent = new Intent(LoginActivity.this, MainActivity.class);
                            startActivity(intent);
                            finish();

                        }

                    }
                }.execute();




            }
        });


    }


}
