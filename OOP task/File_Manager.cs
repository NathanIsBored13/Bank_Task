using System.IO;
using System.Text;

public class Reader
{
    private string root_file;
	public Reader(string Root_File)
	{
        this.root_file = Root_File;
        if (!Directory.Exists(Root_File)) Directory.CreateDirectory(Root_File);
    }
    public string Get_Path() => root_file;
    public string[] Get_Contents() => Directory.GetFiles(root_file, "*.csv");
    public string[] ReadFile(string file)
    {
        FileStream FS = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.None);
        byte[] buffer = new byte[FS.Length];
        FS.Read(buffer, 0, (int)FS.Length);
        FS.Close();
        string[] ret = Encoding.Default.GetString(buffer).Split('\n');
        return ret;
    }
}
