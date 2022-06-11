using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoryViewerListItem : MonoBehaviour
{
    public Image ShapeViewer;

    public void SetShape(Sprite shape)
    {
        ShapeViewer.sprite = shape;
    }
}
