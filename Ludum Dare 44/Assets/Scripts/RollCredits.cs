using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RollCredits : MonoBehaviour
{
    public PlayerController player;
    private bool rolling = false;
    public Animator creditsAnim;
    public TMP_Text text1;
    public TMP_Text text2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isGoal && !rolling)
        {
            AnimCredits();
            StartFades();
        }
    }

    public void AnimCredits()
    {
        rolling = true;
        creditsAnim.SetTrigger("rollCredits");
    }

    public void StartFades()
    {
        StartCoroutine(FadeTextToFullAlpha(2f, text1));
        StartCoroutine(FadeTextToFullAlpha(2f, text2));
    }


    public IEnumerator FadeTextToFullAlpha(float t, TMP_Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }

        yield return new WaitForSeconds(2f);
        StartCoroutine(FadeTextToZeroAlpha(1f, GetComponent<TMP_Text>()));
    }

    public IEnumerator FadeTextToZeroAlpha(float t, TMP_Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
