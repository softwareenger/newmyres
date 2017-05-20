package com.test.milk;

public class Record {

	String id;
	String display;
	String conx;
	
	public Record(String id,String display,String conx)
	{
		this.id = id;
		this.display = display;
		this.conx = conx;
	}
	
	public String getid(){return id;}
	public String getdisplay(){return display;}
	public String getconx(){return conx;}

}
