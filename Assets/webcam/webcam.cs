using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using UnityEngine;

public class webcam : MonoBehaviour
{
    [SerializeField] Text text;

    KeyCode photoKey = KeyCode.J;
    KeyCode resetKey = KeyCode.J;
    KeyCode exitKey = KeyCode.K;

    private const string imagesFolder = "C:/images";

    [SerializeField] string ClientID;
    private ImgurClient imgurClient;

    RawImage rawimage;
    WebCamTexture webcamTexture = null;
    WebCamDevice[] camDevices;

    int photoEnumeration = 0;
    
    void Start()
    {
        imgurClient = new ImgurClient(ClientID);

        camDevices = WebCamTexture.devices;

        rawimage = GetComponentInChildren<RawImage>();

        imgurClient.OnImageUploaded += uploadComplete;

        Directory.CreateDirectory(imagesFolder);

        while (File.Exists(imagesFolder + "/img" + photoEnumeration + ".png"))
            photoEnumeration++;
        StartCoroutine(ChooseWebcam());
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
        }
        text.text = "";
    }

    IEnumerator UseWebcam()
    {
        while (true)
        {
            while (webcamTexture.isPlaying)
            {
                if (Input.GetKeyDown(photoKey))
                {
                    webcamTexture.Pause();
                    ScreenCapture.CaptureScreenshot(imagesFolder + "/img" + photoEnumeration + ".png");
                    break;
                }
                yield return null;
            }
            if (Input.GetKeyDown(resetKey))
            {
                webcamTexture.Play();
            }
            yield return null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(exitKey))
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i));
            }
            Destroy(gameObject);
            if (!webcamTexture.isPlaying)
            {
                imgurClient.UploadImageFromFilePath(imagesFolder + "/img" + photoEnumeration + ".png");
                photoEnumeration++;
            }
        }
    }
    
    public void uploadComplete(object sender, ImgurClient.OnImageUploadedEventArgs response)
    {
        //text.text += response.response.data.link;
    }
}