using UnityEngine;
using UnityEngine.UI;

public class SetActiveElement : MonoBehaviour
{
    private Image _image;
    void Start()
    {
        _image = GetComponent<Image>();
    }

    public void SetActived()
    {
        var tempColor = _image.color;
        tempColor.a = 0.3f;
        _image.color = tempColor;
    }
    public void SetPassived()
    {
        var tempColor = _image.color;
        tempColor.a = 0.0f;
        _image.color = tempColor;
    }
}
