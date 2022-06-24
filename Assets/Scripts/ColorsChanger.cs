using UnityEngine;

public class ColorsChanger
{
    private Color[] _colors = new Color[6]
    {
       new Color (1f, 0f, 0f, 1f),
       new Color (1f, 0.6f, 0f, 1f),
       new Color (1f, 1f, 0f, 1f),
       new Color (0f, 1f, 0f, 1f),
       new Color (0f, 0f, 1f, 1f),
       new Color (1f, 0f, 1f, 1f)
   };

    private int colorIndex = 0;
    private float t;

    public Color GetColor(Color currentColor,float step = 1)
    {
        currentColor = Color.Lerp(currentColor, _colors[colorIndex], step * Time.deltaTime);

        t = Mathf.Lerp(t, 1f, step * Time.deltaTime);
        if (t > .9f)
        {
            t = 0f;
            colorIndex++;
            colorIndex = (colorIndex >= _colors.Length) ? 0 : colorIndex;
        }
        return currentColor;
    }

    public Color GetRandomColor()
    {
        return Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}
