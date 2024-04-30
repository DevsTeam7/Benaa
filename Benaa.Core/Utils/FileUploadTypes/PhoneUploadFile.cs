namespace Benaa.Core.Utils.FileUploadTypes
{
    public static class PhoneUploadFile
    {
        public static List<string> ImageExtensions { get; } = new List<string>()
        {
            ".bmp", ".jpeg", ".jpg", ".png"
        };

        public static List<string> VideoExtensions { get; } = new List<string>()
        {
            ".avi", ".flv", ".mov", ".mp4", ".wmv"
        };

        public static List<string> FileExtensions { get; } = new List<string>()
        {
            ".doc", ".docx", ".pdf"
        };
    }
}
