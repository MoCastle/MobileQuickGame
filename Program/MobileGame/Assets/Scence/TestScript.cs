using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class TestScript : MonoBehaviour {
    public Text text;
	// Use this for initialization
	void Start () {

        string Path = Application.dataPath+"\n" + Application.streamingAssetsPath;

        StreamReader Reader = File.OpenText("jar:file://" + Application.dataPath + "!/assets/" + "ActorInfo.csv");

            //new StreamReader( Path,)//(Application.dataPath+ "/ActorInfo.csv", true, Encoding.UTF8);
        text.text = Reader.ReadToEnd();
        Reader.Close();
        Reader.Dispose();
        //text.text += writer.read
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
