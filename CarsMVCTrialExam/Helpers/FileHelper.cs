namespace CarsMVCTrialExam.Helpers
{
    public static class FileHelper
    {
        public static async Task<string> SaveFileAsync(this IFormFile file, string path)
        {
            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
            string extension = Path.GetExtension(file.FileName);
            fileName = Path.Combine(path, Path.GetRandomFileName() + fileName + extension);
            using (FileStream fs = File.Create(Path.Combine(PathConstants.RootPath, fileName)))
            {
                await file.CopyToAsync(fs);
            }
            return fileName;
        }

        public static bool IsValidSize(this IFormFile file, float kb)
            => file.FileName.Length <= kb;

        public static bool CheckType(this IFormFile file, string type)
            => file.ContentType.Contains(type);
    }
}
