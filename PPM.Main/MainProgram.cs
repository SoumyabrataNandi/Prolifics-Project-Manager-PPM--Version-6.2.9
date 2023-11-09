using PPM.Ui.Consoles;
using PPM.Domain;

namespace PPM.Main
{
    public class MainProgram
    {
        static MainMenuManager mainMenuManager = new();
        static void Main(string[] args)
        {
            mainMenuManager.MainMenu();
        }
    }
}