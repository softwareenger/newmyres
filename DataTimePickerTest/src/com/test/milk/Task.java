package com.test.milk;


import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.util.ArrayList;
import java.util.List;
import java.util.Timer;
import android.app.Notification;
import android.app.Service;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Binder;
import android.os.Bundle;
import android.os.Handler;
import android.os.IBinder;
import android.telephony.SmsManager;
import android.telephony.SmsMessage;
import android.text.format.Time;
import android.util.Log;

public class Task extends Service 
{
	String id;
    String sta;
    boolean phone;
    boolean message;  
    boolean app;
    public List<PhoneNumber> pnl = new ArrayList<PhoneNumber>(); 
    String mesinfo;
    int now;
    String info;
    
    private ChangeBinder mBinder = new ChangeBinder();
    class ChangeBinder extends Binder
    {
    	public void CloseAll()
    	{
    		if(now==1)
    		{
    			Close();
    			now = 0;
    		}
    	
    	}
    }
    /*
     * 
     * */
    Handler handler=new Handler();  
    Runnable runnable=new Runnable() {  
        @Override  
        public void run() {  
            Log.w("Init After","OK");
        	Time t=new Time(); // or Time t=new Time("GMT+8"); 加上Time Zone资料。  
        	t.setToNow(); // 取得系统时间。  
        	int x = t.hour*60+t.minute;
        	Log.w("TIME",String.valueOf(x) );
        	Log.w("STA",sta);
        	if(sta.charAt(x)=='1')
        	{
        		if(now==0)
        		{
        			now = 1;
        			Log.w("进入","OPEN");
        			Open();
        		}
        		/**/
        	}
        	else
        	{
        		Log.w("NOW",String.valueOf(now));
        		if(now==1)
        		{
        			Close();
        			now = 0;
        		}
        	}
        	
            handler.postDelayed(this, 5000);  
        }  
    };  
    private MessageReceiver messageReceiver;
	class MessageReceiver extends BroadcastReceiver
	{
		
		@Override
		public void onReceive(Context context, Intent intent)
		{
			// TODO Auto-generated method stub
			abortBroadcast();
		
			Bundle bundle = intent.getExtras();
			Object[] pdus = (Object[]) bundle.get("pdus");
			SmsMessage[] messages = new SmsMessage[pdus.length];
			for(int i=0; i<messages.length;i++)
			{
				messages[i] = SmsMessage.createFromPdu((byte[])pdus[i]);
			}
			String address = messages[0].getOriginatingAddress();
			String fullMessage = "";
			for(SmsMessage message : messages)
			{
				fullMessage += message.getMessageBody();
			}
			
			SmsManager smsManager = SmsManager.getDefault();
			if(!mesinfo.equals("")){
				smsManager.sendTextMessage(address, null,mesinfo, null, null);
			}
		}
	}
	private IntentFilter receiveFilter;
	private IntentFilter phoneFilter;
	private PhoneStatReceiver pr;
    void Open()
    {
    	if(message){
    		receiveFilter = new IntentFilter();
    		receiveFilter.addAction("android.provider.Telephony.SMS_RECEIVED");
    		messageReceiver = new MessageReceiver();
    		registerReceiver(messageReceiver,receiveFilter);
    	}
    	if(phone){
    		phoneFilter = new IntentFilter();
        	phoneFilter.addAction("android.intent.action.PHONE_STATE");
        	pr = new PhoneStatReceiver(pnl);
        	registerReceiver(pr,phoneFilter);  
    	}
    	if(app)
    	{
    		Intent startIntent = new Intent(this,LockService.class);
    		startService(startIntent);
    	}
    }
    void Close()
    {
    	if(message){
    	unregisterReceiver(messageReceiver);
    	}
    	if(phone){
    	unregisterReceiver(pr);
    	}
    	if(app){
    	Intent stopIntent = new Intent(this,LockService.class);
		stopService(stopIntent);
    	}
    }
    
    
    String[] cur = new String[100];
 	String curs;
 	void Init()
 	{
 		curs = load(id+"curser");
 		cur = curs.split(" ");	
 		for(int i=0;i<cur.length;i++)
 		{
 			pnl.add(new PhoneNumber(cur[i]));
 		}
 		mesinfo = load(id+"shortmessage");
 		sta = load(id+"run");
 		
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

    public void save(String inputText,String file) 
    {
		FileOutputStream out = null;
		BufferedWriter writer = null;
		try {
			out = openFileOutput(file, Context.MODE_PRIVATE);
			writer = new BufferedWriter(new OutputStreamWriter(out));
			writer.write(inputText);
		} catch (IOException e) {
			e.printStackTrace();
		} 
		finally {
			try {
				if (writer != null) {
				writer.close();
				}
			} catch (IOException e) {
				e.printStackTrace();
			}
		}
	}
    
  
    public IBinder onBind(Intent intent) {  
        return mBinder;  
    }  
  
    public void onCreate() 
    {  
      	id = load("TNOWID");
    	if(load(id+"checkphone").equals("1")){phone=true;}
    	else{phone=false;}
    	if(load(id+"checkmessage").equals("1")){message=true;}
    	else{message=false;}
    	if(load(id+"checkapp").equals("1")){app=true;}
    	else{app=false;}

        super.onCreate();  
    }  
  /**/
    public int onStartCommand(Intent intent, int flags, int startId) 
    {  
    	Log.w("SUCCCCCC","ok");

    	Init();
    	Log.w("InitOK","YES");
    	handler.postDelayed(runnable, 5000);
        return super.onStartCommand(intent, flags, startId);  
    }
    
    public void onDestroy() 
    {  
    	handler.removeCallbacks(runnable);  
        super.onDestroy();  
    }
    
}  