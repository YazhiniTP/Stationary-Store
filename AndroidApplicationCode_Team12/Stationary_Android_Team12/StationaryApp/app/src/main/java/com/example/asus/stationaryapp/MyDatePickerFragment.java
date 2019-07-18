package com.example.asus.stationaryapp;

import android.app.DatePickerDialog;
import android.app.Dialog;
import android.os.Bundle;
import android.support.v4.app.DialogFragment;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.example.asus.stationaryapp.Activity.ManageCollectionActivity;
import com.example.asus.stationaryapp.Activity.ManageDelegationActivity;

import java.text.SimpleDateFormat;
import java.util.Calendar;


public class MyDatePickerFragment extends DialogFragment {
    static int passyear;
    static int passmonth;
    static int passday;
    private EditText mEditText;




    @Override
    public Dialog onCreateDialog(Bundle savedInstanceState) {

        final Calendar c = Calendar.getInstance();
        int year = c.get(Calendar.YEAR);
        int month = c.get(Calendar.MONTH);
        int day = c.get(Calendar.DAY_OF_MONTH);

        passmonth=year;
        passmonth=month;
        passday=day;

        DatePickerDialog datePickerDialog=new DatePickerDialog(getActivity(), dateSetListener, year, month, day);




        datePickerDialog.getDatePicker().setMinDate(c.getTimeInMillis());





        return datePickerDialog;
    }

    public void setYear(int year){
        passyear=year;
    }



    public void setDay(int day){
        passday=day;
    }

    public void setMonth(int month){
        passmonth=month;
    }


    public int getYear(){
        return passyear;
    }

    public int getDate(){
        return passday;
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

                    SimpleDateFormat sdf = new SimpleDateFormat("dd MMMM yyyy");
                    String formattedDate = sdf.format(c.getTime());


                    TextView activityText = (TextView) getActivity().findViewById(R.id.textViewdate1);
                    activityText.setText (formattedDate);

                    Calendar c1=Calendar.getInstance();

                    int year2 = new MyDatePickerFragment1().getYear();
                   int  month2 = new MyDatePickerFragment1().getMonth();
                    int day2 = new MyDatePickerFragment1().getDate();

                    c1.set(year2,month2,day2);

                    if(c.after(c1)){
                        TextView activityText1 = (TextView) getActivity().findViewById(R.id.textViewdate2);
                        activityText1.setText (formattedDate);

                        new MyDatePickerFragment1().setYear(passyear);
                        new MyDatePickerFragment1().setDay(passday);
                        new MyDatePickerFragment1().setMonth(passmonth);
                    }



                }
            };
}
