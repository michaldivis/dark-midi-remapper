using Microsoft.Extensions.Configuration;
using System.Windows;

namespace DarkMidiRemapperDesktop;
public partial class App : Application
{
    public App()
    {
        RegisterSyncfusion();
    }

    private void RegisterSyncfusion()
    {
        var builder = new ConfigurationBuilder();
        builder.AddUserSecrets<App>();
        var configuration = builder.Build();

        var licenseKey = configuration["SyncfusionLicenseKey"];
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(licenseKey);
    }
}
