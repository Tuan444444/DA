using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DA.Models;
using DA.Services;

namespace DA.Controllers;

public class HomeController : Controller
{
    
  private readonly IEmailService _emailService;

    public HomeController(IEmailService emailService)
    {
        _emailService = emailService;
    }
   
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  
    public async Task<IActionResult> SendEmail()
    {
        try
        {
            await _emailService.SendEmailAsync(
     "dmanh0364@gmail.com",
     "Thử nghiệm gửi từ ứng dụng ASP.NET Core",
     "<h2>Xin chào!</h2><p>Đây là email gửi tự động từ ứng dụng ASP.NET Core 8. Nội dung phong phú hơn để tránh bị spam.</p>");

        }
        catch (Exception ex)
        {
            ViewBag.Message = "❌ Gửi email thất bại: " + ex.Message;
        }
        return View();
    }

}
