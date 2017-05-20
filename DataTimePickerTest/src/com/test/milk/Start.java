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




import android.annotation.SuppressLint;
import android.app.Activity;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import java.lang.reflect.Method;
public class Start extends Activity implements OnClickListener{
		
	Button login;
	Button register;
	
	   @Override
      protected void onCreate(Bundle savedInstanceState) 
      {   	
	        super.onCreate(savedInstanceState);
	        requestWindowFeature(Window.FEATURE_NO_TITLE);
	        setContentView(R.layout.start);
	        
	        login = (Button) findViewById(R.id.Login); 
	        register = (Button) findViewById(R.id.Register); 
	        login.setOnClickListener(this);
	        register.setOnClickListener(this);
	    }
	
		@Override
		public void onClick(View v) {
			Intent intent;
			switch(v.getId())
			{
				
				case R.id.Login:
					intent = new Intent(this,Login.class);
					startActivity(intent);
					overridePendingTransition(R.anim.in_from_right, R.anim.out_to_left);
					break;
				case R.id.Register:
					intent = new Intent(this,Register.class);
					startActivity(intent);
					overridePendingTransition(R.anim.in_register, R.anim.out_register);
					break;
			}
		}  
	
    
  
	    	
}
