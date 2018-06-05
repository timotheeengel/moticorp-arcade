using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using UnityEngine;

public class webcam : MonoBehaviour
{
    [SerializeField] string eventName = "GGC";
    [SerializeField] Text text;

    string photoKey = "TakePhoto";
    KeyCode resetKey = KeyCode.J;
    KeyCode exitKey = KeyCode.K;

    private const string imagesFolder = "C:/images";

    [SerializeField] string ClientID;
    private ImgurClient imgurClient;

    RawImage rawimage;
    WebCamTexture webcamTexture = null;
    WebCamDevice[] camDevices;

    int photoEnumeration = 0;

    private void OnDestroy()
    {
        webcamTexture.Stop();
    }

    void Start()
    {
        imgurClient = new ImgurClient(ClientID);

        camDevices = WebCamTexture.devices;

        rawimage = GetComponentInChildren<RawImage>();

        imgurClient.OnImageUploaded += uploadComplete;

        Directory.CreateDirectory(imagesFolder);

        StartCoroutine(ChooseWebcam());

        GameObject.Find("Player1").transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Player2").transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;

    }

    IEnumerator ChooseWebcam()
    {
        string camName = "";

        int i = 0;
        if (File.Exists(imagesFolder + "/webcam.txt"))
        {
            camName = File.ReadAllText(imagesFolder + "/webcam.txt");
        }
        else
        {
            foreach (var item in camDevices)
            {
                text.text += "Press numpad " + i + " for " + item.name + "\n";
                i++;
            }
        }
        text.text += "Press Esc to cancel";
        while (camName == "")
        {
            for (int j = 0; j < i; j++)
            {
                if (Input.GetKeyDown((KeyCode)(j + 256)))
                {
                    camName = camDevices[j].name;
                    var file = File.CreateText(imagesFolder + "/webcam.txt");
                    file.Write(camName);
                    file.Close();
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
                break;
            yield return null;
        }
        if (camName != "")
        {
            webcamTexture = new WebCamTexture(camName, 10000, 10000, 30);

            rawimage.texture = webcamTexture;
            rawimage.material.mainTexture = webcamTexture;
            webcamTexture.Play();
            StartCoroutine(UseWebcam());
        }
        text.text = "";
    }

    IEnumerator UseWebcam()
    {
        while (webcamTexture.isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                webcamTexture.Pause();
                string timeStamp = DateTime.Now.ToString("yyyyMMddHHmm");
                string fileName = eventName + timeStamp;

                int resWidth = 1920;
                int resHeight = 1080;

                RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
                Camera.main.targetTexture = rt;
                Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
                Camera.main.Render();
                RenderTexture.active = rt;
                screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
                Camera.main.targetTexture = null;
                RenderTexture.active = null;
                Destroy(rt);
                byte[] bytes = screenShot.EncodeToPNG();
                string filename = imagesFolder + "/img" + fileName + ".png";
                System.IO.File.WriteAllBytes(filename, bytes);

                yield return new WaitForSeconds(1);
                imgurClient.UploadImageFromFilePath(imagesFolder + "/img" + fileName + ".png");
                FindObjectOfType<Concierge>().BringNextCourse("SplashScreen");
                break;
            }
            yield return null;
        }
    }

    public void uploadComplete(object sender, ImgurClient.OnImageUploadedEventArgs response)
    {
        //text.text += response.response.data.link;
    }
}