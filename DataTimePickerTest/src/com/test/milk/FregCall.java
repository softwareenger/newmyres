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

import android.content.Context;
import android.content.Intent;
import android.graphics.Color;
import android.graphics.drawable.ColorDrawable;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.Window;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.baoyz.swipemenulistview.SwipeMenu;
import com.baoyz.swipemenulistview.SwipeMenuCreator;
import com.baoyz.swipemenulistview.SwipeMenuItem;
import com.baoyz.swipemenulistview.SwipeMenuListView;
import com.fourmob.datetimepicker.date.DatePickerDialog.OnDateSetListener;
import com.sleepbot.datetimepicker.time.TimePickerDialog;

public class FregCall extends Fragment   {

	String id;
	EditText phone_input;
	TextView t;
	Button add;
	int time = 1;
	private Context context;  
	
	private List<PhoneNumber> pnl = new ArrayList<PhoneNumber>();
	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,Bundle savedInstanceState) {
		
		id = (String) ((TextView)getActivity().findViewById(R.id.myid)).getText();
		
		return inflater.inflate(R.layout.fregcall, container, false);
		
	}
	 @Override  
	 public void onPause() {
		 	over();
	        super.onPause();  
	        
	 }  
	@Override
	public void onActivityCreated(Bundle savedInstanceState)
    {
	     super.onCreate(savedInstanceState);
	        context=getActivity();
	    
	        if(time==1){Init();time++;}
	        
	        phone_input = (EditText) getActivity().findViewById(R.id.phone_input);
	        add = (Button) getActivity().findViewById(R.id.Add);
	        
	        
	        final PhoneNumberAdapter adapter = new PhoneNumberAdapter(getActivity(),
	        		R.layout.phonenumber, pnl);
	        
	        
	        
	        
	        SwipeMenuCreator creator = new SwipeMenuCreator() {	  
	        	  
	            @Override  
	            public void create(SwipeMenu menu)
	            {  
	                 /* 
	                SwipeMenuItem openItem = new SwipeMenuItem(context);  
	                openItem.setBackground(new ColorDrawable(Color.LTGRAY));  
	                openItem.setWidth(dp2px(80));  
	                openItem.setTitle("Open");  
	                openItem.setTitleSize( 20 );  
	                openItem.setTitleColor(Color.WHITE);  
	                menu.addMenuItem(openItem);               
	                  */
	                SwipeMenuItem deleteItem = new SwipeMenuItem(context);  
	                deleteItem.setBackground(new ColorDrawable(Color.RED));  
	                deleteItem.setWidth(dp2px(80));  
	             
	                deleteItem.setIcon(R.drawable.trash);  
	                menu.addMenuItem(deleteItem);  
	            }  
	        };  
	        
	        SwipeMenuListView listView = (SwipeMenuListView) getActivity().findViewById(R.id.listView);  
	        listView.setMenuCreator(creator);  
	  
	        listView.setOnMenuItemClickListener(
	        	new SwipeMenuListView.OnMenuItemClickListener() {  
	            @Override  
	            public boolean onMenuItemClick(int position, SwipeMenu menu,int index) {  
	                //index的值就是在SwipeMenu依次添加SwipeMenuItem顺序值，类似数组的下标。  
	                //从0开始，依次是：0、1、2、3...  
	                switch (index) 
	                {  
		                case 0:  
		                	pnl.remove(position);
		                	adapter.notifyDataSetChanged();	
		                    break;
		              
	                }  
	  
	                // false : 当用户触发其他地方的屏幕时候，自动收起菜单。  
	                // true : 不改变已经打开菜单的样式，保持原样不收起。  
	                return false;  
	            }  
	        });  

	        
	        

	        
	      	
	        listView.setAdapter(adapter);
	        
	        
	        
	        add.setOnClickListener(
	        		new OnClickListener() {
						@Override
						public void onClick(View v) {
							String temp = phone_input.getText().toString();
							boolean flag = false;
							Iterator<PhoneNumber> it=pnl.iterator();
							while(it.hasNext())
							{
								if(temp.equals(it.next().getn()))
								{
									flag = true;
								}	
							}
							if(flag)
							{
								Toast.makeText(getActivity(), "重复输入没有用哦！", Toast.LENGTH_SHORT).show();
							}
							else
							{
								if(temp.equals("")){Toast.makeText(getActivity(), "设置为空了！", Toast.LENGTH_SHORT).show();}
								else{
									pnl.add(new PhoneNumber(temp));
									adapter.notifyDataSetChanged();	
								}
							}
						}
	        		}
	        );
    }
	   public int dp2px(float dipValue) {  
	        final float scale = this.getResources().getDisplayMetrics().density;  
	        return (int) (dipValue * scale + 0.5f);  
	    }  
		
		String[] cur = new String[100];
		String curs;
		void Init()
		{
			curs = load(id+"curser");
			if(curs==""){pnl.clear();return;}
			cur = curs.split(" ");	
			for(int i=0;i<cur.length;i++)
			{
				pnl.add(new PhoneNumber(cur[i]));
			}
		}
		void over()
		{
			curs ="";
		    int i;
		    Iterator<PhoneNumber> it=pnl.iterator();
		    while(it.hasNext())
		    {
		    	curs += (it.next()).getn()+" ";
		    }
		    save(curs,id+"curser");
		}
		
	
		
		public void save(String inputText,String file)
		{
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
