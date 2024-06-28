## Controller
- Là một lớp kế thừa từ lớp Controller: Microsoft.AspNetCore.Mvc.Controller
- Action trong controller là 1 phương thức public (không được static)
- Action trả về bất cứ dữ liệu nào, thường là IActionResult
- Các dịch vụ inject vào controller qua hàm tạo 
## View
- Là file .cshtml
- View cho Action lưu tại: /View/ControllerName.Action.cshtml
- Thêm thư mục lưu trữ View: 
    //{0} -> Tên Action
    //{1} -> Tên Controller
    //{2} -> Tên Area
    options.ViewLocationFormats.Add("/MyView/{1}/{0}" + RazorViewEngine.ViewExtension);

## Truyền dữ liệu sang view
- Model
- ViewData
- ViewBag
- TemData