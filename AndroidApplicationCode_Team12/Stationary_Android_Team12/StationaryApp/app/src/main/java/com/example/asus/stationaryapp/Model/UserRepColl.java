package com.example.asus.stationaryapp.Model;

import android.util.Log;

import com.example.asus.stationaryapp.Employee;
import com.example.asus.stationaryapp.JSONParser;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

public class UserRepColl extends HashMap<String, String>
{

    //static String host = "172.17.102.164";

    static String host = "172.17.32.255";

    //static String host = "172.17.103.57";

    static String baseURL;

    static {
        baseURL = String.format("http://%s/stationaryappstoreapi/api/userrepcollection", host);

    }



    public UserRepColl(String id, String collid, String empid) {
        put("URCollectionID", id);
        put("CollectionID", collid);
        put("EmployeeID", empid);

    }







    public static UserRepColl ReadUserRepColl(String departmentid) {
        try {
            JSONObject a = JSONParser.getJSONFromUrl(String.format("%s/%s",baseURL+"/departmentid", departmentid));
            //JSONObject a = JSONParser1.getJSONFromUrl(String.format(baseURL1));
            Log.e("apple","apple");
            UserRepColl e = new UserRepColl(Integer.toString(a.getInt("URCollectionID")),
                    Integer.toString(a.getInt("CollectionID")),
                    Integer.toString(a.getInt("EmployeeID")));
            return e;
        } catch (Exception e) {
            Log.e("UserRepColl", "JSONArray error");
        }
        return(null);
    }

    public static void saveRequest(UserRepColl request) {





        JSONObject jemp = new JSONObject();
        try {
            jemp.put("URCollectionID", request.get("URCollectionID"));
            jemp.put("CollectionID", request.get("CollectionID"));
            jemp.put("EmployeeID", request.get("EmployeeID"));


        } catch (Exception e) {
        }

        JSONParser.postStream(baseURL+"/update", true, jemp.toString());

    }







}
