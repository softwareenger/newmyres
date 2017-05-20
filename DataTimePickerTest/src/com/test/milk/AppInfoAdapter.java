
package com.test.milk;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;

import com.example.compoundbuttonview.view.CheckSwitchButton;

import android.content.Context;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.BaseAdapter;
import android.widget.CompoundButton;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;
import android.widget.CompoundButton.OnCheckedChangeListener;

public class AppInfoAdapter extends BaseAdapter{

	 private ArrayList<AppInfo> data;  
	 private LayoutInflater layoutInflater;  
	 private Context context;  
	 
	public AppInfoAdapter(Context context, int textViewResourceId,ArrayList<AppInfo> data) 
	{
		this.context=context;  
        this.data=data;  
        this.layoutInflater=LayoutInflater.from(context);  
	}
	 public final class Zujian{  
	        public ImageView app_icon;  
	        public TextView app_name;  
	        public CheckSwitchButton mCheckSwithcButton;
	    }  
	  @Override  
	  public int getCount() {  
	       return data.size();  
	  }  
	  @Override  
	    public Object getItem(int position) {  
	        return data.get(position);  
	    }  
	  @Override  
	    public long getItemId(int position) {  
	        return position;  
	    }  
	  
	    @Override  
	    public View getView(int position, View convertView, ViewGroup parent) {  
	        Zujian zujian=null;  
	        if(convertView==null){  
	            zujian=new Zujian();  
	            //获得组件，实例化组件  
	            convertView=layoutInflater.inflate(R.layout.appinfo, null);  
	            zujian.app_icon=(ImageView)convertView.findViewById(R.id.app_iconx);  
	            zujian.app_name=(TextView)convertView.findViewById(R.id.app_namex);  
	            zujian.mCheckSwithcButton=(CheckSwitchButton)convertView.findViewById(R.id.mCheckSwithcButton);  
	            convertView.setTag(zujian);  
	        }else{  
	            zujian=(Zujian)convertView.getTag();  
	        }  
	        //绑定数据  
	        zujian.app_icon.setImageDrawable(data.get(position).getappIcon() );  
	        zujian.app_name.setText(data.get(position).getappName());
	        zujian.mCheckSwithcButton.setChecked(data.get(position).getused());
	      //  zujian.mCheckSwithcButton.setId(position);
	        
	
			zujian.mCheckSwithcButton.setEnabled(false);
			
			
			

	        return convertView;  
	    } 
}
