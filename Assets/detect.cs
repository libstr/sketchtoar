using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class detect : MonoBehaviour
{

    public Text text;
    public GameObject dresser;
    public GameObject door;
    public GameObject table;
    public GameObject chair;
    public GameObject mainobj;
    public string s;
    //string[] str;
    DataSnapshot snapshot;
    string[] str;
    int k = 0;
    // Start is called before the first frame update
    void Start()
    {
        //text.text = "Please wait....";
       // string p=getvalue();
        //inst(p);
        new WaitForSeconds(5);
        getElements();


    }


    void inst(string os)
    {
        Debug.Log(os);
        str = os.Split(char.Parse(";"));
        //   = str[0].Split(char.Parse(";"));

        Debug.Log(str[1]);
        for (int i = 0; i < str.Length - 1; i++)
        {
            string[] obj = str[i].Split(char.Parse("c"));
            string[] pos = obj[1].Split(char.Parse(","));
            string posx = pos[0].Substring(1);
            string posy = pos[1].Substring(1, pos[1].Length - 2);
            Debug.Log(obj[0] + posx + posy);
            //Debug.Log((System.Int32.Parse(posy))/200);
            //Debug.Log((System.Int32.Parse(posx))/200);

            float x = System.Int32.Parse(posy);
            float y = System.Int32.Parse(posx);

            //Debug.Log(x/200);
            //Debug.Log(y/200);

            Vector3 posv = new Vector3(x/800, 0, y/800);
            //Instantiate(cube, posv, Quaternion.identity);
            if (obj[0] == "0")
                Instantiate(dresser, posv, Quaternion.identity).transform.SetParent(mainobj.transform);
            if (obj[0] == "1")
                Instantiate(door, posv, Quaternion.identity).transform.SetParent(mainobj.transform);
            if (obj[0] == "2")
                Instantiate(table, posv, Quaternion.identity).transform.SetParent(mainobj.transform);
            if (obj[0] == "3")
                Instantiate(chair, posv, Quaternion.identity).transform.SetParent(mainobj.transform);

        }
        //mainobj.transform.Rotate(0, 90, 0);

        

    }


    public void getElements()
    {
        StartCoroutine("Getfb");
       
    }

 private IEnumerator Getfb()
    {
        yield return new WaitForSeconds(10f);

        var getTask = FirebaseDatabase.DefaultInstance.GetReference("detected").GetValueAsync();

        yield return new WaitUntil(() => getTask.IsCompleted || getTask.IsFaulted);

        if (getTask.IsCompleted)
        {
            inst(getTask.Result.Value.ToString());
        }
    }
}