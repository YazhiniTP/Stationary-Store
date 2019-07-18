package com.example.asus.stationaryapp.Model;

import android.util.Log;

import com.example.asus.stationaryapp.JSONParser;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

public class Department extends HashMap<String, String>
{


    //static String host = "172.17.102.164";


    //static String host = "172.17.103.57";

    static String host = "172.17.32.255";
    static String baseURL;

    static {
        baseURL = String.format("http://%s/stationaryappstoreapi/api/delivery/department", host);

    }



    public Department(String id, String name,String location,String empname) {
        put("DepartmentID", id);
        put("Description", name);
        put("Location", location);
        put("Name", empname);


    }







    public static List<Department> ReadDepartment() {
        List<Department> list = new ArrayList<Department>();
        JSONArray a = JSONParser.getJSONArrayFromUrl(String.format(baseURL));
        try {
            for (int i =0; i<a.length(); i++) {
                JSONObject b = a.getJSONObject(i);
                list.add(new Department(b.getString("DepartmentID"),
                        b.getString("Description"),b.getString("Location"),
                        b.getString("Name")));
            }
        } catch (Exception e) {
            Log.e("Request", "JSONArray error");
        }
        return(list);
    }










}
