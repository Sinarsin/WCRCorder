using System.Windows.Forms;
using WCRCorder.Services;

namespace WCRCorder.Hosting;

public sealed class TrayApplicationContext : ApplicationContext
{
    private readonly ApplicationService _application;

    public TrayApplicationContext(ApplicationService application)
    {
        _application = application;
    }
}