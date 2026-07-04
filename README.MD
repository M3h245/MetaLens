
# MetaLens

MetaLens is a lightweight Windows desktop tool built with WPF (.NET) for viewing images and extracting metadata (EXIF).

---

## 🚀 Features (v1)

- Drag & Drop image support  
- Image thumbnail list  
- EXIF / metadata viewer  
- Organized metadata categories  
- One-click copy of metadata values  
- Clear image list  
- Custom dark UI  
- Custom title bar (minimize / close)  
- Responsive layout  

---

## 🧠 Metadata System

MetaLens uses a mapping file to organize raw EXIF metadata into readable categories.

### File:
metadata-map.json

### What it does:
- Groups raw EXIF tags into readable names  
- Normalizes inconsistent metadata keys  
- Makes metadata easier to browse  
- Fully editable without changing code  

### Example:

{
  "Make": "Camera Brand",
  "Model": "Camera Model",
  "DateTimeOriginal": "Capture Date"
}

---

## 🖼 Preview

![MetaLens Preview](assets/preview.png)

---

## 🛠 Tech Stack

- .NET (WPF)
- C#
- XAML

---

## 📦 How to Run

1. Clone repository:

git clone https://github.com/M3h245/MetaLens.git

2. Open solution in Visual Studio  
3. Restore NuGet packages (if needed)  
4. Run project  

---

## 💡 Planned Features

- Image preview panel  
- Search in metadata  
- Export metadata to JSON / TXT  
- Better drag & drop UX  
- Performance optimization  
- Advanced metadata engine  

---

## 📄 License

MIT License

---

## ☕ Support

If you like this project:

https://buymeacoffee.com/m3h245

Thanks for your support ❤️