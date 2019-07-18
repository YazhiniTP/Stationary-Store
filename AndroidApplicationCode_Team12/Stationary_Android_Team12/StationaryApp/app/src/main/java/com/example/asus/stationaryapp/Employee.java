package com.example.asus.stationaryapp;

import android.content.SharedPreferences;
import android.util.Log;

import com.example.asus.stationaryapp.Model.Request;

import org.json.JSONArray;
import org.json.JSONObject;

import java.net.URLEncoder;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

public class Employee
    extends HashMap<String, String>
    {
        //http://172.17.101.67/WebStationary/

        // http://172.17.101.67/WebStationary/



        static String host = "172.17.32.255";

        //static String host = "172.17.103.57";

        static String baseURL1;
        static String baseURL;

        static String roleURL;

        static String tokenURL;
        static {


            //baseURL1 = String.format("http://192.168.1.106/stationaryappstoreapi/api/user/email/sh@yahoo.com/");

            baseURL1 = String.format("http://%s/stationaryappstoreapi/api/user",host);

            baseURL = String.format("http://%s/stationaryappstoreapi/api/employee",host);

            roleURL = String.format("http://%s/stationaryappstoreapi/home/find",host);



        tokenURL=String.format("http://%s/stationaryappstoreapi/Token",host);

    }



    public Employee(String id, String name, String address) {
        put("EmployeeID", id);
        put("Name", name);
        put("DepartmentID", address);

    }

        public Employee(String id, String name, String address, String email) {
            put("EmployeeID", id);
            put("Name", name);
            put("DepartmentID", address);

            put("Email",email);
        }





        public static void Login(SharedPreferences pref){
        try {
            String id = URLEncoder.encode(pref.getString("username", ""), "UTF-8");
            String pw = URLEncoder.encode(pref.getString("password", ""), "UTF-8");
            //String id="stlh1234@gmail.com";
            //String pw="Asdf123456!";
            Log.e("////////////// Id  ",id);
            Log.e("////////////// PW  ",pw);
            String credential = String.format("username=%s&password=%s&grant_type=password", id, pw);
            String result = JSONParser.postStream(tokenURL, false, credential);

            Log.e("//////// Result",result);

            JSONObject res = new JSONObject(result);
            if (res.has("access_token")){
                JSONParser.access_token = res.getString("access_token");
                Log.e("//////// Access Token",JSONParser.access_token);
            }

        } catch (Exception e) {
            JSONParser.access_token = "";
            Log.e("Login", e.toString());
        }
    }

        public static Employee ReadEmployee(String email) {
            try {
                JSONObject a = JSONParser.getJSONFromUrl(String.format("%s/%s",baseURL1+"/email", email+"/"));
                //JSONObject a = JSONParser1.getJSONFromUrl(String.format(baseURL1));
                Log.e("apple","apple");
                Employee e = new Employee(Integer.toString(a.getInt("EmployeeID")),
                        a.getString("Name"),
                        Integer.toString(a.getInt("DepartmentID"))
                        );
                return e;
            } catch (Exception e) {
                Log.e("Employee", "JSONArray error");
            }
            return(null);
        }


        public static String getRole() {



            String role=JSONParser.getStream(String.format(roleURL));

            return role;


        }


        public static List<Employee> ReadRepEmployee(String did) {

                List<Employee> list = new ArrayList<Employee>();
                JSONArray a = JSONParser.getJSONArrayFromUrl(String.format("%s/%s", baseURL + "/departmentid", did));
                try {
                    for (int i = 0; i < a.length(); i++) {
                        JSONObject b = a.getJSONObject(i);
                        list.add(new Employee(Integer.toString(b.getInt("EmployeeID")),
                                b.getString("Name"),
                                Integer.toString(b.getInt("DepartmentID")),
                                b.getString("Email")));
                    }
                } catch (Exception e) {
                    Log.e("Employee", "JSONArray error");
                }

            return (list);
        }







}
