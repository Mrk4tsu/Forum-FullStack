### REPOSITORY: WEBSITE COMUNITY DRAGON BOY BY GATAPCHOI AKA MRKATSU
#### Truy cập trang web: https://gavl.io.vn

#### Tổng quan:
Trang web lập ra mục đích thử thách bản thân, xem trình độ bản thân thực hiện 1 dự án FullStack như thế nào. Cũng như củng cố kiến thức cho Angular và Asp.NET Core

Trong dự án, tôi sử dụng các công nghệ như sau:
1. Back end (Core, Thư viện và các package):
   - Asp.NET Core 8
   - EntityFrameworkCore
   - Identity.EntityFrameworkCore
   - FluentValidator
   - Stackchange.Redis
   - MailJet.API
   - Microsoft.AspnetCore.Authentication.Jwtbearer 8
   - Microsoft.EntityFrameworkCore.Design 8
   - Microsoft.EntityFrameworkCore.Tool 8
   - Pomelo.EntityFrameworkCore.Mysql 8
2. Front end (CSS):
   - Angular 19 Standalone
   - Bootstrap
   - highlight.js
   - ckeditor5
   - ngx-cookie-service
   - jwt-decode
   - @ckeditor/ckeditor5-angular
3. Database:
   - MySQL
   - Redis
#### Cách sử dụng:
  Back end: Vì lý do bảo mật nên các file appsettings.json của tôi sẽ không được commit lên. Dưới đây là mẫu cho appsettings.json
  ```json
{
  "ConnectionStrings": {
    "Redis": "connectionstring redis",
    "KatsuDB": "connectionstring mySQL"
  },
  "JwtSettings": {
    "Secret": "key siêu dài, tầm 32 kí tự",
    "Issuer": "www.yourdomain.vn",
    "Audience": "www.yourdomain.vn",
    "ExpiryMinutes": 30,
    "RefreshExpiryDays": 7
  },
  "MailJet": {
    "ApiKey": "apikey mailjet",
    "SecretKey": "secret key mailjet",
    "SenderEmail": "no-reply@gavl.io.vn",
    "SenderName": "Gà Tập Chơi Website"
  },
  "CloudflareTurnstile": {
    "SecretKey": "cloudflare key",
    "SiteKey": "site key cloude flare"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
  ```
Front End:
Trong phần environment, captchaKey sẽ giống với SiteKey Cloudflare của Back End, phần SiteKey phía Back end không quan trọng lắm nhưng ở Front End thì sẽ quan trọng, nó dùng để gửi yêu cầu tới server xác thực request
