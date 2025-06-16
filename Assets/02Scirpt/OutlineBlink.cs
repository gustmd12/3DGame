using UnityEngine;
using UnityEngine.UI;

public class OutlineBlink : MonoBehaviour
{
    [SerializeField] Outline[] outlines;
    private Color baseColor = Color.yellow;
    private float blinkSpeed = 2f;


    private float selectIndex = -1;
    private void Update()
    {
        for(int i = 0; i < outlines.Length; i++)
        {
            if (outlines[i] == null) continue;

            if (i == selectIndex)
            {
                float alpha = Mathf.PingPong(Time.time * blinkSpeed, 1f);
                Color c = baseColor;
                c.a = alpha;
                outlines[i].effectColor = c;
            }
            else
            {
                Color c = baseColor;
                c.a = 0f;
                outlines[i].effectColor = c;
            }
        }

    }

    public void SelectBTN(int index)
    {
        selectIndex = index;
    }

    public void ClearSelect()
    {
        selectIndex = -1;
    }
}
