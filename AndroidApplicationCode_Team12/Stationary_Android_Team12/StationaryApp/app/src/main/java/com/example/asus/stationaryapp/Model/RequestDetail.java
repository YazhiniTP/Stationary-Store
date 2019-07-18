package com.example.asus.stationaryapp.Model;

import android.util.Log;

import com.example.asus.stationaryapp.JSONParser;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

public class RequestDetail extends HashMap<String, String>
{
    //static String host = "172.17.102.164";

    static String host = "172.17.32.255";

   // static String host = "172.17.103.57";

    static String baseURL;

    static {
        baseURL = String.format("http://%s/stationaryappstoreapi/api/requestdetail", host);



    }



    public RequestDetail(String name, String qty, String price,String totalprice) {
        put("ProductName", name);
        put("ProductQty", qty);
        put("ProductPrice", price);
        put("ProductTotalPrice", totalprice);
    }







    public static List<RequestDetail> ReadRequest(String did) {
        List<RequestDetail> list = new ArrayList<RequestDetail>();
        JSONArray a = JSONParser.getJSONArrayFromUrl(String.format("%s/%s",baseURL+"/requestid", did));
        try {
            for (int i =0; i<a.length(); i++) {
                JSONObject b = a.getJSONObject(i);
                list.add(new RequestDetail(b.getString("RequestProductDescription"),
                        Integer.toString(b.getInt("Qty")),Double.toString(b.getDouble("UnitCost")),
                        Double.toString(b.getDouble("TotalCost"))));
            }
        } catch (Exception e) {
            Log.e("RequestDetail", "JSONArray error");
        }
        return(list);
    }







}
