using System;
using System.Drawing;

public static class Utils
{
    public static SizeF ProportionalSize(float Width, float Height, float scale)
    {
        return new SizeF(
            Width * scale,
            Height * scale
        );
    }
    public static SizeF ProportionalSize(float Width, float Height, SizeF scaledSize)
        => ProportionalSize(Width, Height,
            Math.Min(scaledSize.Width / (float)Width, scaledSize.Height / (float)Height)
        );
    public static SizeF ProportionalSize(this Image image, float scale)
        => ProportionalSize(image.Width, image.Height, scale);
    public static SizeF ProportionalSize(this Image image, SizeF scaledSize)
        => ProportionalSize(image, 
            Math.Min(scaledSize.Width / image.Width, scaledSize.Height / image.Height)
        );
}