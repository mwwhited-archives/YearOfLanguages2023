namespace HashEm.Persistence;

public class ImageFile
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string RealativePath { get; set; } = null!;
    public string FileName { get; set; } = null!;
    public string Extension { get; set; } = null!;
    public string Hash { get; set; } = null!;
    public string PathHash { get; set; } = null!;
}
