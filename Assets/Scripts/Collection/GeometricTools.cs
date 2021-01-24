using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GeometricTools
{
    /* Return float angle between 2 Vector
     */
    public static float GetAngle(Vector2 pos, Vector2 endPos = new Vector2(), float less = 0)
    {
        if (endPos != new Vector2())
        {
            pos = endPos -= pos;
        }

        float _dirX = pos.x;
        float _dirY = pos.y;
        float _hypo = Mathf.Sqrt(_dirX * _dirX
                        + _dirY * _dirY);


        float _res = 0;

        if (_dirX >= 0 && _dirY >= 0)
        {
            _res = 90 - Mathf.Tan(_dirY / _hypo) * (180 / Mathf.PI);
        }
        else if (_dirX >= 0 && _dirY < 0)
        {
            _res = 90 - Mathf.Tan(_dirY / _hypo) * (180 / Mathf.PI);
        }
        else if (_dirX < 0 && _dirY < 0)
        {
            _res = 270 + Mathf.Tan(_dirY / _hypo) * (180 / Mathf.PI);
        }
        else if (_dirX < 0 && _dirY >= 0)
        {
            _res = 270 + Mathf.Tan(_dirY / _hypo) * (180 / Mathf.PI);
        }

        if (less != 0)
        {
            _res += less;

            if (_res < 0)
            {
                _res += 360;
            }
            else if (_res >= 360)
            {
                _res -= 360;
            }
        }


        return _res;
    }
    
    /* Return Hypotenuse length
     */
    public static float GetHypot(Vector2 pos, Vector2 endPos = new Vector2())
    {
        if(pos.x < 0)
            pos = new Vector2(-pos.x, pos.y);
        if (pos.y < 0)
            pos = new Vector2(pos.x, -pos.y);
        if (endPos.x < 0)
            pos = new Vector2(-endPos.x, endPos.y);
        if (endPos.y < 0)
            pos = new Vector2(endPos.x, -endPos.y);


        if (endPos != new Vector2())
        {
            pos -= endPos;
        }

        float _dirX = pos.x;
        float _dirY = pos.y;
        float _hypo = Mathf.Sqrt(_dirX * _dirX
                        + _dirY * _dirY);
        return _hypo;
    }

    public static bool AngleLower(float a, float b)
    {
        float res = a - b;
        if (res <= -180)
        {
            return false;
        }
        else if (res <= 0 && res > -180)
        {
            return true;
        }
        else if (res > 0 && res <= 180)
        {
            return false;
        }
        else if (res > 180)
        {
            return true;
        }
        return false;
    }
    public static float AngleDiff(float a, float b)
    {
        float res = a - b;
        if (res <= -180)
        {
            return res + 360;
        }
        else if (res <= 0 && res > -180)
        {
            return res;
        }
        else if (res > 0 && res <= 180)
        {
            return res;
        }
        else if (res > 180)
        {
            return res - 360;
        }
        return 0;
    }
}
