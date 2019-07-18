package com.example.asus.stationaryapp.Adapter;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.support.v7.widget.CardView;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import com.example.asus.stationaryapp.Model.Request;
import com.example.asus.stationaryapp.R;

import java.util.List;

public class RequestAdapter extends
        RecyclerView.Adapter<RequestAdapter.PersonViewHolder> {

    private static ClickListener clickListener;

    public static class PersonViewHolder extends RecyclerView.ViewHolder {
        CardView cv;
        TextView RequestDate;
        TextView RequestName;
        TextView RequestId;


        ImageView FoodPhoto;

        View itemView;




        PersonViewHolder(View itemView) {
            super(itemView);
            this.itemView=itemView;
            cv = (CardView)itemView.findViewById(R.id.card_view);
            RequestId = (TextView)itemView.findViewById(R.id.textOne);
            RequestName = (TextView)itemView.findViewById(R.id.textTwo);
            RequestDate = (TextView)itemView.findViewById(R.id.textThree);




        }
    }
    List<Request> requests;
    public RequestAdapter(List<Request> persons){
        this.requests = persons;
    }
    @Override
    public void onAttachedToRecyclerView(RecyclerView recyclerView) {
        super.onAttachedToRecyclerView(recyclerView);
    }
    @Override
    public PersonViewHolder onCreateViewHolder(ViewGroup viewGroup, int i){
        View v = LayoutInflater.from(viewGroup.getContext())
                .inflate(R.layout.card_view_request, viewGroup, false);
        return new PersonViewHolder(v);
    }
    @Override
    public void onBindViewHolder(PersonViewHolder personViewHolder, final int i){
        personViewHolder.RequestId.setText(requests.get(i).get("EmployeeName"));
        personViewHolder.RequestName.setText(requests.get(i).get("Amt"));

        String date1=requests.get(i).get("SubmissionDate");
        int datei= date1.indexOf("T");

        String submissiondate=date1.substring(0,datei);

        personViewHolder.RequestDate.setText(submissiondate);





       /* personViewHolder.FoodPhoto
                .setImageResource(voucherDetails.get(i).getPhotoId());*/

        final String id= requests.get(i).get("RequestID");
        final String name= requests.get(i).get("EmployeeName");
        final String date= requests.get(i).get("SubmissionDate");


        personViewHolder.itemView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {


                clickListener.onItemClick(id,name,date, v);
            }
        });
    }

    public static Bitmap getImage(byte[] image) {
        return BitmapFactory.decodeByteArray(image, 0, image.length);
    }

    @Override
    public int getItemCount() {
        return requests.size();
    }


    public void setOnItemClickListener(ClickListener clickListener) {
        RequestAdapter.clickListener = clickListener;
    }

    public interface ClickListener {
        void onItemClick(String id,String name, String date, View v);

    }
}

