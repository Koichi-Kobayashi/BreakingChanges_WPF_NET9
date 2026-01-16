using System.Collections;
using System.Windows;
using System.Windows.Markup;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        Title = "BC903 - XmlNamespaceMaps (net9.0-windows)";

        // In .NET 9, backing store and API type are Hashtable.
        var ht = new Hashtable { ["p"] = "urn:demo" };

        // .NET 9: SetXmlNamespaceMaps now accepts Hashtable.
        XmlAttributeProperties.SetXmlNamespaceMaps(this, ht);

        var maps = XmlAttributeProperties.GetXmlNamespaceMaps(this);

        StatusText.Text = "OK on .NET 9: no InvalidCastException.\n" +
                          $"Maps type: {maps.GetType().Name}, Count={maps.Count}";
    }
}
