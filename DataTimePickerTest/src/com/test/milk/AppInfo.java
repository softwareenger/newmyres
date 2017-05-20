package com.test.milk;

import android.graphics.drawable.Drawable;
import android.util.Log;

public class AppInfo
{
	public String appName="";
	public String packageName="";
	public Drawable appIcon=null;
	public boolean used = false;
	public AppInfo()
	{
	}
	public void Changeused()
	{
		if(used){used=false;}
		else{used=true;}
	}
	public String getappName(){return appName;}
	public String getpackageName(){return packageName;}
	public Drawable getappIcon(){return appIcon;}
	public Boolean getused(){return used;}
}