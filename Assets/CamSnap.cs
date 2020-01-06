using Firebase;
using Firebase.Unity.Editor;
using Firebase.Storage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using UnityEngine.SceneManagement;

public class CamSnap : MonoBehaviour
{
    public RawImage imageCam;
    public RawImage imageShot;
    private WebCamTexture webCamTexture;
    private string fileName = "/imgforar";
    private string savePath = "/mnt/sdcard/Android/data/com.libin.sketchtoar/files/";
    private string firebasePath = "/img.png";

    //rivate string savePath = "F:/";

    private int captureCounter = 0;
    private byte[] bytes;
    private Texture2D snap;
    // Start is called before the first frame update
    void Start()
    {
        webCamTexture = new WebCamTexture();
        imageCam.texture = webCamTexture;
        webCamTexture.Play();
    }

    public void ButtonSnapShot()
    {
       
        StartCoroutine("ClickImage");

    }
    public void changetodetect()
    {
        SceneManager.LoadScene("arcpp");

    }

    private IEnumerator ClickImage() {
        yield return new WaitForEndOfFrame();
        snap = new Texture2D(webCamTexture.width, webCamTexture.height);
        snap.SetPixels(webCamTexture.GetPixels());
        snap.Apply();
        bytes = snap.EncodeToPNG();

        var storage = FirebaseStorage.DefaultInstance;
        var finalScoreReference = storage.GetReference(string.Format(firebasePath));
        var uploadTask = finalScoreReference.PutBytesAsync(bytes);
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://sketchtoar.firebaseio.com/");
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        reference.Child("val2").SetValueAsync("we");

        System.IO.File.WriteAllBytes(Application.persistentDataPath + fileName + ".png", bytes);

        imageShot.texture = snap as Texture;
        SceneManager.LoadScene("arcpp");
        ++captureCounter;
    }
  
}
