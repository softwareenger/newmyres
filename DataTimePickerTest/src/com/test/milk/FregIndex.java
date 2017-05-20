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
import java.util.Calendar;
import java.util.Iterator;
import java.util.List;

import org.xmlpull.v1.XmlPullParser;
import org.xmlpull.v1.XmlPullParserFactory;

import com.test.milk.R;
import com.fourmob.datetimepicker.date.DatePickerDialog.OnDateSetListener;
import com.sleepbot.datetimepicker.time.TimePickerDialog;


import android.app.Activity;
import android.app.AlertDialog;
import android.support.v4.app.Fragment; 
import android.app.KeyguardManager;
import android.app.KeyguardManager.KeyguardLock;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.DialogInterface;
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
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.RelativeLayout;
import android.widget.RelativeLayout.LayoutParams;
import android.widget.TextView;
import android.widget.Toast;
import com.fourmob.datetimepicker.date.DatePickerDialog;
import com.fourmob.datetimepicker.date.DatePickerDialog.OnDateSetListener;
import com.sleepbot.datetimepicker.time.RadialPickerLayout;
import com.sleepbot.datetimepicker.time.TimePickerDialog;
import com.sleepbot.datetimepicker.time.TimePickerDialog.OnTimeSetListener;

public class FregIndex extends Fragment implements OnDateSetListener, TimePickerDialog.OnTimeSetListener , OnClickListener  {  

	  
		public static final String TIMEPICKER_TAG = "timepicker";
		String id;
		String sta;
	    
		Button Ban,Statue;
		
		Button TimeButtonL;
		Button TimeButtonR;
		Button clr;
		Button define;
		CheckBox check1;
		CheckBox check2;
		CheckBox check3;
		Calendar calendar;
		TimePickerDialog timePickerDialog;
		int mark;
		int LL,RR;
	
		@Override
		public View onCreateView(LayoutInflater inflater, ViewGroup container,
				Bundle savedInstanceState) {
			id = (String) ((TextView)getActivity().findViewById(R.id.myid)).getText();
			save(id,"TNOWID");
			return inflater.inflate(R.layout.fregindex, container, false);
		}
		@Override
		public void onActivityCreated(Bundle savedInstanceState)
	    {  

			super.onActivityCreated(savedInstanceState);
	       
	
	        Ban = (Button) getActivity().findViewById(R.id.Ban);
	        Statue = (Button) getActivity().findViewById(R.id.Statue);
	        check1 = (CheckBox) getActivity().findViewById(R.id.check1);
	        check1.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
	            @Override
	            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
	            	if(isChecked){save("1",id+"checkphone");}
	            	else{save("0",id+"checkphone");}
	            }
	        });
	        check2 = (CheckBox) getActivity().findViewById(R.id.check2);
	        check2.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
	            @Override
	            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
	            	if(isChecked){save("1",id+"checkmessage");}
	            	else{save("0",id+"checkmessage");}
	            	
	            }
	        });
	        check3 = (CheckBox) getActivity().findViewById(R.id.check3);
	        check3.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
	            @Override
	            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
	       
	            	if(isChecked){Log.w("APP","1");save("1",id+"checkapp");}
	            	else{save("0",id+"checkapp");}
	            } 
	        });
	        Ban.setOnClickListener(this);
	        Statue.setOnClickListener(this);
	        
	        
	        Init();

	        
	        clr = (Button) getActivity().findViewById(R.id.Clr);
	        TimeButtonL = (Button)getActivity().findViewById(R.id.LTime);
	        TimeButtonR = (Button)getActivity().findViewById(R.id.RTime);
	        /*
	        Ban.setText("Start");
	        Statue.setText("Statue");
	        TimeButtonL.setText("LTime");
	        TimeButtonR.setText("RTime");
	        clr.setText("Clear");
	        */
	       	TimeButtonL.setOnClickListener(new OnClickListener() {
	            @Override
	            public void onClick(View v) {
	            	mark = 1;
	                timePickerDialog.setVibrate(true);
	                timePickerDialog.setCloseOnSingleTapMinute(false);
	                timePickerDialog.show(getFragmentManager(), TIMEPICKER_TAG);
	            /*    
	                LayoutParams params = (RelativeLayout.LayoutParams)TimeButtonL.getLayoutParams();
	                params.addRule(RelativeLayout.ALIGN_PARENT_LEFT);
	                params.addRule(RelativeLayout.ALIGN_TOP, R.id.RTime);
	                params.setMargins(45, 48, 0, 0);  
	             
	                TimeButtonL.setLayoutParams(params); //使layout更新
	             */   
	                
	            }
	        });
	       	TimeButtonR.setOnClickListener(new OnClickListener() {
	            @Override
	            public void onClick(View v) {
	            	mark = 2;
	                timePickerDialog.setVibrate(true);
	                timePickerDialog.setCloseOnSingleTapMinute(false);
	                timePickerDialog.show(getFragmentManager(), TIMEPICKER_TAG);
	                LayoutParams params = (RelativeLayout.LayoutParams)TimeButtonR.getLayoutParams();
	               /*
	                params.addRule(RelativeLayout.BELOW,R.id.Clr);
	                params.addRule(RelativeLayout.RIGHT_OF, R.id.LTime);
	                params.setMargins(0, 16, 0, 0);  
	                		
	                TimeButtonR.setLayoutParams(params); //使layout更新
	                TimeButtonR.setBackgroundResource(R.drawable.mygs);
	            	*/
	            }
	        });
	        clr.setOnClickListener(this);
	        define  = (Button) getActivity().findViewById(R.id.Define);
	        define.setOnClickListener(this);
	        
	        
	        calendar = Calendar.getInstance();

	        timePickerDialog = TimePickerDialog.newInstance(this, 
	        		calendar.get(Calendar.HOUR_OF_DAY) ,
	        		calendar.get(Calendar.MINUTE), 
	        		false, false);
	        

	        
	        if (savedInstanceState != null) {

	            TimePickerDialog tpd = (TimePickerDialog) getFragmentManager().findFragmentByTag(TIMEPICKER_TAG);
	            if (tpd != null) {
	                tpd.setOnTimeSetListener(this);
	            }
	        }
	        
	        
	        
	    }
		@Override
		public void onClick(View v) {
			Intent intent;
			LayoutParams params;
			switch(v.getId())
			{	
				
				case R.id.Statue:
					/*
        		    params = (RelativeLayout.LayoutParams)Statue.getLayoutParams();
	                params.addRule(RelativeLayout.ALIGN_LEFT,R.id.LTime);
	                params.addRule(RelativeLayout.ALIGN_TOP,R.id.Define);
	                params.setMargins(0, 31, 0, 0);  
	                Statue.setLayoutParams(params); //使layout更新
	                */
					AlertDialog.Builder dialog = new AlertDialog.Builder(getActivity());
	        		dialog.setTitle("Statue");
	        		dialog.setMessage( solve( sta ) );
	        		dialog.setCancelable(false);
	        		dialog.setNegativeButton("ok", new DialogInterface.OnClickListener() 
	        		{
						@Override
							public void onClick(DialogInterface arg0, int arg1){
							   /*
								LayoutParams params = (RelativeLayout.LayoutParams)Statue.getLayoutParams();
				                params.addRule(RelativeLayout.ALIGN_LEFT,R.id.LTime);
				                params.addRule(RelativeLayout.ALIGN_TOP,R.id.Define);
				                params.setMargins(0, 31, 0, 0);  
				                Statue.setLayoutParams(params); //使layout更新
								*/
							}
						}
	        		);
	        		dialog.show();
	        	
		                
		                
	        		break;
	        		
			
				case R.id.Ban:	
					
					
					intent = new Intent(getActivity(),Ban.class);
					startActivity(intent);
					
					break;
				
				case R.id.Clr:
					sta = "";
					for(int i=0;i<=1439;i++)
					{
						sta += "0";
						
					}
					save(sta,id+"run");
					/*
					    params = (RelativeLayout.LayoutParams)clr.getLayoutParams();
		                params.addRule(RelativeLayout.ALIGN_LEFT,R.id.Define);
		                params.addRule(RelativeLayout.BELOW,R.id.Define);
		                params.setMargins(24, 0, 0, 0);  
		                clr.setLayoutParams(params); //使layout更新
		              */  
					break;
				case R.id.Define:
					StringBuilder strb = new StringBuilder(sta);
					for(int i=LL;i<=RR;i++)
					{
						strb.setCharAt(i,'1');
					}
					sta = strb.toString();
					save(sta,id+"run");
					/*
				    params = (RelativeLayout.LayoutParams)define.getLayoutParams();
	                params.addRule(RelativeLayout.ALIGN_LEFT,R.id.RTime);
	                params.addRule(RelativeLayout.BELOW,R.id.Ban);
	                
	                params.setMargins(0, 16, 0, 0);  
	                	
	                define.setLayoutParams(params); //使layout更新
	                define.setBackgroundResource(R.drawable.myds);
					 */	
					break;
				
			}
		}  
		
			
		  private boolean isVibrate() {
		        return true;
		    }

		    private boolean isCloseOnSingleTapDay() {
		    	return true;
		    }

		    private boolean isCloseOnSingleTapMinute() {
		    	return true;
		    }

	    
		   @Override
		    public void onDateSet(DatePickerDialog datePickerDialog, int year, int month, int day) {
		        Toast.makeText(getActivity(), "new date:" + year + "-" + month + "-" + day, Toast.LENGTH_LONG).show();
		    }

		    
		    @Override
		    public void onTimeSet(RadialPickerLayout view, int hourOfDay, int minute)
		    {
		    	int h = hourOfDay,m = minute;
		    	if(mark==1)
		    	{
		    		LL = h*60+m;
		    	}
		    	else
		    	{
		    		RR = h*60+m;
		    	}
		        
		    }
		
		String trans(int a)
		{
			int hour = a/60;
			int minute = a%60;
			return String.valueOf(hour)+":"+String.valueOf(minute);
		}
		String solve(String sr)
		{
			String ans = "";
			int i;
			int t = 0,s = 0,e = 0;
			for(i=0;i<sr.length();i++)
			{
				if(sr.charAt(i)=='1')
				{
					if(t==0)
					{
						s = i;
					}
					t = 1;
				}
				else
				{
					if(t==1)
					{
						e = i-1;
						ans += "from "+trans(s)+" to "+trans(e)+"\n"; 
					}
					t = 0;
				}
			}
			if(t==1)
			{
				e = 1439;
				ans += "from "+trans(s)+" to "+trans(e)+"\n"; 
			}
			if(ans.equals("")){ans="状态还没有设置呢";}
			return ans;
		}
		boolean judge(String temp)
		{
			for(int i=0;i<temp.length();i++)
			{
				if( temp.charAt(i) < '0' || temp.charAt(i) > '9')
				{
					return false;
				}
			}
			return true;
		}

		void Init()
		{
			if(load(id+"checkphone").equals("1")){check1.setChecked(true);}
	 		else{check1.setChecked(false);}
			if(load(id+"checkmessage").equals("1")){check2.setChecked(true);}
	 		else{check2.setChecked(false);}
			if(load(id+"checkapp").equals("1")){check3.setChecked(true);}
	 		else{check3.setChecked(false);}
			mark = 1;
			sta = load(id+"run");
	        if(sta=="")
	        {
	        	int i;
	        	for(i=0;i<1440;i++)
	        	{
	        		sta += "0";
	        	}
	        	save(sta,id+"run");
	        }
		}
		void over()
		{
			save(sta,id+"run");
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
