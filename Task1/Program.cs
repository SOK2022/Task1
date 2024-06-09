Console.WriteLine("Введите путь до папки");
string strDir = Console.ReadLine();
CleanFolder(strDir);
static void CleanFolder(string strDir)
{
    try
    {
        if (Directory.Exists(strDir))
        {
            string[] folders = Directory.GetDirectories(strDir);
            string[] files = Directory.GetFiles(strDir);
            if (files.Length > 0)
            {
                foreach (var file in files)
                {
                    if ((DateTime.Now - File.GetLastAccessTime(file)).TotalSeconds > TimeSpan.FromMinutes(30).TotalSeconds)
                    {
                        File.Delete(file);
                    }
                }
            }
            if (folders.Length > 0)
            {
                foreach (string folder in folders)
                {
                    CleanFolder(folder);
                    if (isFolderEmpty(Directory.GetParent(folder).FullName))
                    {
                        Directory.Delete(Directory.GetParent(folder).FullName);
                    }
                }
            }
            else
            {
                if (isFolderEmpty(strDir))
                {
                    Directory.Delete(strDir);
                }
            }
        }
        else
        {
            Console.WriteLine("Папка не существует или путь указан неверно");
        }
    }
    catch (Exception e)
    {
        Console.WriteLine("Отсутствуют права на доступ к папке");
    }

}

static bool isFolderEmpty(string strDir)
{
    string[] folders = Directory.GetDirectories(strDir);
    string[] files = Directory.GetFiles(strDir);
    if (folders.Length > 0 || files.Length > 0)
    {
        return true;
    }
    return false;
}