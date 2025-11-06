using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JournalScript : MonoBehaviour
{
    public List<Sprite> Pages;
    List<Sprite> current=new List<Sprite>();
    Dictionary<Sprite, int> currentDict=new Dictionary<Sprite, int>();
    int curPage=0;
    public TextMeshProUGUI pageTxt;
    public Image img;
    int unlockedPages = 5;
    bool isOpen = false;
    public List<bool> Unlocked;

    
    void Awake()
    {
        for (int i = 0; i < Pages.Count; i++)
        {
            currentDict.Add(Pages[i], i);   
        }
        initialize();
    }
    public void Prev()
    {
        if (curPage <=0) return;
        curPage--;
        Page();
    }
    public void Next()
    {
        if (curPage >= current.Count-1) return;
        curPage++;
        Page();
    }
    public void Page()
    {
            img.sprite = current[curPage];
            pageTxt.text = "Journal page:" + (currentDict[current[curPage]]+1)+"/6" ;  
    }
    public void SetUnlocked(int pageNum)
    {
        if (Unlocked[pageNum] == false)
        {
            Unlocked[pageNum] = true;
            unlockedPages++;
        }
       
    }
    public void Open()
    {
        isOpen = true;
        GetComponentInChildren<Canvas>().enabled = true;
        FindAnyObjectByType<PlayerController>().PauseGame();
        
    }
    public void Close()
    {
        isOpen = false;
        GetComponentInChildren<Canvas>().enabled = false;
        FindAnyObjectByType<PlayerController>().ResumeGame();
        
    }
    public void Toggle()
    {
        if (isOpen)
        {
            Close();
        }
        else
        {
            Open();
        }
    }
    void initialize()
    {
        current.Clear();
        for (int i = 0; i < Pages.Count; i++)
        {
            
            if (Unlocked[i]==true)
            {
                current.Add(Pages[i]);   
            }
        }
    }
}


