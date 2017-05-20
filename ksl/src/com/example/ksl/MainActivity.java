package com.example.ksl;

import java.util.ArrayList;
import java.util.List;



import android.app.Activity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.Toast;
import android.view.*;
import java.util.*;

import com.example.compoundbuttonview.view.CheckSwitchButton;



public class MainActivity extends Activity {
    //ListView控件
    private ListView mList;
    //ListView数据源
    private ArrayList<String> data;
    private CheckSwitchButton mCheckSwithcButton;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        data = new ArrayList<String>();
        mList = (ListView)findViewById(R.id.mList);
        for(int i = 0; i < 20; i ++){
        	
            data.add("今天好手气" + i);
        }
        MyAdapter adapter = new MyAdapter(data);
        mList.setAdapter(adapter);
    }
}