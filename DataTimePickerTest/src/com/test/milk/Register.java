package com.test.milk;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.StringReader;
import java.net.HttpURLConnection;
import java.net.URL;

import org.xmlpull.v1.XmlPullParser;
import org.xmlpull.v1.XmlPullParserFactory;

import android.app.Activity;
import android.app.KeyguardManager;
import android.app.KeyguardManager.KeyguardLock;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.Window;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;
public class Register extends Activity implements OnClickListener{  
	  
	Button re;
	EditText pas;
	EditText id;
	
	
    public void onCreate(Bundle savedInstanceState){  
        super.onCreate(savedInstanceState); 
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        setContentView(R.layout.register);
        id = (EditText) findViewById(R.id.id_input);
        pas = (EditText) findViewById(R.id.password_input);
        re = (Button) findViewById(R.id.register);
        re.setOnClickListener(this);
        
    }
	@Override
	public void onClick(View v) {
		switch(v.getId())
		{
			case R.id.register:
				String id_input = id.getText().toString();
				String password_input = pas.getText().toString();
				sendRequestWithHttpURLConnection("http://10.0.2.2:8080/webof/user_add.jsp?id="+
				id_input+
				"&password="+
				password_input
				);
				break;
		}
	}  
	 

	  private void sendRequestWithHttpURLConnection(final String address) 
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
			    	message.what = 1;
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
					res = parseXMLWithPull(tem);
					if(res.equals("no"))
					{
						Toast.makeText(Register.this, "fail", Toast.LENGTH_SHORT).show();
					}
					else
					{
						Toast.makeText(Register.this, "success", Toast.LENGTH_SHORT).show();
					}
				default:
					break;
				}
			}
		};
	   private String parseXMLWithPull(String xmlData) 
	   {
		    String sta = "";
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
					    	if ("statue".equals(nodeName)) 
					    	{
					    		sta = xmlPullParser.nextText();
					    	} 
					    	break;
				    	}
				    	// 完成解析某个结点
				    	case XmlPullParser.END_TAG:
				    	{
					    	if ("app".equals(nodeName)) 
					    	{
					    		return sta;
						    }
					    	break;
				    	}
				    	default:
				    	break;
				    	
			    	}
			    	eventType = xmlPullParser.next();
			    }
			   
		    } 
	    	catch (Exception e) {
		    	e.printStackTrace();
	    	}
			return sta;
	    }
	    
}  
