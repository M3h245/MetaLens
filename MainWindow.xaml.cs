using MetaLens.Models;
using MetaLens.Services;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace MetaLens;

public partial class MainWindow : Window
{
    private ObservableCollection<ImageItem> Images = new();

    private MetadataEngine _engine = new();
    private MetadataNormalizer _normalizer = new();
    private MetadataCategorizer _categorizer;

    private Dictionary<string, string> _map;

    public MainWindow()
    {
        InitializeComponent();
        Loaded += MainWindow_Loaded;

        //Images.Add(new ImageItem(@"C:\Images\Image1.png"));
        //Images.Add(new ImageItem(@"C:\Images\Image2.png"));

        _map = LoadMap();
        _categorizer = new MetadataCategorizer(_map);

        ImageList.ItemsSource = Images;
        ImageList.SelectionChanged += ImageList_SelectionChanged;
    }

    private Dictionary<string, string> LoadMap()
    {
        var json = File.ReadAllText("metadata-map.json");
        return JsonSerializer.Deserialize<Dictionary<string, string>>(json)!;
    }

    private void ImageList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selected = ImageList.SelectedItem as ImageItem;
        if (selected == null) return;

        var raw = _engine.Extract(selected.FilePath);
        var clean = _normalizer.Normalize(raw);
        var grouped = _categorizer.Categorize(clean);

        selected.Metadata.Clear();

        foreach (var category in grouped)
        {
            selected.Metadata[category.Key] = category.Value
        .Select(x => new KeyValuePair<string, string>(x.Key, x.Value))
        .ToList();
        }

        RenderMetadata(selected);
    }

    private void AddMetadataRow(string key, string value)
    {
        var row = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Margin = new Thickness(0, 3, 0, 3)
        };



        var btn = new Button
        {
            Content = "⧉",
            Tag = value,
            ToolTip = "Copy Value",
            Background = Brushes.Transparent,
            BorderBrush = Brushes.Transparent,
            Foreground = Brushes.White,
            Padding = new Thickness(5,0 ,5,0),
            Margin = new Thickness(0, 0, 5, 0)

        };

        btn.Click += (s, e) =>
        {
            Clipboard.SetText(value);
        };

        row.Children.Add(btn);
        row.Children.Add(new TextBlock
        {
            Text = $"{key}: {value}",
            Width = 260,
            Foreground = Brushes.White
        });
        MetadataPanel.Children.Add(row);
    }

    private void CopySingle_Click(object sender, RoutedEventArgs e)
    {
        var selected = ImageList.SelectedItem as ImageItem;
        if (selected == null) return;

        var sb = new StringBuilder();

        foreach (var category in selected.Metadata)
        {
            sb.AppendLine(category.Key);

            foreach (var item in category.Value)
            {
                sb.AppendLine($"- {item.Key}: {item.Value}");
            }

            sb.AppendLine();
        }

        Clipboard.SetText(sb.ToString());
    }
    private void RenderMetadata(ImageItem selected)
    {
        MetadataPanel.Children.Clear();

        foreach (var category in selected.Metadata)
        {
            // Category title
            MetadataPanel.Children.Add(new TextBlock
            {
                Text = category.Key,
                FontSize = 16,
                Margin = new Thickness(0, 10, 0, 5),
                Opacity = 0.8,
                Foreground = Brushes.White
            });

            // Items
            foreach (var item in category.Value)
            {
                AddMetadataRow(item.Key, item.Value);
            }
        }
    }

    private void Window_DragOver(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            DropOverlay.Visibility = Visibility.Visible;
            e.Effects = DragDropEffects.Copy;
        }
    }

    private void Window_Drop(object sender, DragEventArgs e)
    {
        DropOverlay.Visibility = Visibility.Collapsed;

        if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            return;

        var files = (string[])e.Data.GetData(DataFormats.FileDrop);

        foreach (var file in files)
        {
            if (IsImage(file))
                Images.Add(new ImageItem(file));
        }
    }
    private bool IsImage(string path)
    {
        var ext = Path.GetExtension(path).ToLower();

        return ext is ".jpg" or ".jpeg" or ".png" or ".bmp" or ".tiff" or ".webp";
    }
  

    private void Clear_Click(object sender, RoutedEventArgs e)
    {
        if (MessageBox.Show(
         "Clear all images?",
         "Confirm",
         MessageBoxButton.YesNo,
         MessageBoxImage.Question) == MessageBoxResult.Yes)
        {
            Images.Clear();
            MetadataPanel.Children.Clear();
            ImageList.SelectedItem = null;
        }
    }
    private void DropOverlay_Leave(object sender, DragEventArgs e)
    {
        DropOverlay.Visibility = Visibility.Collapsed;
    }
    private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            DragMove();
        }
    }
    private void Minimize_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        var screenWidth = SystemParameters.PrimaryScreenWidth;
        var screenHeight = SystemParameters.PrimaryScreenHeight;

        Width = screenWidth * 0.7;  
        Height = screenHeight * 0.7;

        Left = (screenWidth - Width) / 2;
        Top = (screenHeight - Height) / 2;
    }
}