package com.test.milk;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.util.ArrayList;
import java.util.Timer;

import android.app.KeyguardManager;
import android.app.KeyguardManager.KeyguardLock;
import android.app.Notification;
import android.app.Service;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.IBinder;
import android.util.Log;


public class LockService extends Service {
	private Timer mTimer;
	String id;
	public static final int FOREGROUND_ID = 0;
	ArrayList<String> data;

	private void startTimer() {
		if (mTimer == null) {
			mTimer = new Timer();
			LockTask lockTask = new LockTask(this, data);
			mTimer.schedule(lockTask, 0L, 1000L);
		}
	}

	public IBinder onBind(Intent intent) {
		return null;
	}

	public void onCreate() {
		super.onCreate();

		startForeground(FOREGROUND_ID, new Notification());
	}

	public int onStartCommand(Intent intent, int flags, int startId) {

		id = load("TNOWID");
		Log.w("NOWID", id);
		data = new ArrayList<String>();
		Init();
		Log.w("INITSUCCESS", id);
		startTimer();
		return super.onStartCommand(intent, flags, startId);
	}

	public void onDestroy() {
		stopForeground(true);
		mTimer.cancel();
		mTimer.purge();
		mTimer = null;
		super.onDestroy();
	}

	String[] pags = new String[300];
	String pag;

	void Init() {
		pag = load(id + "package");
		if (pag == "") {
			data.clear();
		}
		pags = pag.split(" ");
		for (int i = 0; i < pags.length; i++) {
			data.add(new String(pags[i]));
			Log.w("data",pags[i]);
 
		}
		Log.w("data.size", String.valueOf(data.size()));
	}

	public String load(String file) {
		FileInputStream in = null;
		BufferedReader reader = null;
		StringBuilder content = new StringBuilder();
		try {
			in = openFileInput(file);
			reader = new BufferedReader(new InputStreamReader(in));
			String line = "";
			while ((line = reader.readLine()) != null) {
				content.append(line);
			}
		} catch (IOException e) {
			e.printStackTrace();
		} finally {
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