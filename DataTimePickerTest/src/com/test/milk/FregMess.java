package com.test.milk;
import java.io.BufferedReader;

import java.io.BufferedWriter;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.io.StringReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

import org.xmlpull.v1.XmlPullParser;
import org.xmlpull.v1.XmlPullParserFactory;


import android.app.Activity;
import android.app.KeyguardManager;
import android.app.KeyguardManager.KeyguardLock;
import android.support.v4.app.*;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
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

public class FregMess extends Fragment implements OnClickListener{

	
	String id;
	Button sure;
	EditText message;
	//框框里的内容
	String mess;
	//当前保存的信息是什么
	
	
	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		id = (String) ((TextView)getActivity().findViewById(R.id.myid)).getText();
		
		return inflater.inflate(R.layout.fregmess, container, false);
	}
	@Override
	public void onActivityCreated(Bundle savedInstanceState)
    {
	    super.onCreate(savedInstanceState);  
        
       
	  
        Init();
        message = (EditText) getActivity().findViewById(R.id.Message);
        sure = (Button) getActivity().findViewById(R.id.Sure);
        sure.setOnClickListener(this);		
        message.setText(mess);
    }
	@Override
	public void onClick(View v) {
		switch(v.getId())
		{
			case R.id.Sure:
				mess = message.getText().toString();
				if(mess.equals(""))
				{
					Toast.makeText(getActivity(), "输入为空代表不发送哦", Toast.LENGTH_SHORT).show();
				}
				save(mess,id+"shortmessage");
				Toast.makeText(getActivity(), "Set success", Toast.LENGTH_SHORT).show();
				break;
		}
	}  

	void Init()
	{
		mess = load(id+"shortmessage");
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
