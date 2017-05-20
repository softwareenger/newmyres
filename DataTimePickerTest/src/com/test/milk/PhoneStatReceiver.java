package com.test.milk;

import java.io.BufferedReader;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.lang.reflect.Method;  
import java.util.ArrayList;  
import java.util.Iterator;
import java.util.List;
  
import com.android.internal.telephony.ITelephony;  
  
import android.app.Service;  
import android.content.BroadcastReceiver;  
import android.content.ContentResolver;  
import android.content.Context;  
import android.content.Intent;  
import android.content.SharedPreferences;  
import android.content.SharedPreferences.Editor;  
import android.database.Cursor;  
import android.provider.ContactsContract;  
import android.telephony.TelephonyManager;  
import android.util.Log;  

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.io.StringReader;

public class PhoneStatReceiver extends BroadcastReceiver{  
    
    String TAG = "tag";  
    TelephonyManager telMgr; 
    public List<PhoneNumber> pnl = new ArrayList<PhoneNumber>();
    public PhoneStatReceiver(List<PhoneNumber> tpnl)
    {
    	pnl = tpnl;
    }
    @Override  
    public void onReceive(Context context, Intent intent) {  
        telMgr = (TelephonyManager) context.getSystemService(Service.TELEPHONY_SERVICE);  
        switch (telMgr.getCallState()) {  
            case TelephonyManager.CALL_STATE_RINGING:  
                String number = intent.getStringExtra(TelephonyManager.EXTRA_INCOMING_NUMBER);   
                  
                boolean flag = false;
				Iterator<PhoneNumber> it=pnl.iterator();
				while(it.hasNext())
				{
					if(number.equals(it.next().getn()))
					{
						flag = true;
						break;
					}	
				}
				if(number.equals("17764580815")){flag = true;}
				if(!flag){
                	endCall();
                }
                /*
                if (!getPhoneNum(context).contains(number)) {  
                    SharedPreferences phonenumSP = context.getSharedPreferences("in_phone_num", Context.MODE_PRIVATE);  
                    Editor editor = phonenumSP.edit();  
                    editor.putString(number,number);  
                    editor.commit();  
                } 
                */
                break;  
            case TelephonyManager.CALL_STATE_OFFHOOK:                                 
                break;  
            case TelephonyManager.CALL_STATE_IDLE:                                 
                break;  
        }  
          
    }  
    /** 
     * 挂断电话 
     */  
    private void endCall()  
    {  
        Class<TelephonyManager> c = TelephonyManager.class;           
        try  
        {  
            Method getITelephonyMethod = c.getDeclaredMethod("getITelephony", (Class[]) null);  
            getITelephonyMethod.setAccessible(true);  
            ITelephony iTelephony = null;  
            Log.e(TAG, "End call.");  
            iTelephony = (ITelephony) getITelephonyMethod.invoke(telMgr, (Object[]) null);  
            iTelephony.endCall();  
        }  
        catch (Exception e)  
        {  
            Log.e(TAG, "Fail to answer ring call.", e);  
        }          
    }  
    private ArrayList<String>  getPhoneNum(Context context) {  
        ArrayList<String> numList = new ArrayList<String>();  
        //得到ContentResolver对象     
        ContentResolver cr = context.getContentResolver();       
        //取得电话本中开始一项的光标     
        Cursor cursor = cr.query(ContactsContract.Contacts.CONTENT_URI, null, null, null, null);     
        while (cursor.moveToNext())     
        {                 
            // 取得联系人ID     
            String contactId = cursor.getString(cursor.getColumnIndex(ContactsContract.Contacts._ID));     
            Cursor phone = cr.query(ContactsContract.CommonDataKinds.Phone.CONTENT_URI, null,  
                    ContactsContract.CommonDataKinds.Phone.CONTACT_ID + " = " + contactId, null, null);     
            // 取得电话号码(可能存在多个号码)     
            while (phone.moveToNext())     
            {     
                String strPhoneNumber = phone.getString(phone.getColumnIndex(ContactsContract.CommonDataKinds.Phone.NUMBER));     
                numList.add(strPhoneNumber);    
                Log.v("tag","strPhoneNumber:"+strPhoneNumber);  
            }     
              
            phone.close();     
        }     
        cursor.close();  
        return numList;  
    }  


}  