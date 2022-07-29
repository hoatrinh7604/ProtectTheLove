using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleController : MonoBehaviour
{
    [SerializeField] Button button;
    public int colorIndex;

    float distance = 10;
    bool canMove = false;

    public bool death = false;

    [SerializeField] GameObject effect;
    private Camera cam;

    [SerializeField] bool isX = false;
    private void Start()
    {
        //button.onClick.AddListener(() => Press());
        cam = GamePlayController.Instance.UICamera;

        if(isX)
        {
            GamePlayController.Instance.scaleByX += 0.1f;
        }
    }

    private void Update()
    {
        if(canMove)
        {
            Ray r = cam.ScreenPointToRay(Input.mousePosition);
            Vector3 pos = r.GetPoint(distance);
            transform.position = new Vector3 (pos.x, pos.y, transform.position.z);
        }
    }

    private void OnMouseDown()
    {
        canMove = true;
    }

    private void OnMouseUp()
    {
        canMove = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "X")
        {
            if (isX)
            {
                SpawEffect(collision.gameObject.transform);
                GamePlayController.Instance.UpdateSliderByValue(2);
                GamePlayController.Instance.scaleByX -= 0.1f;
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
                GamePlayController.Instance.GameOver();
            }
        }
    }

    public void SetColorIndex(int value)
    {
        colorIndex = value;
        UpdateSprite();
    }

    public int GetColorIndex()
    {
        return colorIndex;
    }

    public void UpdateSprite()
    {
        //color.color = GamePlayController.Instance.template[colorIndex];
    }

    public void Press()
    {
        GamePlayController.Instance.OnPressHandle(colorIndex);
        Destroy(this.gameObject, 0.05f);
    }

    public void RandomColor()
    {
        SetColorIndex(Random.Range(0, GamePlayController.Instance.template.Length));
    }

    public void SpawEffect(Transform trans)
    {
        GameObject eff = Instantiate(effect, Vector2.zero, Quaternion.identity, trans);
        eff.transform.localPosition = Vector3.zero;
        Destroy(eff, 0.5f);
    }
}
