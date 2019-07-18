package com.example.asus.stationaryapp.Activity;

import android.Manifest;
import android.app.Activity;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Color;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Environment;
import android.support.annotation.NonNull;
import android.support.v4.app.ActivityCompat;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.Toast;

import com.example.asus.stationaryapp.Adapter.RVCatalogueAdapter;
import com.example.asus.stationaryapp.ClickListener;
import com.example.asus.stationaryapp.Model.Catalogue;
import com.example.asus.stationaryapp.Model.Disbursement;
import com.example.asus.stationaryapp.R;
import com.github.gcacace.signaturepad.views.SignaturePad;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.util.List;

public class SignActivity extends AppCompatActivity {

    private static final int REQUEST_EXTERNAL_STORAGE = 1;
    private static String[] PERMISSIONS_STORAGE = {Manifest.permission.WRITE_EXTERNAL_STORAGE};
    private SignaturePad mSignaturePad;
    private Button mClearButton;
    private Button mSaveButton;



    String depid;
    String empname;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        verifyStoragePermissions(this);
        setContentView(R.layout.activity_sign);

        mSignaturePad = (SignaturePad) findViewById(R.id.signature_pad);
        mSignaturePad.setOnSignedListener(new SignaturePad.OnSignedListener() {
            @Override
            public void onStartSigning() {
                //Toast.makeText(SignActivity.this, "OnStartSigning", Toast.LENGTH_SHORT).show();
            }

            @Override
            public void onSigned() {
                mSaveButton.setEnabled(true);
                mClearButton.setEnabled(true);
            }

            @Override
            public void onClear() {
                mSaveButton.setEnabled(false);
                mClearButton.setEnabled(false);
            }
        });

        mClearButton = (Button) findViewById(R.id.clear_button);
        mSaveButton = (Button) findViewById(R.id.save_button);

        mClearButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                mSignaturePad.clear();
            }
        });

        mSaveButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                Disbursement disbursement = DeliveryActivity.disbursement;



                depid=DeliveryActivity.depid;
                empname=DeliveryActivity.empname;



             /*   new AsyncTask<Disbursement, Void, Void>() {
                    @Override
                    protected Void doInBackground(Disbursement... params) {


                        Disbursement.ConfirmDelivery(params[0]);



                        return null;
                    }
                }.execute(disbursement);*/

                new AsyncTask<Disbursement, Void, Integer>() {
                    @Override
                    protected Integer doInBackground(Disbursement... params) {
                        Disbursement.ConfirmDelivery(params[0]);
                        return 1;
                    }

                    @Override
                    protected void onPostExecute(Integer result) {

                        Bitmap signatureBitmap = mSignaturePad.getSignatureBitmap();
                        if (addJpgSignatureToGallery(signatureBitmap)) {
                            //Toast.makeText(SignActivity.this, "Signature saved into the Gallery", Toast.LENGTH_SHORT).show();
                        } else {
                            //Toast.makeText(SignActivity.this, "Unable to store the signature", Toast.LENGTH_SHORT).show();
                        }
                        if (addSvgSignatureToGallery(mSignaturePad.getSignatureSvg())) {
                            //Toast.makeText(SignActivity.this, "SVG Signature saved into the Gallery", Toast.LENGTH_SHORT).show();
                        } else {
                            //Toast.makeText(SignActivity.this, "Unable to store the SVG signature", Toast.LENGTH_SHORT).show();
                        }


                        runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                //UI related code

                                android.app.AlertDialog.Builder builder = new android.app.AlertDialog.Builder(SignActivity.this);
                                builder.setMessage("Delivery completed"
                                )
                                        .setCancelable(false)
                                        .setPositiveButton("OK", new DialogInterface.OnClickListener() {
                                            public void onClick(DialogInterface dialog, int id) {
                                                //do things

                                                // we have finished this Activity
                                                Intent data = new Intent();

                                                data.putExtra("success", 1);


                                                setResult(RESULT_OK, data); // we have finished this Activity
                                                finish();


                                            }
                                        });
                                android.app.AlertDialog alert = builder.create();
                                alert.show();

                            }
                        });





                    }
                }.execute(disbursement);


            }
        });
    }

    @Override
    public void onRequestPermissionsResult(int requestCode,
                                           @NonNull String permissions[], @NonNull int[] grantResults) {
        switch (requestCode) {
            case REQUEST_EXTERNAL_STORAGE: {
                // If request is cancelled, the result arrays are empty.
                if (grantResults.length <= 0
                        || grantResults[0] != PackageManager.PERMISSION_GRANTED) {
                    //Toast.makeText(SignActivity.this, "Cannot write images to external storage", Toast.LENGTH_SHORT).show();
                }
            }
        }
    }

    public File getAlbumStorageDir(String albumName) {
        // Get the directory for the user's public pictures directory.
        File file = new File(Environment.getExternalStoragePublicDirectory(
                Environment.DIRECTORY_PICTURES), albumName);
        if (!file.mkdirs()) {
            Log.e("SignaturePad", "Directory not created");
        }
        return file;
    }

    public void saveBitmapToJPG(Bitmap bitmap, File photo) throws IOException {
        Bitmap newBitmap = Bitmap.createBitmap(bitmap.getWidth(), bitmap.getHeight(), Bitmap.Config.ARGB_8888);
        Canvas canvas = new Canvas(newBitmap);
        canvas.drawColor(Color.WHITE);
        canvas.drawBitmap(bitmap, 0, 0, null);
        OutputStream stream = new FileOutputStream(photo);
        newBitmap.compress(Bitmap.CompressFormat.JPEG, 80, stream);
        stream.close();
    }

    public boolean addJpgSignatureToGallery(Bitmap signature) {
        boolean result = false;
        try {
            File photo = new File(getAlbumStorageDir("SignaturePad"), String.format("Signature_%s%s%d.jpg","dep"+depid," emp"+empname, System.currentTimeMillis()));

            saveBitmapToJPG(signature, photo);
            scanMediaFile(photo);
            result = true;
        } catch (IOException e) {
            e.printStackTrace();
        }
        return result;
    }

    private void scanMediaFile(File photo) {
        Intent mediaScanIntent = new Intent(Intent.ACTION_MEDIA_SCANNER_SCAN_FILE);
        Uri contentUri = Uri.fromFile(photo);
        mediaScanIntent.setData(contentUri);
        SignActivity.this.sendBroadcast(mediaScanIntent);
    }

    public boolean addSvgSignatureToGallery(String signatureSvg) {
        boolean result = false;
        try {

            File svgFile = new File(getAlbumStorageDir("SignaturePad"), String.format("Signature_%s%s%d.svg","dep"+depid," emp"+empname, System.currentTimeMillis()));

            OutputStream stream = new FileOutputStream(svgFile);
            OutputStreamWriter writer = new OutputStreamWriter(stream);
            writer.write(signatureSvg);
            writer.close();
            stream.flush();
            stream.close();
            scanMediaFile(svgFile);
            result = true;
        } catch (IOException e) {
            e.printStackTrace();
        }
        return result;
    }

    /**
     * Checks if the app has permission to write to device storage
     * <p/>
     * If the app does not has permission then the user will be prompted to grant permissions
     *
     * @param activity the activity from which permissions are checked
     */
    public static void verifyStoragePermissions(Activity activity) {
        // Check if we have write permission
        int permission = ActivityCompat.checkSelfPermission(activity, Manifest.permission.WRITE_EXTERNAL_STORAGE);

        if (permission != PackageManager.PERMISSION_GRANTED) {
            // We don't have permission so prompt the user
            ActivityCompat.requestPermissions(
                    activity,
                    PERMISSIONS_STORAGE,
                    REQUEST_EXTERNAL_STORAGE
            );



        }
    }



}
