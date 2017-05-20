package com.test.milk;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.content.Intent;
import android.os.Build;
import android.os.Bundle;
import android.support.v4.app.Fragment; 
import android.support.v4.app.FragmentActivity;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentTransaction;
import android.view.Display;
import android.view.View;
import android.view.Window;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.ImageButton;





import android.os.Bundle;
import android.provider.Settings;
import android.support.v4.app.FragmentActivity;
import android.support.v4.app.FragmentTabHost;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.ImageView;
import android.widget.TabHost.TabSpec;
import android.widget.TextView;


 public class Divide extends FragmentActivity {
	 	
	 	String id;
	 	TextView saveid;
		//定义FragmentTabHost对象
		private FragmentTabHost mTabHost;
		
		//定义一个布局
		private LayoutInflater layoutInflater;
			
		//定义数组来存放Fragment界面
		private Class fragmentArray[] = {FregIndex.class,FregCall.class,FregMess.class,FregApp.class,FregShare.class};
		
		//定义数组来存放按钮图片
		private int mImageViewArray[] = {R.drawable.tab_index,R.drawable.tab_call,R.drawable.tab_mess,
										 R.drawable.tab_app,R.drawable.tab_share};
		
		//Tab选项卡的文字
		private String mTextviewArray[] = {"首页", "电话", "短信", "应用", "分享"};
		
		public void onCreate(Bundle savedInstanceState) {
	        super.onCreate(savedInstanceState);
	        requestWindowFeature(Window.FEATURE_NO_TITLE);
	        setContentView(R.layout.divide);
	        
	        Intent intent = getIntent();
	        id = intent.getStringExtra("ID");
	        //Check if permission enabled
	        if (Build.VERSION.SDK_INT >= 21) {
		        if (UStats.getUsageStatsList(this).isEmpty()){
		            intent = new Intent(Settings.ACTION_USAGE_ACCESS_SETTINGS);
		            startActivity(intent);
		        }
	        }
	        saveid = (TextView)findViewById(R.id.myid);
	        saveid.setText(id);
	        
	        initView();
	    }
		 
		/**
		 * 初始化组件
		 */
		private void initView(){
			//实例化布局对象
			layoutInflater = LayoutInflater.from(this);
					
			//实例化TabHost对象，得到TabHost
			mTabHost = (FragmentTabHost)findViewById(android.R.id.tabhost);
			mTabHost.setup(this, getSupportFragmentManager(), R.id.realtabcontent);	
			
			//得到fragment的个数
			int count = fragmentArray.length;	
					
			for(int i = 0; i < count; i++){	
				//为每一个Tab按钮设置图标、文字和内容
				TabSpec tabSpec = mTabHost.newTabSpec(mTextviewArray[i]).setIndicator(getTabItemView(i));
				//将Tab按钮添加进Tab选项卡中
				mTabHost.addTab(tabSpec, fragmentArray[i], null);
				//设置Tab按钮的背景
				mTabHost.getTabWidget().getChildAt(i).setBackgroundResource(R.drawable.selector_tab_background);
			}
		}
					
		/**
		 * 给Tab按钮设置图标和文字
		 */
		private View getTabItemView(int index)
		{
			View view = layoutInflater.inflate(R.layout.tab_item_view, null);
		
			ImageView imageView = (ImageView) view.findViewById(R.id.imageview);
			imageView.setImageResource(mImageViewArray[index]);
			
			TextView textView = (TextView) view.findViewById(R.id.textview);		
			textView.setText(mTextviewArray[index]);
		
			return view;
		}

	 

}
