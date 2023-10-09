namespace Hotel.Models.Shared
{
    public class CommonMethod
    {

        public static async Task<string> uploadImage(IFormFile file)
        {
            try
            {
                string FileNameConvert = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString() + new Random().Next();
                var FileType = Path.GetExtension(file.FileName);


                var FileURL = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", $"{FileNameConvert}{FileType}");
                using (var stream = new FileStream(FileURL, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return FileNameConvert + FileType;
            }
            catch (Exception ex)
            {
                return "false";
            }


        }
    }
}
