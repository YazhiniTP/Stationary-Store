package com.example.asus.stationaryapp.Model;

import android.content.SharedPreferences;
import android.util.Log;

import com.example.asus.stationaryapp.Employee;
import com.example.asus.stationaryapp.JSONParser;

import org.json.JSONArray;
import org.json.JSONObject;

import java.net.URLEncoder;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

public class Request extends HashMap<String, String>
{


    //static String host = "172.17.102.164";

    //static String host = "172.17.103.57";

    static String host = "172.17.32.255";
    static String baseURL;

    static {
        baseURL = String.format("http://%s/stationaryappstoreapi/api/request", host);

    }



    public Request(String id, String name, String submissiondate,String status) {
        put("RequestID", id);
        put("EmployeeName", name);
        put("SubmissionDate", submissiondate);
        put("Status", status);
    }

    public Request(String id, String name, String submissiondate,String status,String amt) {
        put("RequestID", id);
        put("EmployeeName", name);
        put("SubmissionDate", submissiondate);
        put("Status", status);
        put("Amt",amt);
    }







    public static List<Request> ReadRequest(String did) {
        List<Request> list = new ArrayList<Request>();
        JSONArray a = JSONParser.getJSONArrayFromUrl(String.format("%s/%s",baseURL+"/departmentid", did));
        try {
            for (int i =0; i<a.length(); i++) {
                JSONObject b = a.getJSONObject(i);
                list.add(new Request(b.getString("RequestID"),
                        b.getString("EmployeeName"),b.getString("SubmissionDate"), b.getString("Status"),Double.toString(b.getDouble("Amt"))));
            }
        } catch (Exception e) {
            Log.e("Request", "JSONArray error");
        }
        return(list);
    }

    public static void saveRequest(Request request) {



        JSONObject jemp = new JSONObject();
        try {
            jemp.put("RequestID", request.get("RequestID"));
            jemp.put("EmployeeName", request.get("EmployeeName"));
            jemp.put("SubmissionDate", request.get("SubmissionDate"));
            jemp.put("Status", request.get("Status"));

        } catch (Exception e) {
        }

            JSONParser.postStream(baseURL+"/apprej/update", true, jemp.toString());

    }







}
