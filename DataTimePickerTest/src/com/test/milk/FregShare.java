package com.test.milk;
import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.io.PrintWriter;
import java.io.StringReader;
import java.io.UnsupportedEncodingException;
import java.net.HttpURLConnection;
import java.net.InetSocketAddress;
import java.net.Socket;
import java.net.SocketTimeoutException;
import java.net.URL;
import java.net.URLEncoder;
import java.net.UnknownHostException;
import java.text.DecimalFormat;
import java.text.NumberFormat;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

import org.xmlpull.v1.XmlPullParser;
import org.xmlpull.v1.XmlPullParserFactory;

import com.baoyz.swipemenulistview.SwipeMenu;
import com.baoyz.swipemenulistview.SwipeMenuCreator;
import com.baoyz.swipemenulistview.SwipeMenuItem;
import com.baoyz.swipemenulistview.SwipeMenuListView;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.KeyguardManager;
import android.app.KeyguardManager.KeyguardLock;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.IntentFilter;
import android.graphics.Color;
import android.graphics.drawable.ColorDrawable;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.support.v4.app.Fragment; 
import android.util.Log;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.view.View.OnClickListener;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

public class FregShare extends Fragment{  


	
	String user_id;
	// id 
	String sta;
	EditText input;
	Button add;
	int now = 1;
	RecordAdapter adapter;
	private Context context;  
	
	Button Play;
	Button Mouse;
	Button Communicate;
	
   
	@Override  
	public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {  
		user_id = (String) ((TextView)getActivity().findViewById(R.id.myid)).getText();
		return inflater.inflate(R.layout.fregshare, container, false);  
	}  
	@Override
	public void onActivityCreated(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);  
        context = getActivity();
        
        
        Init();
        
        
        Play = (Button) getActivity().findViewById(R.id.Play); 
        Mouse = (Button) getActivity().findViewById(R.id.Mouse); 
        Communicate = (Button) getActivity().findViewById(R.id.Communicate); 
      
        Communicate.setOnClickListener(new OnClickListener() {  
        	  
            @Override  
            public void onClick(View v) {  
					Intent intent = new Intent(getActivity(),Communicate.class);
					startActivity(intent);
          
            }  
        });  
        
        Play.setOnClickListener(new OnClickListener() {  
  
            @Override  
            public void onClick(View v) {  
					Intent intent = new Intent(getActivity(),Play.class);
					startActivity(intent);
          
            }  
        });  
        
       Mouse.setOnClickListener(new OnClickListener() {  
        	  
            @Override  
            public void onClick(View v) {  
					Intent intent = new Intent(getActivity(),Mouse.class);
					startActivity(intent);
          
            }  
        }); 
    }
      
	
	
	
	
	
    
	

	void Init()
	{
		sta = load(user_id+"run");
	}
	void over()
	{
		save(sta,user_id+"run");
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
	
	
	
	
	  private void sendRequestWithHttpURLConnection(final String address,final int show) 
	  {
	    	// 开启线程来发起网络请求
	    	new Thread(new Runnable() {
		    	@Override
		    	public void run() {
		    	HttpURLConnection connection = null;
		    	try {
			    	URL url = new URL(address);
			    	connection = (HttpURLConnection) url.openConnection();
			    	connection.setRequestMethod("GET");
			    	connection.setConnectTimeout(8000);
			    	connection.setReadTimeout(8000);
			    	InputStream in = connection.getInputStream();
			    	// 下面对获取到的输入流进行读取
			    	BufferedReader reader = new BufferedReader(new
			    	InputStreamReader(in));
			    	StringBuilder response = new StringBuilder();
			    	String line;
			    	while ((line = reader.readLine()) != null) {
			    	response.append(line);
			    	}
			    	Message message = new Message();
			    	message.what = show;
			    	// 将服务器返回的结果存放到Message中
			    	message.obj = response.toString();
			    	handler.sendMessage(message);
			    	} catch (Exception e) {
			    	e.printStackTrace();
			    	} finally {
				    	if (connection != null) 
				    	{
				    		connection.disconnect();
				    	}
			    	}
		    	}
	    	}).start();
	    }
	  
		Handler handler = new Handler()
		{	
			public void handleMessage(Message msg)
			{
				String tem,res;
				switch(msg.what){
				case 1:
					tem = (String)msg.obj;
					parseXMLWithPull(tem);
					break;
				default:
					break;
				}
			}
		};
	   private void parseXMLWithPull(String xmlData) 
	   {
		    String id = "";
		    String display = "";
		    String conx = "";
	    	try 
	    	{
		    	XmlPullParserFactory factory = XmlPullParserFactory.newInstance();
		    	XmlPullParser xmlPullParser = factory.newPullParser();
		    	xmlPullParser.setInput(new StringReader(xmlData));
		    	int eventType = xmlPullParser.getEventType();
		    		    	
		    	
			    while (eventType != XmlPullParser.END_DOCUMENT) 
			    {
			    	String nodeName = xmlPullParser.getName();
			    	switch (eventType) {
				    	// 开始解析某个结点
				    	case XmlPullParser.START_TAG: {
					    	if ("id".equals(nodeName)) 
					    	{
					    		id = xmlPullParser.nextText();
					    	} 
					    	if ("display".equals(nodeName)) 
					    	{
					    		display = xmlPullParser.nextText();
					    	} 
					    	if ("conx".equals(nodeName)) 
					    	{
					    		conx = xmlPullParser.nextText();
					    	} 
					    	break;
				    	}
				    	// 完成解析某个结点
				    	case XmlPullParser.END_TAG:
				    	{
					    	if ("app".equals(nodeName)) 
					    	{
					    	//	pnl.add(new Record(id,display,conx));
						    }
					    	break;
				    	}
				    	default:
				    	break;
				    	
			    	}
			    	eventType = xmlPullParser.next();
			    }
			    adapter.notifyDataSetChanged();
			   
		    } 
	    	catch (Exception e) {
		    	e.printStackTrace();
	    	}
			
	    }
	   public int dp2px(float dipValue) {  
	        final float scale = this.getResources().getDisplayMetrics().density;  
	        return (int) (dipValue * scale + 0.5f);  
	    }  
}  