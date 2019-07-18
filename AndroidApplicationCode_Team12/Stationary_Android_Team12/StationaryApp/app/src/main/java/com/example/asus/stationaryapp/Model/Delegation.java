package com.example.asus.stationaryapp.Model;

import android.util.Log;

import com.example.asus.stationaryapp.JSONParser;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

public class Delegation extends HashMap<String, String>
{


    //static String host = "172.17.102.164";

    //static String host = "172.17.103.57";

    static String host = "172.17.32.255";
    static String baseURL;

    static {
        baseURL = String.format("http://%s/stationaryappstoreapi/api/delegation", host);

    }



    public Delegation(String did, String eid, String startdate, String enddate) {
        put("DepartmentID", did);
        put("EmployeeID", eid);
        put("StartDate", startdate);
        put("EndDate", enddate);
    }

    public static void saveRequest(Delegation request) {

        JSONObject jemp = new JSONObject();
        try {

            jemp.put("EmployeeID", request.get("EmployeeID"));
            jemp.put("StartDate", request.get("StartDate"));
            jemp.put("EndDate", request.get("EndDate"));
            jemp.put("DepartmentID", request.get("DepartmentID"));

        } catch (Exception e) {
        }
        JSONParser.postStream(baseURL+"/add", true, jemp.toString());
    }

    public static String IsDuplicate(Delegation request){
        JSONObject jemp = new JSONObject();
        try {
            jemp.put("EmployeeID", request.get("EmployeeID"));
            jemp.put("StartDate", request.get("StartDate"));
            jemp.put("EndDate", request.get("EndDate"));
            jemp.put("DepartmentID", request.get("DepartmentID"));

        } catch (Exception e) {
        }
        String s = JSONParser.postStream(baseURL+"/check", true, jemp.toString());
        return s;
    }



}

