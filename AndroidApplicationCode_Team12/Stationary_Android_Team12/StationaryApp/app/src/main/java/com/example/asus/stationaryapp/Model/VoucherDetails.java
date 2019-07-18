package com.example.asus.stationaryapp.Model;

import android.util.Log;

import com.example.asus.stationaryapp.JSONParser;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

public class VoucherDetails extends HashMap<String, String>
{


    //static String host = "172.17.102.164";

    static String host = "172.17.32.255";

    //static String host = "172.17.103.57";

    static String baseURL;

    static {
        baseURL = String.format("http://%s/stationaryappstoreapi/api/voucherdetail", host);

    }




    public VoucherDetails(String id,String adjusqty,String adjusamt,String voucherid,String empid,String remarks) {

        put("ItemID", id);
        put("AdjustedQty", adjusqty);
        put("AdjustedAmt", adjusamt);
        put("VoucherID", voucherid);
        put("EmployeeID", empid);
        put("Remarks", remarks);


    }

    public VoucherDetails(String des,String adjusqty,String adjusamt,String price) {

        put("ItemDescription", des);
        put("AdjustedQty", adjusqty);
        put("AdjustedAmt", adjusamt);
        put("ProductPrice",price);



    }




    public static List<VoucherDetails> getVoucherDetails(String did) {
        List<VoucherDetails> list = new ArrayList<VoucherDetails>();
        JSONArray a = JSONParser.getJSONArrayFromUrl(String.format("%s/%s",baseURL+"/voucherid", did));
        try {
            for (int i =0; i<a.length(); i++) {
                JSONObject b = a.getJSONObject(i);
                list.add(new VoucherDetails(b.getString("Item_Description"),
                        b.getString("AdjustedQty"),b.getString("AdjustedAmt"),b.getString("UnitCost")));
            }
        } catch (Exception e) {
            Log.e("Request", "JSONArray error");
        }
        return(list);
    }











    public static void saveRequest(List<VoucherDetails> request) {


        for(int i=0;i<request.size();i++) {


            JSONObject jemp = new JSONObject();
            try {

                jemp.put("ItemID", request.get(i).get("ItemID"));
                jemp.put("AdjustedQty", request.get(i).get("AdjustedQty"));
                jemp.put("AdjustedAmt", request.get(i).get("AdjustedAmt"));
                jemp.put("VoucherID", request.get(i).get("VoucherID"));
                jemp.put("EmployeeID", request.get(i).get("EmployeeID"));
                jemp.put("Remarks", request.get(i).get("Remarks"));


            } catch (Exception e) {
            }

            JSONParser.postStream(baseURL + "/add", true, jemp.toString());

            Log.e("hello"," si thu "+i);

        }

    }


    public static List<VoucherDetails> getVoucherDetails(){

        List<VoucherDetails> vd=new ArrayList<VoucherDetails>();

        vd.add(new VoucherDetails("sithu","10","5","50"));
        vd.add(new VoucherDetails("lin htut","10","4","50"));
        vd.add(new VoucherDetails("siaathu","10","3","50"));
        vd.add(new VoucherDetails("bbbb","10","2","50"));
        vd.add(new VoucherDetails("dddd","10","1","50"));

        return vd;




    }
}
