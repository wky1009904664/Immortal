using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class BallManager : MonoBehaviour {

    private static string path;

    private Object red;
    private Object green;
    private Object blue;

	// Use this for initialization
	void Start () {
        path = Application.streamingAssetsPath + "/Balls.json";
        BallManager.CreatBall();
        red = Resources.Load("Prefabs/ColorBalls/red");
        green = Resources.Load("Prefabs/ColorBalls/green");
        blue = Resources.Load("Prefabs/ColorBalls/blue");
        //BallManager.InsertBall("red",8.4f,1,7.8f);
        //BallManager.InsertBall("green",-13.6f,1,-9.5f);
        //BallManager.InsertBall("blue",1.5f,1,9.8f);
        //BallManager.DeleteBall();
	}

    public static void CreatBall()
    {
        if (!File.Exists(path))
        {
            StringBuilder sb = new StringBuilder();
            JsonWriter jw = new JsonWriter(sb);

            //相当于写下了"{"
            jw.WriteObjectStart();

            jw.WritePropertyName("Number");
            jw.Write(3);

            jw.WritePropertyName("Member");
            //相当于写下了"["
            jw.WriteArrayStart();

            jw.WriteObjectStart();
            jw.WritePropertyName("color");
            jw.Write("red");
            jw.WritePropertyName("x");
            jw.Write(8.4);
            jw.WritePropertyName("y");
            jw.Write(1);
            jw.WritePropertyName("z");
            jw.Write(-15);
            jw.WriteObjectEnd();

            jw.WriteObjectStart();
            jw.WritePropertyName("color");
            jw.Write("blue");
            jw.WritePropertyName("x");
            jw.Write(9.8);
            jw.WritePropertyName("y");
            jw.Write(1);
            jw.WritePropertyName("z");
            jw.Write(-9.5);
            jw.WriteObjectEnd();

            jw.WriteObjectStart();
            jw.WritePropertyName("color");
            jw.Write("green");
            jw.WritePropertyName("x");
            jw.Write(12.4);
            jw.WritePropertyName("y");
            jw.Write(1);
            jw.WritePropertyName("z");
            jw.Write(-13.6);
            jw.WriteObjectEnd();

            //相当于写下了"]"
            jw.WriteArrayEnd();
            //相当于写下了"}"
            jw.WriteObjectEnd();

            StreamWriter sw = new StreamWriter(path);
            sw.Write(sb.ToString());
            sw.Close();
        }
    }


    public static void InsertBall(string color,float x,float y,float z)
    {
        if (File.Exists(path))
        {
            string str = File.ReadAllText(path);
            JsonData jd = JsonMapper.ToObject(str);
            JsonData jd1 = jd["Member"];

            JsonData jd2 = new JsonData();
            jd2["color"] = color;
            jd2["x"] = x;
            jd2["y"] = y;
            jd2["z"] = z;
            jd1.Add(jd2);
            jd["Number"] = jd1.Count;
            jd["Member"] = jd1;
            File.WriteAllText(path, jd.ToJson());
        }
    }

    public static void DeleteBall()
    {
        if (File.Exists(path))
        {
            print("ok?");
            string str = File.ReadAllText(path);
            JsonData jd = JsonMapper.ToObject(str);
            JsonData jd1 = jd["Member"];

            int i = 0;
            foreach (JsonData vjd in jd1)
            {
                i++;
                if (i>=5)
                {
                    ((IList)jd1).Remove(vjd);
                    jd["Number"] = jd1.Count;
                    jd["Member"] = jd1;
                }
            }
            File.WriteAllText(path, jd.ToJson());
        }
    }

    public void LoadBalls()
    {
        if (File.Exists(path))
        {
            StreamReader sr = new StreamReader(path);
            string str = sr.ReadToEnd();
            JsonData jd = JsonMapper.ToObject(str);

            Debug.Log("Balls 有" + jd["Number"] + "个球，分别是：");

            foreach (JsonData vjd in jd["Member"])
            {
                float tmpx = float.Parse(vjd["x"].ToString());
                float tmpy = float.Parse(vjd["y"].ToString()); 
                float tmpz = float.Parse(vjd["z"].ToString());
                print(tmpz);

                switch ((string)vjd["color"])
                {
                    case "red":
                        Instantiate(red, new Vector3(tmpx, tmpy, tmpz), Quaternion.identity);
                        break;
                    case "green":
                        Instantiate(green, new Vector3(tmpx, tmpy, tmpz), Quaternion.identity);
                        break;
                    case "blue":
                        Instantiate(blue, new Vector3(tmpx, tmpy, tmpz), Quaternion.identity);
                        break;
                }
                Debug.Log("姓名：" + (string)vjd["color"]);
               
            }
        }
    }
}
