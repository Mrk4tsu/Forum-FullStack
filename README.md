### REPOSITORY: WEBSITE COMUNITY DRAGON BOY BY GATAPCHOI AKA MRKATSU
#### Visit the website: https://gavl.io.vn
#### Overview:
This website was created as a personal challenge to evaluate my capabilities in building a FullStack project from scratch, while also reinforcing my skills in Angular and ASP.NET Core.

This project utilizes the following technologies:

1. Back End (Core, Libraries, and Packages):
   - ASP.NET Core 8
   - Entity Framework Core
   - Identity.EntityFrameworkCore
   - FluentValidation
   - StackExchange.Redis
   - MailJet.API
   - Microsoft.AspNetCore.Authentication.JwtBearer 8
   - Microsoft.EntityFrameworkCore.Design 8
   - Microsoft.EntityFrameworkCore.Tools 8
   - Pomelo.EntityFrameworkCore.MySql 8
2. Front End (CSS & Libraries):
   - Angular 19 (Standalone Components)
   - Bootstrap
   - highlight.js
   - CKEditor 5
   - ngx-cookie-service
   - jwt-decode
   - @ckeditor/ckeditor5-angular
3. Databases:
   - MySQL
   - Redis
#### Usage:
Back End: For security reasons, the appsettings.json file is not committed to the repository. Below is a sample template:
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
In the environment configuration, the captchaKey must match the Cloudflare SiteKey used in the back end. While the SiteKey on the back end is not strictly required, it is essential on the front end to validate requests via the server.

----------------------------------------------   
## Vietnamese
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

