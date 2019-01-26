using System;

namespace FaceApi
{
    [Serializable]
    public class FaceResult
    {
        public string faceId;
        public FaceRectangle faceRectangle;
        public FaceAttributes faceAttributes;

        public override string ToString()
        {
            return $"{nameof(faceId)}: {faceId}, {nameof(faceRectangle)}: {faceRectangle}," +
                   $" {nameof(faceAttributes)}: {faceAttributes}";
        }
    }

    [Serializable]
    public class FaceRectangle
    {
        public int top;
        public int left;
        public int width;
        public int height;

        public override string ToString()
        {
            return $"{nameof(top)}: {top}, {nameof(left)}: {left}, {nameof(width)}: {width}," +
                   $" {nameof(height)}: {height}";
        }
    }

    [Serializable]
    public class FaceAttributes
    {
        public Emotion emotion;

        public override string ToString()
        {
            return $"{nameof(emotion)}: {emotion}";
        }
    }

    [Serializable]
    public class Emotion
    {
        public float anger;
        public float contempt;
        public float disgust;
        public float fear;
        public float happines;
        public float neutral;
        public float sadness;
        public float suprise;

        public override string ToString()
        {
            return $"{nameof(anger)}: {anger}, {nameof(contempt)}: {contempt}, {nameof(disgust)}: {disgust}," +
                   $" {nameof(fear)}: {fear}, {nameof(happines)}: {happines}, {nameof(neutral)}: {neutral}," +
                   $" {nameof(sadness)}: {sadness}, {nameof(suprise)}: {suprise}";
        }
    }
}