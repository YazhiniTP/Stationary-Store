package com.example.asus.stationaryapp;

import android.app.DatePickerDialog;
import android.app.Dialog;
import android.os.Bundle;
import android.support.v4.app.DialogFragment;
import android.widget.DatePicker;
import android.widget.TextView;
import android.widget.Toast;

import com.example.asus.stationaryapp.Activity.ManageDelegationActivity;

import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;

public class MyDatePickerFragment1 extends DialogFragment {
    static int passyear;
    static int passmonth;
    static int passday;
    public static Calendar cal;

    @Override
    public Dialog onCreateDialog(Bundle savedInstanceState) {

        final Calendar c = Calendar.getInstance();
        int year = c.get(Calendar.YEAR);
        int month = c.get(Calendar.MONTH);
        int day = c.get(Calendar.DAY_OF_MONTH);

        passyear=year;
        passmonth=month;
        passday=day;




        cal = Calendar.getInstance();
        cal.set(Calendar.YEAR, MyDatePickerFragment.passyear);
        cal.set(Calendar.MONTH, MyDatePickerFragment.passmonth);
        cal.set(Calendar.DAY_OF_MONTH, MyDatePickerFragment.passday);
        cal.set(Calendar.HOUR_OF_DAY, 0);
        cal.set(Calendar.MINUTE, 0);
        cal.set(Calendar.SECOND, 0);
        cal.set(Calendar.MILLISECOND, 0);


        DatePickerDialog datePickerDialog=new DatePickerDialog(getActivity(), dateSetListener, year, month, day);




        datePickerDialog.getDatePicker().setMinDate(cal.getTime().getTime());

        return datePickerDialog;
    }


    public int getYear(){
        return passyear;
    }

    public void setYear(int year){
        passyear=year;
    }

    public int getDate(){
        return passday;
    }

    public void setDay(int day){
        passday=day;
    }

    public void setMonth(int month){
        passmonth=month;
    }

    public int getMonth(){
        return passmonth;
    }

    private DatePickerDialog.OnDateSetListener dateSetListener =
            new DatePickerDialog.OnDateSetListener() {
                public void onDateSet(DatePicker view, int year, int month, int day) {
                    /*Toast.makeText(getActivity(), "selected date is " + view.getYear() +
                            " / " + (view.getMonth()+1) +
                            " / " + view.getDayOfMonth(), Toast.LENGTH_SHORT).show();
*/


                    passyear=view.getYear();
                    passmonth=view.getMonth();
                    passday=view.getDayOfMonth();

                    Calendar c = Calendar.getInstance();
                    c.set(passyear, passmonth, passday);
                    cal.set(passyear, passmonth, passday);

                    SimpleDateFormat sdf = new SimpleDateFormat("dd MMMM yyyy");
                    String formattedDate = sdf.format(c.getTime());


                    TextView activityText2 = (TextView) getActivity().findViewById(R.id.textViewdate2);
                    activityText2.setText (formattedDate);
                }
            };
}
