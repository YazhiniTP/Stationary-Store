package com.example.asus.stationaryapp;

import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.preference.PreferenceManager;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.GridLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.Toast;

import com.example.asus.stationaryapp.Activity.RequestActivity;
import com.example.asus.stationaryapp.Home.SettingRecyclerViewAdapter;
import com.example.asus.stationaryapp.Model.ItemObject;

import java.util.ArrayList;
import java.util.List;

public class MainActivity extends AppCompatActivity {

    SharedPreferences pref;
    GridLayoutManager lLayout;
    RecyclerView rView;
    SettingRecyclerViewAdapter srcAdapter;
    List<ItemObject> rowListItem;
    Employee e;



    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_home_page);
        lLayout = new GridLayoutManager(this, 2);
        rView = (RecyclerView)findViewById(R.id.recycler_view);
        rView.setHasFixedSize(true);
        rView.setLayoutManager(lLayout);


        String sithu="1234T2953";

       int i= sithu.indexOf("T");

       Log.e("Test Data is ",i+" ");

       String newDate=sithu.substring(0,4);

        Log.e("Test Data is ",newDate+" ");



        pref = PreferenceManager.getDefaultSharedPreferences(getApplicationContext());

        String email=pref.getString("username", "");
        pref.getString("roleid", "");
        pref.getString("departmentid", "");
        pref.getString("employeeid","");



        new AsyncTask<Void,Void,String>() {
            @Override
            protected String doInBackground(Void... params) {
                String e=Employee.getRole();

                return e;
            }
            @Override
            protected void onPostExecute(String roleid) {

               Log.e("Login Role is ",roleid);

                String roleid1=roleid.trim();


                SharedPreferences.Editor editor = pref.edit();
                editor.putString("roleid", roleid1);
                editor.commit();


                Log.e("Role ID",pref.getString("roleid", ""));


                if(roleid1.equals("Dept Head")){



                    rowListItem = getDeptHeadAllItemList();
                    srcAdapter = new SettingRecyclerViewAdapter(getApplicationContext(), rowListItem);
                    rView.setAdapter(srcAdapter);

                }
                else if(roleid1.equals("Store Clerk")){



                    rowListItem = getStoreClerkAllItemList();
                    srcAdapter = new SettingRecyclerViewAdapter(getApplicationContext(), rowListItem);
                    rView.setAdapter(srcAdapter);
                }
                else{
                    finish();
                }



            }
        }.execute();



        new AsyncTask<String,Void,Employee>() {
            @Override
            protected Employee doInBackground(String... params) {
                e=Employee.ReadEmployee(params[0]);

                return e;
            }
            @Override
            protected void onPostExecute(Employee e) {



                SharedPreferences.Editor editor = pref.edit();

                editor.putString("departmentid", e.get("DepartmentID").toString());
                editor.putString("employeeid",e.get("EmployeeID").toString());
                editor.commit();

                Log.e("Employee ID",e.get("EmployeeID").toString());
                Log.e("Department ID",e.get("DepartmentID").toString());





            }
        }.execute(email);



    }



    private List<ItemObject> getDeptHeadAllItemList() {
        ArrayList<ItemObject> allItems = new ArrayList<ItemObject>();
        allItems.add(new ItemObject("Manage Request", R.drawable.managerequest1));
        allItems.add(new ItemObject("Manage Collection", R.drawable.managecollection1));
        allItems.add(new ItemObject("Delegate Role", R.drawable.delegaterole));

        return allItems;
    }

    private List<ItemObject> getStoreClerkAllItemList() {
        ArrayList<ItemObject> allItems = new ArrayList<ItemObject>();

        allItems.add(new ItemObject("Raise Voucher", R.drawable.makevoucher1));
        allItems.add(new ItemObject("Pending Voucher", R.drawable.viewvoucher1));
        allItems.add(new ItemObject("Disbursement", R.drawable.delegation1));
        allItems.add(new ItemObject("Delivery", R.drawable.delivery1));



        return allItems;
    }


    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case R.id.log_out: {

                SharedPreferences.Editor editor = pref.edit();
                editor.putString("username", "");
                editor.putString("password", "");
                editor.commit();

                Intent intent = new Intent(MainActivity.this, LoginActivity.class);
                startActivity(intent);
                finish();
                return true;
            }

            default:
                return super.onOptionsItemSelected(item);
        }
    }


}
