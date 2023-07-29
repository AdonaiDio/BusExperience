using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreLevelScript : MonoBehaviour
{
    
    [SerializeField] private TMP_Text collectedText;
    [SerializeField] private TMP_Text costsText;
    [SerializeField] private TMP_Text totalText;

    private SoundManager soundManager;
    private int nextSceneIndex = 1;

    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }

    public void StartCounting(int money, int lvlCosts, int _nextSceneIndex)
    {
        nextSceneIndex = _nextSceneIndex;
        soundManager.StopSFX();
        StartCoroutine(CountUpNumberTextUI(0,money, 1.5f, collectedText, "Dinheiro coletado: \n"));
        StartCoroutine(CountUpNumberTextUI(1.5f,lvlCosts, 1.5f, costsText, "Despesas gerais: \n"));
        int totalMoney = money - lvlCosts;
        StartCoroutine(CountUpNumberTextUI(3,totalMoney, 1.5f, totalText, "Valor total: \n"));
        if(totalMoney <= 0)
        {
            //perdeu
            StartCoroutine(PlaySfxEndLvL(7, 1));
        }
        else { //proxima fase
            StartCoroutine(PlaySfxEndLvL(3, nextSceneIndex));
        }
    }
    IEnumerator CountUpNumberTextUI(float delay, float money, float duration, TMP_Text tmpro, string text)
    {
        yield return new WaitForSeconds(delay);
        soundManager.PlaySFX(soundManager.sfxs[6]);
        float curValue = 0;
        float framerate = Mathf.Abs(money - curValue)/duration;//talvez n precise
        while (curValue != money)
        {
            curValue = Mathf.MoveTowards(curValue, money, framerate * Time.deltaTime);
            tmpro.text = text + ((int)curValue).ToString();
            yield return null;
        }
    }
    IEnumerator PlaySfxEndLvL(int x, int index)
    {
        soundManager.PlaySFX(soundManager.sfxs[x]);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(index);
    }
}
