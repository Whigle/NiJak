using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class ResourcesManger {

	static Dictionary<string,int> resources = new Dictionary<string,int>();
	static public int resourcesCapacity=1000;
	// Use this for initialization
	static void Start () {
		resources.Add("Food",100);
		resources.Add("Building material",100);
		resources.Add("Bananas",0);
		resources.Add("Sugar",0);
		resources.Add("Wood",0);	
		resources.Add("Stone",0);	
	}
	
	// Update is called once per frame
	static void Update () {
		
	}

	static public bool hasResource(string resourceName, int value=1){
		if(resources.ContainsKey(resourceName)){
			if (resources[resourceName]>=value) return true;
			return false;
		}
		else return false;
	}

	static public int getResource(string resourceName){
		if(resources.ContainsKey(resourceName)){
			return resources[resourceName];
		}
		else return 0;
	}

	static public bool decreaseResource(string resourceName, int value){
		if(resources.ContainsKey(resourceName)&&(resources[resourceName]-value>=0)){
			resources[resourceName]-=value;
			return true;
		}
		else return false;
	}

	static public bool increaseResource(string resourceName, int value){
		if(resources.ContainsKey(resourceName)){
			if (resources[resourceName]+value>=resourcesCapacity){
				resources[resourceName]=resourcesCapacity;
			}
			else{
				resources[resourceName]+=value;
			}
			return true;
		}
		else return false;
	}

	static public bool clearStockpile(){
		string [] str = new string[resources.Keys.Count];
		resources.Keys.CopyTo(str,0);
		resources.Clear();
		foreach(string s in str){
			resources.Add(s,0);
		}
		return true;
	}

}
