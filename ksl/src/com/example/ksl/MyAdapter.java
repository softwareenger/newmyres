package com.example.ksl;

import java.util.ArrayList;
import java.util.List;


import com.example.compoundbuttonview.view.CheckSwitchButton;

import android.content.Context;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast; 

public class MyAdapter extends BaseAdapter implements View.OnClickListener {
    private Context context;
    private List<String> data;
    ArrayList<CheckSwitchButton>btn;
    public MyAdapter(List<String> data){
        this.data = data;
        btn = new ArrayList<CheckSwitchButton>();
    }
    @Override
    public int getCount() {
        return data == null ? 0 : data.size();
    }
    @Override
    public Object getItem(int i) {
        return data.get(i);
    }

    @Override
    public long getItemId(int i) {
        return i;
    }

    
    @Override
    public View getView(int i, View view, ViewGroup viewGroup) {
    		
        ViewHolder viewHolder = null;
        if(context == null)
            context = viewGroup.getContext();
        if(view == null){
            view = LayoutInflater.from(viewGroup.getContext()).inflate(R.layout.list_item,null);
            viewHolder = new ViewHolder();
            viewHolder.mTv = (TextView)view.findViewById(R.id.mTv);
            viewHolder.mBtn = (CheckSwitchButton)view.findViewById(R.id.mCheckSwithcButton);
            btn.add(viewHolder.mBtn);
            /*初始化按钮的状态*/
            view.setTag(viewHolder);
        }
        
        viewHolder = (ViewHolder)view.getTag();
        
        //设置tag标记
        viewHolder.mBtn.setTag(R.id.btn,i);//添加此代码
        viewHolder.mBtn.setChecked(false);
    
        view.setOnClickListener(myClickListener);
        view.setId(i);
        viewHolder.mTv.setText(data.get(i));
        
        //设置tag标记
        viewHolder.mTv.setTag(R.id.tv,i);//添加此代码
       
        return view;
}
    @Override
    public void onClick(View view) {
        switch (view.getId()){
          
        }
    }
    static class ViewHolder{
        TextView mTv;
        CheckSwitchButton  mBtn;
    }
    
    public OnClickListener myClickListener = new OnClickListener() 
    {
    	public void onClick(View v) 
    	{
    		int position = v.getId();
    		
    		btn.get(position).performClick();
    	
    	}
    };
}