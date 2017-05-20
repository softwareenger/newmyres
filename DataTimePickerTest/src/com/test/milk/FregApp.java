package com.test.milk;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

import com.example.compoundbuttonview.view.CheckSwitchButton;

import android.content.Context;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageInfo;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;
import android.widget.AdapterView.OnItemClickListener;

public class FregApp extends Fragment{

	String id;
    private Context context;  
    //ListView控件
    private ListView mList;
    //ListView数据源
    private ArrayList<AppInfo> data;
    private ArrayList<String>mp;
    /*
     * data 从mp和applist合成
     * mp从文件中读取。来存储应用的状态
     * 
     */
	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,Bundle savedInstanceState) {
		
		id = (String) ((TextView)getActivity().findViewById(R.id.myid)).getText();
		return inflater.inflate(R.layout.fregapp, container, false);
	}
	 @Override  
	 public void onPause() {
		 	Log.w("PAUSE","YES");
		 	over();
	        super.onPause();  
	        
	 }  
	@Override
	public void onActivityCreated(Bundle savedInstanceState)
    {
		  super.onCreate(savedInstanceState);  
           Log.w("ONACTIVITY","YES");
	        context=getActivity();
	        data = new ArrayList<AppInfo>(); 
	        mp = new ArrayList<String>(); 
	        Init();
	        mList = (ListView)getActivity().findViewById(R.id.mList);
	       
	        List<PackageInfo> packages = getActivity().getPackageManager().getInstalledPackages(0);
	        
	        for(int i=0;i<packages.size();i++)
	        { 
		        PackageInfo packageInfo = packages.get(i);
		        
		        if((packageInfo.applicationInfo.flags&ApplicationInfo.FLAG_SYSTEM)==0)
		        {
		        	AppInfo tmpInfo = new AppInfo(); 
			        tmpInfo.appName = packageInfo.applicationInfo.loadLabel(getActivity().getPackageManager()).toString(); 
			        tmpInfo.packageName = packageInfo.packageName; 
			        tmpInfo.appIcon = packageInfo.applicationInfo.loadIcon(getActivity().getPackageManager());
			        tmpInfo.used = false;
			        data.add(tmpInfo);
		        }
	        }
	        
	        
		    Iterator<String> it=mp.iterator();
		    while(it.hasNext())
		    {
		    	String temp = it.next();
		    	Iterator<AppInfo> itd = data.iterator();
		    	while(itd.hasNext())
		    	{
		    		AppInfo td = itd.next();
		    		if(temp.equals( td.getappName() ))
		    		{
		    			td.used = true;
		    		
		    		}
		    	}
		    }
	        
	        final AppInfoAdapter adapter = new AppInfoAdapter(getActivity(),R.layout.appinfo,data);
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
	            			Toast.makeText(getActivity(), "Open", Toast.LENGTH_SHORT).show();
	            		}
	            		else
	            		{
	            			Toast.makeText(getActivity(), "Close", Toast.LENGTH_SHORT).show();
	            		}
	            		
	            	}
	            });	
    }
	String [] input = new String[100];
	void Init()
	{
		String str = load(id+"myapp");
		if(str==""){mp.clear();return;}
		input = str.split(" ");	
		for(int i=0;i<input.length;i++)
		{
			mp.add(new String(input[i]));
		}
	}
	void over()
	{
		String str1 ="";
		String str2 ="";
	    int i;
	    Iterator<AppInfo> it=data.iterator();
	    while(it.hasNext())
	    {
	    	AppInfo td = it.next();
	    	if(td.getused())
	    	{
	    		str1 += td.getappName()+" ";
	    	}
	    	else
	    	{
	    		str2 += td.getpackageName()+" ";
	    	}
	    }
	    save(str1,id+"myapp");
	    save(str2,id+"package");
	    
	    
	    
	    
	}
	public void save(String inputText,String file) {
		FileOutputStream out = null;
		BufferedWriter writer = null;
		try {
			out = getActivity().openFileOutput(file, Context.MODE_PRIVATE);
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
	
	public String load(String file) {
		FileInputStream in = null;
		BufferedReader reader = null;
		StringBuilder content = new StringBuilder();
		try {
			in = getActivity().openFileInput(file);
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
