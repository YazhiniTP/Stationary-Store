package com.example.asus.stationaryapp.Model;

import android.util.Log;

import com.example.asus.stationaryapp.JSONParser;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

public class Voucher extends HashMap<String, String>
{


    //static String host = "172.17.102.164";

    static String host = "172.17.32.255";

   // static String host = "172.17.103.57";

    static String baseURL;

    static {
        baseURL = String.format("http://%s/stationaryappstoreapi/api/voucher", host);

    }



    public Voucher(String eid) {

        put("EmployeeID", eid);

    }

    public Voucher(String vid,String eid,String subdate,String check) {
        put("VoucherID",vid);
        put("EmployeeID", eid);
        put("SubmissionDate", subdate);
        put("check", check);


    }


   /* public static List<Voucher> getVoucherList(){
        List<Voucher> v=new ArrayList<Voucher>();

        v.add(new Voucher("1001","2001","26-1-1996","0"));
        v.add(new Voucher("1002","2001","27-1-1996","0"));
        v.add(new Voucher("1003","2001","28-1-1996","0"));
        v.add(new Voucher("1004","2001","29-1-1996","0"));


        return v;




    }*/

    public static List<Voucher> getVoucherList(String did) {
        List<Voucher> list = new ArrayList<Voucher>();
        JSONArray a = JSONParser.getJSONArrayFromUrl(String.format("%s/%s",baseURL+"/employeeid", did));
        try {
            for (int i =0; i<a.length(); i++) {
                JSONObject b = a.getJSONObject(i);
                list.add(new Voucher(b.getString("VoucherID"),
                        b.getString("Name"),b.getString("SubmissionDate"),"0"));
            }
        } catch (Exception e) {
            Log.e("Request", "JSONArray error");
        }
        return(list);
    }

    public static void deleteVoucher(List<Voucher> request) {


        for(int i=0;i<request.size();i++) {


            JSONObject jemp = new JSONObject();
            try {

                jemp.put("VoucherID", request.get(i).get("VoucherID"));



            } catch (Exception e) {
            }

            JSONParser.postStream(baseURL + "/delete", true, jemp.toString());

            Log.e("hello"," si thu "+i);



        }





    }











    public static String saveRequest(Voucher request) {


        JSONObject jemp = new JSONObject();
        try {

            jemp.put("EmployeeID", request.get("EmployeeID"));


        } catch (Exception e) {
        }

        String id=JSONParser.postStream(baseURL+"/add", true, jemp.toString());

        return id;

    }







}
