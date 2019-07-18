package com.example.asus.stationaryapp.Model;

import android.util.Log;

import com.example.asus.stationaryapp.JSONParser;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

public class Catalogue extends HashMap<String, String>
{


    //static String host = "172.17.103.57";

    static String host = "172.17.32.255";
    static String baseURL;

    static {
        baseURL = String.format("http://%s/stationaryappstoreapi/api/catalogue", host);

    }



    public Catalogue(String itemid, String catid, String des,String unitcost, String acqty, String adjqty,String adjuscost,String status) {
        put("ItemID", itemid);
        put("CatID", catid);
        put("Description", des);
        put("UnitCost",unitcost);
        put("ActualQty", acqty);
        put("AdjustedQty", adjqty);
        put("AdjustedCost", adjuscost);
        put("Status", status);
    }




    public static List<Catalogue> ReadRequest1() {
        List<Catalogue> list = new ArrayList<Catalogue>();


            JSONArray a = JSONParser.getJSONArrayFromUrl(String.format(baseURL));
            try {
                for (int i =0; i<a.length(); i++) {
                    JSONObject b = a.getJSONObject(i);
                    list.add(new Catalogue(b.getString("ItemID"),
                            Integer.toString(b.getInt("CatID")),b.getString("Description"), Double.toString(b.getDouble("UnitCost"))
                    ,Integer.toString(b.getInt("ActualQty")),"0","0",""));
                }
            } catch (Exception e) {
                Log.e("Catalogue", "JSONArray error");
            }






        return(list);
    }







    public static List<Request> ReadRequest(String did) {
        List<Request> list = new ArrayList<Request>();
        JSONArray a = JSONParser.getJSONArrayFromUrl(String.format("%s/%s",baseURL+"/departmentid", did));
        try {
            for (int i =0; i<a.length(); i++) {
                JSONObject b = a.getJSONObject(i);
                list.add(new Request(b.getString("RequestID"),
                        b.getString("EmployeeName"),b.getString("SubmissionDate"), b.getString("Status")));
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

