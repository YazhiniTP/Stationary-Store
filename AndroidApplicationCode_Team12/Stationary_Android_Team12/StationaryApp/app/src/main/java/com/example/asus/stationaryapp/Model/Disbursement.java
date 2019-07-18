package com.example.asus.stationaryapp.Model;

import android.util.Log;

import com.example.asus.stationaryapp.JSONParser;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

public class Disbursement extends HashMap<String, String>
{


    //static String host = "172.17.102.164";

    static String host = "172.17.32.255";

    //static String host = "172.17.103.57";

    static String baseURL;
    static String baseURL1;

    static {
        baseURL = String.format("http://%s/stationaryappstoreapi/api/disbursement", host);
        baseURL1 = String.format("http://%s/stationaryappstoreapi/api/delivery", host);

    }





    public Disbursement(String id, String name, String qty) {
        put("ItemID", id);
        put("ItemDes", name);
        put("DisbursedQty", qty);

    }

    public Disbursement(String disid,  String qty) {
        put("DisbursementID", disid);
        put("DisbursedQty", qty);

    }

    public Disbursement(String disid, String depdes, String qty,String itemid, String name) {
        put("DisbursementID", disid);
        put("DepartmentDes", depdes);
        put("DisbursedQty", qty);
        put("ItemID", itemid);
        put("ItemDes", name);


    }







    public static List<Disbursement> ReadDisbursement() {
        List<Disbursement> list = new ArrayList<Disbursement>();
        JSONArray a = JSONParser.getJSONArrayFromUrl(String.format(baseURL+"/get"));
        try {
            for (int i =0; i<a.length(); i++) {
                JSONObject b = a.getJSONObject(i);
                list.add(new Disbursement(b.getString("ItemID"),
                        b.getString("ItemDes"),b.getString("DisbursedQty")));
            }
        } catch (Exception e) {
            Log.e("Request", "JSONArray error");
        }
        return(list);
    }


    public static List<Disbursement> ReadDelivery(String did, String status) {
        List<Disbursement> list = new ArrayList<Disbursement>();
        JSONArray a = JSONParser.getJSONArrayFromUrl(String.format("%s/%s/%s",baseURL1+"/departmentid", did, status));



        try {
            for (int i =0; i<a.length(); i++) {
                JSONObject b = a.getJSONObject(i);
                list.add(new Disbursement(b.getString("DisbursementID"),b.getString("DepartmentDes"),b.getString("DisbursedQty"),b.getString("ItemID"),
                        b.getString("ItemDes")));
            }
        } catch (Exception e) {
            Log.e("Request", "JSONArray error");
        }
        return(list);
    }

    public static String UpdateDisbursement(Disbursement request) {



        JSONObject jemp = new JSONObject();
        try {
            jemp.put("ItemID", request.get("ItemID"));
            jemp.put("DisbursedQty", request.get("DisbursedQty"));


        } catch (Exception e) {
        }

        String id=JSONParser.postStream(baseURL+"/update", true, jemp.toString());

        return id.toString();
    }

    public static String UpdateDeliveryDisbursement(Disbursement request) {



        JSONObject jemp = new JSONObject();
        try {
            jemp.put("DisbursementID", request.get("DisbursementID"));
            jemp.put("DisbursedQty", request.get("DisbursedQty"));


        } catch (Exception e) {
        }

        String id=JSONParser.postStream(baseURL1+"/update", true, jemp.toString());

        return id.toString();
    }


    public static void DeliverySchedlue(String employeeid) {



        JSONParser.getStream(String.format("%s/%s",baseURL+"/scheduledelivery",employeeid));


    }

    public static void ConfirmDelivery(Disbursement request) {

        JSONObject jemp = new JSONObject();
        try {
            jemp.put("EmployeeID", request.get("DisbursementID"));
            jemp.put("DepartmentID", request.get("DisbursedQty"));


        } catch (Exception e) {
        }

        JSONParser.postStream(baseURL1+"/confirm", true, jemp.toString());








    }







}
