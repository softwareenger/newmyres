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
import com.test.milk.Play.MyThreadSend;

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

public class Mouse extends Activity implements OnClickListener{  


	
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
    Button send,link,Lhit,Rhit;  
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
        setContentView(R.layout.mouse);
        
        
      //  user_id = (String) ((TextView)findViewById(R.id.myid)).getText();
    
	
        
        
       // Init();
        
        
        txt1 = (TextView) findViewById(R.id.txt1);  
        Lhit = (Button) findViewById(R.id.Lhit);  
        Rhit = (Button) findViewById(R.id.Rhit); 
        send = (Button) findViewById(R.id.send);  
        link = (Button) findViewById(R.id.link);  
        ed1 = (EditText) findViewById(R.id.ed1); 
        
        sensorManager = (SensorManager) getSystemService(Context.SENSOR_SERVICE);
        magneticSensor = sensorManager.getDefaultSensor(Sensor.TYPE_MAGNETIC_FIELD);
        accelerometerSensor = sensorManager.getDefaultSensor(Sensor.TYPE_ACCELEROMETER);
        gyroscopeSensor = sensorManager.getDefaultSensor(Sensor.TYPE_GYROSCOPE);
        
        Lhit.setOnClickListener(new OnClickListener()
        {  
            @Override  
            public void onClick(View v)
            {  
            	String str = "*";
        		if(pv>=1){
        			MyThreadSend th = new MyThreadSend(str);
        			th.start();
        			pv--;
        		}
            }  
        });  	
        Rhit.setOnClickListener(new OnClickListener()
        {  
            @Override  
            public void onClick(View v)
            {  
            	String str = "/";
        		if(pv>=1){
        			MyThreadSend th = new MyThreadSend(str);
        			th.start();
        			pv--;
        		}
            }  
        });  	

        
        listener = new SensorEventListener()
        {
       // 	 NumberFormat decimalFormat=new DecimalFormat(");//���췽�����ַ���ʽ�������С������2λ,����0����.
        	
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
	        			
	        			
	        		
	                    
	                    
	                    
	        			float a = (float) Math.sqrt(x * x + y * y + z * z);
	        			//txt1.setText("x---------->" + x + "\ny-------------->" + y + "\nz----------->" + z);
	        	}
        		else if (event.sensor.getType() == Sensor.TYPE_MAGNETIC_FIELD) 
        		{
	        			
	        			float x = event.values[0];
	        			float y = event.values[1];
	        			float z = event.values[2];
	        		
	        
	
	        	}
	        	else if (event.sensor.getType() == Sensor.TYPE_GYROSCOPE) 
	        	{
	        			
	        		if(timestamp != 0){ 
		        		
			        	final float dT = (event.timestamp -timestamp) * NS2S;
			        			
			        	
			        	String str = String.valueOf(event.values[1]) + " " + 
	        					String.valueOf(event.values[0]) + " ";
	        			
	        			if(pv>=1){
	        				MyThreadSend th = new MyThreadSend(str);
	        				th.start();
	        				pv--;
	        			}
	        			/*
				        angle[0] += event.values[0] * dT;
				        angle[1] += event.values[1] * dT;
				        angle[2] += event.values[2] * dT;
				        		
				        float anglex = (float) Math.toDegrees(angle[0]);
				        float angley = (float) Math.toDegrees(angle[1]);
				        float anglez = (float) Math.toDegrees(angle[2]);
	        			 */
	        			
				
	        		
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
                txt1.append("client:"+"�ҿ�ʼ����������"+"\n");  
                //�����߳� ����������ͺͽ�����Ϣ  
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
            //������Ϣ  
            Message msg = new Message();  
            msg.what = 0x11;  
            Bundle bundle = new Bundle();  
            bundle.clear();  
            DatagramSocket socket;  
            try {  
                    //����DatagramSocket����ָ��һ���˿ںţ�ע�⣬����ͻ�����Ҫ���շ������ķ�������,  
                    //����Ҫʹ������˿ں���receive������һ��Ҫ��ס  
                    socket = new DatagramSocket();  
                    //ʹ��InetAddress(Inet4Address).getByName��IP��ַת��Ϊ�����ַ    
                    InetAddress serverAddress = InetAddress.getByName(IpAddress);  
                    //Inet4Address serverAddress = (Inet4Address) Inet4Address.getByName("192.168.1.32");    
                    String str =  txt1;//����Ҫ���͵ı���    
                    byte data[] = str.getBytes();//���ַ���str�ַ���ת��Ϊ�ֽ�����    
                    //����һ��DatagramPacket�������ڷ������ݡ�    
                    //����һ��Ҫ���͵�����  �����������ݵĳ���  ������������˵������ַ  �����ģ��������˶˿ں�   
                    DatagramPacket packet = new DatagramPacket(data, data.length ,serverAddress ,9876);    
                    socket.send(packet);//�����ݷ��͵�����ˡ�    
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
	    	// �����߳���������������
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
			    	// ����Ի�ȡ�������������ж�ȡ
			    	BufferedReader reader = new BufferedReader(new
			    	InputStreamReader(in));
			    	StringBuilder response = new StringBuilder();
			    	String line;
			    	while ((line = reader.readLine()) != null) {
			    	response.append(line);
			    	}
			    	Message message = new Message();
			    	message.what = show;
			    	// �����������صĽ����ŵ�Message��
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
				    	// ��ʼ����ĳ�����
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
				    	// ��ɽ���ĳ�����
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