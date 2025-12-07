using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ImagePicker : MonoBehaviour
{
    public RawImage image;

    [SerializeField] private GameObject objectToHide;
    [SerializeField] private GameObject objectToShow;

    [SerializeField] private string path = "C:\\Users\\s_dummy\\Desktop\\Hackathon appli\\Save path\\LookupImage.png";

    [SerializeField] private Button ShowInfosBtn;

    public void PickImageFromGallery()
    {
        PickAndCropPicture();
        objectToHide.SetActive(false);
        objectToShow.SetActive(true);

        ShowInfosBtn.interactable = true;
    }

    private void PickAndCropPicture()
    {
        NativeGallery.GetImageFromGallery(path =>
        {
            if (path == null) return;

            Texture2D texture = NativeGallery.LoadImageAtPath(path);

            if (texture == null) return;

            CropPicture(texture);
        });
    }

    private void CropPicture(Texture2D texture)
    {
        Texture2D croppedTexture = new Texture2D(0, 0);
        ImageCropper.Instance.Show(texture, (bool result, Texture originalImage, Texture2D croppedImage) =>
        {
            if (result)
            {
                image.texture = croppedImage;

                Vector2 size = image.rectTransform.sizeDelta;
                if (croppedImage.height <= croppedImage.width)
                    size = new Vector2(400f, 400f * (croppedImage.height / (float)croppedImage.width));
                else
                    size = new Vector2(400f * (croppedImage.width / (float)croppedImage.height), 400f);
                image.rectTransform.sizeDelta = size;

                croppedTexture = croppedImage;

                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                byte[] bytes = ImageConversion.EncodeToPNG(MakeReadableCopy(croppedImage));
                File.WriteAllBytes(path, bytes);
            }
            else
            {
                Debug.LogError("Image could not be loaded");
            }
        });
    }

    private Texture2D MakeReadableCopy(Texture2D source)
    {
        RenderTexture rt = RenderTexture.GetTemporary(
            source.width, source.height, 0,
            RenderTextureFormat.Default, RenderTextureReadWrite.sRGB);

        Graphics.Blit(source, rt);

        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = rt;

        Texture2D readable = new Texture2D(source.width, source.height, TextureFormat.RGBA32, false);
        readable.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        readable.Apply();

        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(rt);

        return readable;
    }
}
