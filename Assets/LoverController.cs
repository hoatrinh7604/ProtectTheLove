using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LoverController : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] Transform posEff;
    [SerializeField] Image lover;
    [SerializeField] GameObject effPrefab;

    private float loverTime = 0;
    private bool loving = false;

    private void Update()
    {
        if(loving)
        {
            loverTime += Time.deltaTime;
            if(loverTime > 1)
            {
                ChangeToNormal();
                loving = false;
                loverTime = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Love")
        {
            GamePlayController.Instance.UpdateScore();
            GamePlayController.Instance.UpdateSliderByValue(4);
            ChangeToLove();

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Broken")
        {
            Destroy(collision.gameObject);
            GamePlayController.Instance.GameOver();
        }
    }

    public void ChangeToLove()
    {
        loving = true;
        lover.sprite = sprites[1];

        var eff = Instantiate(effPrefab, Vector2.zero, Quaternion.identity, posEff);
        eff.transform.localPosition = Vector3.zero;
        Destroy(eff, 1f);
    }

    public void ChangeToNormal()
    {
        lover.sprite = sprites[0];
    }
}
