namespace RepositoryDemo.Extentions;

public static class FormFileExtension
{
    // public static bool IsImage(this IFormFile file)
    // {
    //     return file.ContentType.Contains("image/");
    // }
    //
    //
    // public static bool MaxLength(this IFormFile file, int kb)
    // {
    //     return file.Length / 1024 > kb;
    // }


    public static async Task<string> SaveImg(this IFormFile file, string webroot, string folder)
    {
        var path = webroot;
        var fileName = Guid.NewGuid() + file.FileName;
        var resultPath = Path.Combine(path, folder, fileName);

        using (var fileStream = new FileStream(resultPath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        return fileName;
    }


    // public static void DeletePath(string webroot, string folder, string image)
    // {
    //     string path = Path.Combine(webroot, folder, image);
    //
    //     if (File.Exists(path))
    //     {
    //         File.Delete(path);
    //     }
    // }
}