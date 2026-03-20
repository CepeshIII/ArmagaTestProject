using UnityEngine;

public class FrameRateLimitSetter : IFrameRateLimitSetter
{

    public void SetFrameRateLimit(int frameRateIndex)
    {
        var frameRate = -1;
        switch (frameRateIndex) 
        {
            case 0:
                frameRate = 30;
                break;
            case 1:
                frameRate = 50;
                break;
            case 2:
                frameRate = 60;
                break;
            case 3:
                frameRate = 90;
                break;
            case 4:
                frameRate = 120;
                break;
            case 5:
                frameRate = 144;
                break;
        }

        Application.targetFrameRate = frameRate;
    }
}


public interface IFrameRateLimitSetter
{
    public void SetFrameRateLimit(int frameRateIndex);
}
