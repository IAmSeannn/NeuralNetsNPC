using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using NeuroNetworkAI;

public class AICreator : EditorWindow 
{
	public static List<int> Topology = new List<int>();
	private static int TopologyLength = 2;
	public static string StudyFileName = @"Assets\AI\DataAI\";  // You can set the other default folder
	public static string SaveFileName = @"Assets\AI\DataAI\";   //
	public static float Error = 0.05f;
	public static float Speed = 0.1f;
	public static int MaxCicles = 20000;

	public AI ai;
	
	static bool groupEnabled;
	
	
	[MenuItem ("Window/AI")]
	static void Make()
	{
		EditorWindow.GetWindow<AICreator>();
		Topology.Add (1);
		Topology.Add (1);
	}
	
	void OnGUI()
	{
		GUILayout.Label ("Files", EditorStyles.boldLabel);
		StudyFileName = EditorGUILayout.TextField ("Study file folder", StudyFileName);
		SaveFileName = EditorGUILayout.TextField ("Save file folder", SaveFileName);
		
		GUILayout.Label ("Number of layers", EditorStyles.boldLabel);
		TopologyLength = EditorGUILayout.IntSlider("",TopologyLength,2,20);	
		
		GUILayout.Label ("Layers", EditorStyles.boldLabel);
		EditorGUILayout.BeginVertical();
		if(Topology.Count<TopologyLength)
		{
			Topology.Add(1);
		}
		if(Topology.Count>TopologyLength)
		{
			Topology.RemoveAt (Topology.Count-1);
		}
		for (int i = 0; i < Topology.Count; i++) 
		{
			Topology[i] = EditorGUILayout.IntSlider("Neurons in layer",Topology[i],1,50);
		}
		EditorGUILayout.EndVertical ();	
		
		groupEnabled = EditorGUILayout.BeginToggleGroup ("Advanced Settings", groupEnabled);
		Error = EditorGUILayout.FloatField("Max Error",Error);
		Speed = EditorGUILayout.FloatField ("Speed of Studying",Speed);
		MaxCicles = EditorGUILayout.IntSlider ("Max Cicles",MaxCicles,5000,500000);
		EditorGUILayout.EndToggleGroup ();
		
		if(GUILayout.Button("Create and study AI"))
		{
			MakeNetwork ();
			EditorUtility.DisplayDialog ("Information","Cicles Passed = "+ai.CiclesPassed+"\n"+"AverageError = "+ai.CurrentStudyError,"Ok");
		}
	}
	
	void MakeNetwork()
    {
        ai = new AI();
        ai.Initialize(Conversion(Topology), StudyFileName,Error,Speed,MaxCicles);
        ai.SaveNeuroNetwork(SaveFileName);
    }
	
	int[] Conversion(List<int> inp)
	{
		int[] res = new int[inp.Count];
		for (int i = 0; i < res.Length; i++) 
		{
			res[i] = inp[i];
		}
		return res;
	}
}
