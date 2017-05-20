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
import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.HttpURLConnection;
import java.net.InetAddress;
import java.net.InetSocketAddress;
import java.net.Socket;
import java.net.SocketException;
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
import android.text.Editable;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.view.Window;
import android.view.View.OnClickListener;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

public class Play extends Activity implements OnClickListener{  


	
	String user_id;
	// id 
	String sta;
	EditText input;
	Button add;
	int now = 1;
	RecordAdapter adapter;
	private Context context;  
	
	static int pv = 5;
	
	
	private static String IpAddress = "192.168.191.1";  
    private static int Port = 9876; 
    
    
    Socket socket = null;  
    String buffer = "";  
    TextView txt1;  
    Button send,link;  
    EditText ed1;  
    String geted1;  
    
    
    
    private SensorManager sensorManager;
    private Sensor magneticSensor;
    private Sensor accelerometerSensor;
    private Sensor gyroscopeSensor;
    SensorEventListener listener;
    private static final float NS2S = 1.0f / 1000000000.0f;
    private float timestamp;
    private float angle[] =new float[3];
    private static int TT = 6000; 
    
    
    
    
    public Handler myHandler = new Handler() 
    {  
        @Override  
        public void handleMessage(Message msg) 
        {  
            if (msg.what == 0x11) {  
                Bundle bundle = msg.getData();  
                txt1.append("server:"+bundle.getString("msg")+"\n");  
            }  
        }  
    };  
    
    @Override  
	 public void onPause() {
	        super.onPause();  
	        sensorManager.unregisterListener(listener);
	 }  


    
    
    public void onCreate(Bundle savedInstanceState){  
        super.onCreate(savedInstanceState);
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        setContentView(R.layout.play);
        
        
      //  user_id = (String) ((TextView)findViewById(R.id.myid)).getText();
    
	
        
        
       // Init();
        
        
        txt1 = (TextView) findViewById(R.id.txt1);  
        send = (Button) findViewById(R.id.send);  
        link = (Button) findViewById(R.id.link);  
        ed1 = (EditText) findViewById(R.id.ed1); 
        
        sensorManager = (SensorManager) getSystemService(Context.SENSOR_SERVICE);
        magneticSensor = sensorManager.getDefaultSensor(Sensor.TYPE_MAGNETIC_FIELD);
        accelerometerSensor = sensorManager.getDefaultSensor(Sensor.TYPE_ACCELEROMETER);
        gyroscopeSensor = sensorManager.getDefaultSensor(Sensor.TYPE_GYROSCOPE);
        
   

        
        listener = new SensorEventListener()
        {
       // 	 NumberFormat decimalFormat=new DecimalFormat(");//构造方法的字符格式这里如果小数不足2位,会以0补足.
        	
        	@Override
        	public void onAccuracyChanged(Sensor sensor,int accuracy)
        	{
        		
        	}
        	@Override
        	public void onSensorChanged(SensorEvent event)
        	{
        		if (event.sensor.getType() == Sensor.TYPE_ACCELEROMETER)
        		{
	        			
	        			float x = event.values[0];
	        			float y = event.values[1];
	        			float z = event.values[2];
	        			
	        			String str = String.valueOf(event.values[0]) + " " + 
	        					String.valueOf(event.values[1]) + " " + 
	        					String.valueOf(event.values[2]);
	        			
	        			if(pv>=1){
	        				MyThreadSend th = new MyThreadSend(str);
	        				th.start();
	        				pv--;
	        			}
	        		
	                     
	                    
	                    
	        			float a = (float) Math.sqrt(x * x + y * y + z * z);
	        			//txt1.setText("x---------->" + x + "\ny-------------->" + y + "\nz----------->" + z);
	        	}
        		else if (event.sensor.getType() == Sensor.TYPE_MAGNETIC_FIELD) 
        		{
	        			
	        			float x = event.values[0];
	        			float y = event.values[1];
	        			float z = event.values[2];
	        		
	        			String str = "@"+String.valueOf(event.values[0]) + " " + 
	        					String.valueOf(event.values[1]) + " " + 
	        					String.valueOf(event.values[2]);
	        			
	        			if(pv>=1){
	        				MyThreadSend th = new MyThreadSend(str);
	        				th.start();
	        				pv--;
	        			}
	
	        	}
	        	else if (event.sensor.getType() == Sensor.TYPE_GYROSCOPE) 
	        	{
	        			
	        		if(timestamp != 0){ 
		        		
			        	final float dT = (event.timestamp -timestamp) * NS2S;
			        			
			        	
			        	
	        			
	        			
				
	        		
	        		}
	        			timestamp = event.timestamp;

        		}
        	}
        };
       
        
      
        
        		
      
      
        send.setOnClickListener(new OnClickListener()
        {  
            @Override  
            public void onClick(View v) {  
                geted1 = ed1.getText().toString();  
                txt1.append("client:"+"我开始发送坐标了"+"\n");  
                //启动线程 向服务器发送和接收信息  
                if(now==1){
                	now++;
                //	new MyThreadRev().start();
                }
                sensorManager.registerListener(listener,gyroscopeSensor,
        				TT);
        		sensorManager.registerListener(listener,magneticSensor,
        				TT);
        		sensorManager.registerListener(listener,accelerometerSensor,
        				TT);
            }  
        });  
        
        
        link.setOnClickListener(new OnClickListener()
        {  
            @Override  
            public void onClick(View v) {  
               IpAddress = String.valueOf(ed1.getText());
               
            }  
        });  
        
    }
      
    
    
    
  
    
    

    
    
	
	
   
    
    
    
    class MyThreadSend extends Thread {  
  	  
        public String txt1;  
  
        public MyThreadSend(String str) {  
            txt1 = str;  
        }  
   
        @Override  
        public void run() {  
            //定义消息  
            Message msg = new Message();  
            msg.what = 0x11;  
            Bundle bundle = new Bundle();  
            bundle.clear();  
            DatagramSocket socket;  
            try {  
                    //创建DatagramSocket对象并指定一个端口号，注意，如果客户端需要接收服务器的返回数据,  
                    //还需要使用这个端口号来receive，所以一定要记住  
                    socket = new DatagramSocket();  
                    //使用InetAddress(Inet4Address).getByName把IP地址转换为网络地址    
                    InetAddress serverAddress = InetAddress.getByName(IpAddress);  
                    //Inet4Address serverAddress = (Inet4Address) Inet4Address.getByName("192.168.1.32");    
                    String str =  txt1;//设置要发送的报文    
                    byte data[] = str.getBytes();//把字符串str字符串转换为字节数组    
                    //创建一个DatagramPacket对象，用于发送数据。    
                    //参数一：要发送的数据  参数二：数据的长度  参数三：服务端的网络地址  参数四：服务器端端口号   
                    DatagramPacket packet = new DatagramPacket(data, data.length ,serverAddress ,9876);    
                    socket.send(packet);//把数据发送到服务端。    
                    socket.close();
                    pv++;
                } catch (SocketException e) {  
                    e.printStackTrace();  
                } catch (UnknownHostException e) {  
                    e.printStackTrace();  
                } catch (IOException e) {  
                    e.printStackTrace();  
             }    
                
        }  
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
	@Override
	public void onClick(View v) {
		// TODO Auto-generated method stub
		
	}  
}  