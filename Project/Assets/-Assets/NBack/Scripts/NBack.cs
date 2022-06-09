using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NBack : MonoBehaviour
{
    [Header("도형 이미지.")]
    public Sprite[] Shapes;

    [Header("게임 진행 관련.")]
    public Image ShapeViewer;
    public Slider TimeSlider;
    public GameObject HistoryViewer;
    public Transform HistoryViewerContent;
    public GameObject HistoryViewerListItem;

    List<int> randomIndexList = new List<int>();
    Coroutine playingCoroutine;
    bool isShowHistory = false;

    public void Start()
    {
        Play();
    }

    public void ToggleShowHistory()
    {
        if (isShowHistory)
        {
            isShowHistory = false;
            HistoryViewer.SetActive(false);
            Time.timeScale = 1;

            Transform[] listItems = HistoryViewerContent.GetComponentsInChildren<Transform>();
            for (int i = 1; i < listItems.Length; i++)
            {
                Destroy(listItems[i].gameObject);
            }
        }
        else
        {
            isShowHistory = true;
            HistoryViewer.SetActive(true);
            Time.timeScale = 0;

            for(int i=0; i<randomIndexList.Count; i++)
            {
                GameObject go = Instantiate(HistoryViewerListItem, HistoryViewerContent);
                go.GetComponent<HistoryViewerListItem>().SetShape(Shapes[randomIndexList[i]]);
            }
        }
    }

    public void Play()
    {
        isShowHistory = true;
        ToggleShowHistory();

        randomIndexList.Clear();

        if(playingCoroutine != null) // 진행 중인 게임 중단.
        {
            StopCoroutine(playingCoroutine);
        }
        playingCoroutine = StartCoroutine(PlayCoroutine());
    }

    IEnumerator PlayCoroutine()
    {
        ShapeViewer.enabled = false;

        int randomIndex = -1;
        while (true)
        {
            float time = 0.0f;
            while(time < 3.0f)
            {
                time += Time.deltaTime;
                TimeSlider.value = time;
                yield return null;
            }
            randomIndex = GetRandomIndex(randomIndex);
            randomIndexList.Add(randomIndex);

            ShapeViewer.enabled = true;
            ShapeViewer.sprite = Shapes[randomIndex];
        }
    }

    private int GetRandomIndex(int exceptIndex = -1)
    {
        int random = Random.Range(0, Shapes.Length);
        while (random == exceptIndex) // 특정 숫자 제외하기.
        {
            random = Random.Range(0, Shapes.Length);
        }
        
        return random;
    }
}
