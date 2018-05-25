using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using UnityEngine;

public class webcam : MonoBehaviour
{
    [SerializeField] Text text;

    private const string imagesFolder = "C:/images";

    [SerializeField] string ClientID;
    private ImgurClient imgurClient;

    RawImage rawimage;
    WebCamTexture webcamTexture = null;
    WebCamDevice[] camDevices;

    int photoEnumeration = 0;
    
    void Start()
    {
        DontDestroyOnLoad(gameObject);
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            TakePhoto();
    }

    public void StopCamera()
    {
        webcamTexture.Pause();
    }

    public void StartCamera()
    {
        webcamTexture.Play();
    }

    public void DisableWebcam()
    {
        StopCamera();
        gameObject.SetActive(false);
    }

    public void EnableWebcam()
    {
        StartCamera();
        gameObject.SetActive(true);
    }

    IEnumerator Screenshot(string filePath)
    {
        //Texture2D texture = new Texture2D(rawimage.texture.width, rawimage.texture.height, TextureFormat.ARGB32, false);

        ScreenCapture.CaptureScreenshot(filePath);

        webcamTexture.Pause();
        //texture.SetPixels(webcamTexture.GetPixels());
        //texture.Apply();
        //
        //File.WriteAllBytes(Application.dataPath + "/images/img" + photoEnumeration + ".png", texture.EncodeToPNG());

        while (!File.Exists(filePath))
            yield return null;
        imgurClient.UploadImageFromFilePath(filePath);
    }

    public void TakePhoto()
    {
        StartCoroutine(Screenshot(imagesFolder + "/img" + photoEnumeration + ".png"));
        photoEnumeration++;
    }

    public void uploadComplete(object sender, ImgurClient.OnImageUploadedEventArgs response)
    {
        //text.text += response.response.data.link;
    }
}