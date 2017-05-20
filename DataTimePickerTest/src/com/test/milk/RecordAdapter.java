package com.test.milk;

import java.util.List;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

public class RecordAdapter extends ArrayAdapter<Record>{

	private int resourceId;
	private List<Record> mItemList; 
	private LayoutInflater mInflater;
	private Context mContext;
	 
	public RecordAdapter(Context context, int textViewResourceId,List<Record> objects) 
	{
		super(context, textViewResourceId, objects);
		resourceId = textViewResourceId;
	}
	public void setItemList(List list) {
	        mItemList = list;
	}
	
			@Override
	public View getView(int position, View convertView, ViewGroup parent) {
				
		Record pn = getItem(position); // 获取当前项的Fruit实例
		View view;
		ViewHolder viewHolder;
		if(convertView==null){
			view = LayoutInflater.from(getContext()).inflate(resourceId, null);
			viewHolder = new ViewHolder();
			viewHolder.id = (TextView) view.findViewById(R.id.id);
			viewHolder.conx = (TextView) view.findViewById(R.id.conx);
			view.setTag(viewHolder); // 将ViewHolder存储在View中
		}
		else
		{
			view = convertView;
			viewHolder = (ViewHolder) view.getTag(); // 重新获取ViewHolder
		}
		viewHolder.id.setText(pn.getid());
		viewHolder.conx.setText(pn.getconx());
		
		
		return view;
	}
	class ViewHolder {
		TextView id;
		TextView conx;
	}

}
