using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideShow : MonoBehaviour
{
    [SerializeField] private Image imgShow;

    [SerializeField] List<Sprite> imageToShow;

    private int indexImageToShow;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ShowNextImage", 2f, 3f);
    }


    private void ShowNextImage()
    {

        indexImageToShow++;
        if (indexImageToShow > imageToShow.Count - 1)
        {
            indexImageToShow = 0;
        }

        imgShow.sprite = imageToShow[indexImageToShow];

    }

 
   
}
