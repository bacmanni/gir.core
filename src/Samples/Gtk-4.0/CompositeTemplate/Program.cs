using System;
using System.Reflection;
using CompositeTemplate;

var application = Gtk.Application.New("org.gir.core", Gio.ApplicationFlags.FlagsNone);
application.OnActivate += (sender, args) =>
{
    Console.WriteLine("Resources:");
    var names = Assembly.GetExecutingAssembly().GetManifestResourceNames();
    foreach (var name in names)
        Console.WriteLine(name);
    
    var window = Gtk.ApplicationWindow.New((Gtk.Application) sender);
    window.Title = "Gtk4 Window";
    window.SetDefaultSize(300, 300);
    window.Child = CompositeBoxWidget.NewWithProperties([]);
    window.Show();
};
return application.RunWithSynchronizationContext(null);
