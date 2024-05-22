using WpfFinal.DataBases;

namespace WpfFinal.Services;
public  class SaveChangesToFileService
{
    public static void Save()
    {
        var data = App.Container.GetInstance<AppDbContext>();
        data.SaveBooks();
        data.SaveUsersToFile();
    }
}
