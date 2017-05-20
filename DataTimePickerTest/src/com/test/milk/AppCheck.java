package com.test.milk;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;

import com.baoyz.swipemenulistview.SwipeMenu;  
import com.baoyz.swipemenulistview.SwipeMenuCreator;  
import com.baoyz.swipemenulistview.SwipeMenuItem;  
import com.baoyz.swipemenulistview.SwipeMenuListView;  
import com.example.compoundbuttonview.view.CheckSwitchButton;
import com.example.ksl.MyAdapter;


  
import android.support.v7.app.ActionBarActivity;  
import android.util.Log;  
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;  
import android.widget.ListView;
import android.widget.Toast;  
import android.widget.AdapterView.OnItemClickListener;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;  
import android.content.DialogInterface;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageInfo;
import android.graphics.Color;  
import android.graphics.drawable.ColorDrawable;  
import android.os.Bundle;  
  
public class AppCheck extends Activity {  
      
    private Context context;  
    //ListView控件
    private ListView mList;
    //ListView数据源
    private ArrayList<AppInfo> data;
    private Map<String,Boolean>mp;
    /*
     * data 从mp和applist合成
     * mp从文件中读取。来存储应用的状态
     * 之后可以删除掉applist
     * 
     */
  
    
    
    @Override  
    protected void onCreate(Bundle savedInstanceState) {  
        super.onCreate(savedInstanceState);  
          
        context=this;
        setContentView(R.layout.appcheck);  
  
        
        mList = (ListView)findViewById(R.id.mList);
        data = new ArrayList<AppInfo>(); 
        List<PackageInfo> packages = getPackageManager().getInstalledPackages(0);
        
        for(int i=0;i<packages.size();i++)
        { 
	        PackageInfo packageInfo = packages.get(i);
	        
	        if((packageInfo.applicationInfo.flags&ApplicationInfo.FLAG_SYSTEM)==0)
	        {
	        	AppInfo tmpInfo = new AppInfo(); 
		        tmpInfo.appName = packageInfo.applicationInfo.loadLabel(getPackageManager()).toString(); 
		        tmpInfo.packageName = packageInfo.packageName; 
		        tmpInfo.appIcon = packageInfo.applicationInfo.loadIcon(getPackageManager());
		        tmpInfo.used = false;
		        data.add(tmpInfo);
	        }
        }
        
        
        final AppInfoAdapter adapter = new AppInfoAdapter(AppCheck.this,R.layout.appinfo,data);
        mList.setAdapter(adapter);
        mList.setOnItemClickListener(
            	new OnItemClickListener(){
            	@Override
            	public void onItemClick(AdapterView<?> parent, View view,int position, long id)
            	{
            		
            		AppInfo pn = data.get(position);
            		pn.Changeused();
            		CheckSwitchButton t = (CheckSwitchButton)view.findViewById(R.id.mCheckSwithcButton);
            		t.performClick();
            		
            		if(pn.getused()){
            			Toast.makeText(AppCheck.this, "Open", Toast.LENGTH_SHORT).show();
            		}
            		else
            		{
            			Toast.makeText(AppCheck.this, "Close", Toast.LENGTH_SHORT).show();
            		}
            		
            	}
            });	
  
     
        
    }  
  
}  