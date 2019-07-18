package com.example.asus.stationaryapp.Model;

import android.util.Log;

import com.example.asus.stationaryapp.JSONParser;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

public class Collection extends HashMap<String, String>
{

   // static String host = "172.17.102.164";

    //static String host = "172.17.103.57";

    static String host = "172.17.32.255";
    static String baseURL;

    static {
        baseURL = String.format("http://%s/stationaryappstoreapi/api/collection", host);

    }



    public Collection(String id, String name) {
        put("CollectionID", id);
        put("Location", name);

    }

    public static List<Collection> ReadCollection() {
        List<Collection> list = new ArrayList<Collection>();
        JSONArray a = JSONParser.getJSONArrayFromUrl(String.format(baseURL));
        try {
            for (int i =0; i<a.length(); i++) {
                JSONObject b = a.getJSONObject(i);
                list.add(new Collection(Integer.toString(b.getInt("CollectionID")),
                        b.getString("Location")));
            }
        } catch (Exception e) {
            Log.e("Request", "JSONArray error");
        }
        return(list);
    }



}
