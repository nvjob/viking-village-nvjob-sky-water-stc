using System.Collections;
using UnityEngine;



public class ScreenShot : MonoBehaviour {
    //******************************************************


    // 4k = 3840 × 2160
    // 2k = 2560 × 1440

    public int resWidth = 1920;
    public int resHeight = 1080;
    public float timeShift = 1.0f;
    public float timeRepit = 1.0f;
    public bool auto;
    bool screOn;




    //******************************************************




    public static string ScreenShotName(int width, int height)
    {
        return string.Format("{0}/screenshot/screen_{1}x{2}_{3}.png",
                             Application.dataPath,
                             width, height,
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }




    //******************************************************




    void Start()
    {

        if (auto == true) StartCoroutine(Cor_0());

    }

    


    //******************************************************





    IEnumerator Cor_0()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeRepit);
            screOn = true;
            Screnn();
        }
    }




    //******************************************************




    void LateUpdate()
    {
        Time.timeScale = timeShift;
        if (Input.GetKeyDown("k") == true && screOn == false)
        {
            screOn = true;
            Screnn();
        }
    }
    



    //******************************************************




    void Screnn()
    {
        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        GetComponent<Camera>().targetTexture = rt;
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        GetComponent<Camera>().Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        GetComponent<Camera>().targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = ScreenShotName(resWidth, resHeight);
        System.IO.File.WriteAllBytes(filename, bytes);
        screOn = false;
    }







    //******************************************************
}
