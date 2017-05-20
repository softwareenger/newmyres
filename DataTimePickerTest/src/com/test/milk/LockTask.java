package com.test.milk;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.lang.reflect.Field;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Date;
import java.util.HashSet;
import java.util.Iterator;
import java.util.List;
import java.util.Set;
import java.util.TimerTask;
import java.util.TreeMap;

import com.baoyz.swipemenulistview.SwipeMenu;
import com.baoyz.swipemenulistview.SwipeMenuCreator;
import com.baoyz.swipemenulistview.SwipeMenuItem;
import com.baoyz.swipemenulistview.SwipeMenuListView;

import android.app.Activity;
import android.app.ActivityManager;
import android.app.ActivityManager.RunningAppProcessInfo;
import android.app.ActivityManager.RunningTaskInfo;
import android.app.usage.UsageStats;
import android.app.usage.UsageStatsManager;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.graphics.Color;
import android.graphics.drawable.ColorDrawable;
import android.media.audiofx.BassBoost.Settings;
import android.os.Build;
import android.os.Build.VERSION;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

public class LockTask extends TimerTask {  
    public static final String TAG = "LockTask";  
    private Context mContext;  
    private static ActivityManager mActivityManager;  
    
    ArrayList<String>data;
    
    public LockTask(Context context,ArrayList<String> tdata)
    {  
    	data = new ArrayList<String>();
    	data = tdata;
        mContext = context;  
        mActivityManager = (ActivityManager) context.getSystemService("activity");  
    }  
    
    public static String getTopAppName(Context context) {
        
        String strName = "";
        try {
            if (Build.VERSION.SDK_INT >= 21) {
                strName = getLollipopFGAppPackageName(context);
            } else {
                strName = mActivityManager.getRunningTasks(1).get(0).topActivity.getPackageName();
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        return strName;
    }


    private static String getLollipopFGAppPackageName(Context ctx) {

        try {
            UsageStatsManager usageStatsManager = (UsageStatsManager) ctx.getSystemService("usagestats");
            long milliSecs = 60 * 1000;
            Date date = new Date();
            List<UsageStats> queryUsageStats = usageStatsManager.queryUsageStats(UsageStatsManager.INTERVAL_DAILY, date.getTime() - milliSecs, date.getTime());
            if (queryUsageStats.size() > 0) {
                Log.i("LPU", "queryUsageStats size: " + queryUsageStats.size());
            }
            long recentTime = 0;
            String recentPkg = "";
            for (int i = 0; i < queryUsageStats.size(); i++) {
                UsageStats stats = queryUsageStats.get(i);
                if (i == 0 && !"org.pervacio.pvadiag".equals(stats.getPackageName())) {
                    Log.i("LPU", "PackageName: " + stats.getPackageName() + " " + stats.getLastTimeStamp());
                }
                if (stats.getLastTimeStamp() > recentTime) {
                    recentTime = stats.getLastTimeStamp();
                    recentPkg = stats.getPackageName();
                }
            }
            return recentPkg;
        } catch (Exception e) {
            e.printStackTrace();
        }
        return "";
    }
   
    @Override  
    public void run() 
    {  
    	
    	String packageName;
    	packageName = getTopAppName(mContext);
        Iterator<String> it=data.iterator();
        boolean flag = false;
        Log.w("DATASIZE",String.valueOf(data.size()) );
        
	    while(it.hasNext())
	    {
	    	if( packageName.equals( it.next() ) ){flag = true;break;}
	    }
	    if(packageName.equals("com.test.milk")){flag = false;}
	    Log.w("PAG",packageName);

	    if(flag)
	    {
	    	    Intent intent = new Intent();  
	            intent.setClassName("com.test.milk", "com.test.milk.Ban");  
	            intent.setFlags(	Intent.FLAG_ACTIVITY_NEW_TASK); 
	            
	            mContext.startActivity(intent);  
	    }
	 
    }  
    
    
    
 
    
}  