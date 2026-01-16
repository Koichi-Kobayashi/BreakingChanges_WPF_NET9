using System;
using System.Collections;
using System.Windows;
using System.Windows.Markup;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        Title = "BC903 - XmlNamespaceMaps (net8.0-windows)";

        try
        {
            // Repro (documented):
            // In .NET 8, the backing store is Hashtable, but GetXmlNamespaceMaps tried to cast to string.
            var ht = new Hashtable { ["p"] = "urn:demo" };
            SetValue(XmlAttributeProperties.XmlNamespaceMapsProperty, ht);

            // This call is expected to throw InvalidCastException on .NET 8.
            _ = XmlAttributeProperties.GetXmlNamespaceMaps(this);

            StatusText.Text = "UNEXPECTED: GetXmlNamespaceMaps did not throw.";
        }
        catch (Exception ex)
        {
            StatusText.Text = "EXPECTED on .NET 8: " + ex.GetType().Name + "\n" + ex.Message;
        }
    }
}
