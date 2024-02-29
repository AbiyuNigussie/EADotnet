namespace EventAddis.Repository
{
    public interface IFileService
    {
        public Tuple<int, string> SaveImage(IFormFile imageFile, string subDirectory);
        public bool DeleteImage(string imageFileName, string subDirectory);

    }
}
