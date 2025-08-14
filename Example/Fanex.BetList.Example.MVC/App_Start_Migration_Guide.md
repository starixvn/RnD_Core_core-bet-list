# Migration Guide: App_Start → .NET 8

## Tổng quan
Khi convert từ .NET Framework sang .NET 8, các class trong thư mục `App_Start` được thay thế bằng cấu hình trong `Program.cs` và các công cụ hiện đại.

## 🔄 **Mapping chi tiết**

### 1. **WebApiConfig.cs** → **Program.cs**
```csharp
// Trước (.NET Framework)
public static void Register(HttpConfiguration config)
{
    config.Routes.MapHttpRoute(
        name: "DefaultApi",
        routeTemplate: "api/{controller}/{id}",
        defaults: new { id = RouteParameter.Optional }
    );
}

// Sau (.NET 8)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
app.MapControllers();
```

### 2. **RouteConfig.cs** → **Program.cs**
```csharp
// Trước (.NET Framework)
routes.MapRoute(
    name: "Default",
    url: "{controller}/{action}/{id}",
    defaults: new { controller = "BetList", action = "All", id = UrlParameter.Optional }
);

// Sau (.NET 8)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=BetList}/{action=All}/{id?}");
```

### 3. **BundleConfig.cs** → **libman.json + Build Tools**
```csharp
// Trước (.NET Framework)
bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));
bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

// Sau (.NET 8)
// Sử dụng libman.json để quản lý packages
// Sử dụng WebOptimizer hoặc BundlerMinifier để bundle
```

### 4. **FilterConfig.cs** → **Program.cs + Middleware**
```csharp
// Trước (.NET Framework)
filters.Add(new HandleErrorAttribute());

// Sau (.NET 8)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
```

## 🚀 **Các bước thực hiện**

### **Bước 1: Cập nhật Program.cs**
- Thêm `AddControllers()` và `AddEndpointsApiExplorer()`
- Cập nhật route pattern
- Thêm `MapControllers()` cho Web API

### **Bước 2: Cài đặt LibMan**
```bash
dotnet tool install -g Microsoft.Web.LibraryManager.Cli
libman restore
```

### **Bước 3: Cập nhật _Layout.cshtml**
- Thay đổi đường dẫn từ `~/lib/bootstrap/dist/` thành `~/lib/bootstrap/`
- Thêm các script và CSS mới

### **Bước 4: Xóa thư mục App_Start**
Sau khi đã migrate xong, có thể xóa thư mục `App_Start` vì không còn cần thiết.

## 📦 **LibMan Packages**
- **jquery@3.7.1**: jQuery core
- **jquery-ui@1.13.2**: jQuery UI components
- **bootstrap@5.3.2**: Bootstrap CSS framework
- **modernizr@3.12.0**: Feature detection

## ⚠️ **Lưu ý quan trọng**
1. **Global.asax.cs** → **Program.cs**
2. **Web.config** → **appsettings.json**
3. **packages.config** → **PackageReference**
4. **System.Web.Mvc** → **Microsoft.AspNetCore.Mvc**
5. **System.Web.Http** → **Microsoft.AspNetCore.Mvc**

## 🔍 **Kiểm tra sau migration**
- [ ] Web API routes hoạt động
- [ ] MVC routes hoạt động
- [ ] Static files được serve
- [ ] Client-side packages được load
- [ ] Error handling hoạt động
- [ ] Bundle và minification hoạt động (nếu sử dụng)
