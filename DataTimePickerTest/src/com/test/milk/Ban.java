package com.test.milk;

import java.io.*;
import java.net.*;
import java.util.ArrayList;
import java.util.List;

import org.xmlpull.v1.XmlPullParser;
import org.xmlpull.v1.XmlPullParserFactory;



import com.test.milk.LockService;

import android.media.AudioManager;
import android.media.audiofx.BassBoost.Settings;
import android.os.*;
import android.telephony.*;
import android.text.TextUtils;
import android.util.Log;
import android.view.*;
import android.view.View.OnClickListener;
import android.view.View.OnLongClickListener;




import android.annotation.SuppressLint;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.BroadcastReceiver;
import android.content.ComponentName;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.ServiceConnection;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import java.lang.reflect.Method;
public class Ban extends Activity implements OnLongClickListener{
		
	Button stop;
	String id;
	String sta;
	private Task.ChangeBinder mBinder;

	  private ServiceConnection connection = new ServiceConnection()
	  {
		  @Override
		  public void onServiceDisconnected(ComponentName name){}
		  @Override
		  public void onServiceConnected(ComponentName name,IBinder service)
		  {
			  mBinder = (Task.ChangeBinder) service;
			  
		  }
	  };
	  
	   @Override
      protected void onCreate(Bundle savedInstanceState) 
      {   	
	        super.onCreate(savedInstanceState);
	        requestWindowFeature(Window.FEATURE_NO_TITLE);
	        setContentView(R.layout.ban);
	       
	        
	        Intent bindIntent = new Intent(this,Task.class);
	        startService(bindIntent);
			bindService(bindIntent,connection,BIND_AUTO_CREATE);
			
	        stop = (Button) findViewById(R.id.stop); 
	        stop.setOnLongClickListener(this);
	 
	        
	        
	    }
	
		@Override
		public boolean onLongClick(View v) {
			switch(v.getId())
			{
				
				case R.id.stop:
					

					AlertDialog.Builder dialog = new AlertDialog.Builder(this);
	        		dialog.setTitle("think again");
	        		dialog.setMessage( "Are you sure?" );
	    
	        		dialog.setCancelable(false);
	        		dialog.setPositiveButton("no", new DialogInterface.OnClickListener() {
						
						@Override
							public void onClick(DialogInterface arg0, int arg1) 
							{
														
							}
						}
	        		);
	        		dialog.setNegativeButton("yes", new DialogInterface.OnClickListener() {
						
						@Override
							public void onClick(DialogInterface arg0, int arg1)
							{
								Toast.makeText(Ban.this, "exit success", Toast.LENGTH_SHORT).show();
								mBinder.CloseAll();
								unbindService(connection);
								Intent bindIntent = new Intent(Ban.this,Task.class);
							    stopService(bindIntent);
							    finish();
							}
						}
	        		);
	        		dialog.show();
				  
					break;
			}
			return false;
		}  
	
  
    public boolean onKeyDown(int keyCode, KeyEvent event) {

        switch (keyCode) {
            case KeyEvent.KEYCODE_HOME:
                return true;
            case KeyEvent.KEYCODE_BACK:
                return true;
            case KeyEvent.KEYCODE_CALL:
                return true;
            case KeyEvent.KEYCODE_SYM:
                return true;
            case KeyEvent.KEYCODE_VOLUME_DOWN:
                return true;
            case KeyEvent.KEYCODE_VOLUME_UP:
                return true;
            case KeyEvent.KEYCODE_STAR:
                return true;
        }
        return super.onKeyDown(keyCode, event);
    }
    
    

	
	public String load(String file) {
		FileInputStream in = null;
		BufferedReader reader = null;
		StringBuilder content = new StringBuilder();
		try {
			in = openFileInput(file);
			reader = new BufferedReader(new InputStreamReader(in));
			String line = "";
			while ((line = reader.readLine()) != null) {
				content.append(line);
			}
		} catch (IOException e) {	e.printStackTrace();}
		finally {
			if (reader != null) {
				try {
					reader.close();
				} catch (IOException e) {
					e.printStackTrace();
				}
			}
		}
			return content.toString();
	}


	    	
}
