# ASP.NET Core JWT Authentication Örneği (JwtApp)

Bu proje, ASP.NET Core Web API uygulamalarında **JWT (JSON Web Token)** kullanarak token tabanlı kimlik doğrulama (authentication) ve yetkilendirme (authorization) mekanizmalarının nasıl kurulacağını gösteren bir örnek uygulamadır.

## Projenin Amacı

Projenin temel amacı, bir API'nin güvenliğini JWT kullanarak sağlamaktır. Bu, özellikle modern web ve mobil uygulamalarda, istemci (client) ile sunucu (server) arasında durum bilgisi tutulmayan (stateless), güvenli bir iletişim kanalı kurmak için yaygın olarak kullanılan bir yöntemdir.

Proje aşağıdaki temel işlevleri kapsar:
* Kullanıcı kaydı ve girişi.
* Başarılı bir giriş işlemi sonrasında kullanıcıya özel bir JWT üretilmesi.
* API üzerindeki belirli endpoint'lerin `[Authorize]` attribute'u ile korunması.
* Rol tabanlı yetkilendirme (`[Authorize(Roles = "Admin")]`) ile farklı kullanıcı rollerine farklı erişim hakları tanınması.

## Nasıl Çalışır?

1.  **Giriş İsteği:** İstemci, kullanıcı adı ve şifre gibi kimlik bilgilerini API'de bulunan halka açık bir giriş endpoint'ine (örn: `/api/auth/login`) gönderir.
2.  **Kimlik Doğrulama:** Sunucu, veritabanındaki bilgilerle gelen kimlik bilgilerini doğrular.
3.  **JWT Üretimi:** Kimlik bilgileri doğru ise, sunucu kullanıcıya özel bilgileri (kullanıcı ID, rol vb.) içeren ve gizli bir anahtar (secret key) ile imzalanmış bir JWT oluşturur.
4.  **Token'ın İletilmesi:** Oluşturulan JWT, istemciye bir yanıt olarak geri gönderilir.
5.  **Token ile Erişim:** İstemci, bu token'ı saklar ve korumalı API endpoint'lerine yapacağı sonraki tüm isteklerin `Authorization` başlığına (Header) `Bearer <token>` formatında ekler.
6.  **Token Doğrulama ve Yetkilendirme:** API, gelen her istekte bu token'ı alır, geçerliliğini (imzasını, süresini vb.) kontrol eder ve token içindeki bilgilere göre kullanıcının istenen kaynağa erişim yetkisi olup olmadığına karar verir.

## Proje Yapısı

* **Controllers:**
    * `AuthController` (veya benzeri): Kullanıcı girişi ve token üretiminden sorumludur.
    * Diğer Controller'lar (`ProductsController` vb.): `[Authorize]` attribute'u ile korunan ve sadece geçerli bir token ile erişilebilen iş mantığını içerir.
* **Entities:** `User` ve `Role` gibi veritabanı varlıklarını içerir.
* **DataAccess/Persistence:** Entity Framework Core `DbContext` sınıfını ve veritabanı konfigürasyonlarını barındırır.
* **JWT Ayarları (`Program.cs` / `Startup.cs`):**
    * Authentication servislerinin (`AddAuthentication`) ve JWT Bearer ayarlarının (`AddJwtBearer`) yapıldığı yerdir.
    * Token'ın nasıl doğrulanacağına dair kuralları (`TokenValidationParameters`) içerir.
* **appsettings.json:** JWT'nin `Issuer` (sağlayıcı), `Audience` (hedef kitle) ve en önemlisi `SecretKey` (gizli anahtar) gibi hassas bilgilerini barındırır.

## Kullanılan Teknolojiler

* **Backend:** ASP.NET Core
* **Kimlik Doğrulama:** JSON Web Tokens (JWT)
* **Veri Erişimi:** Entity Framework Core

## Kurulum ve Çalıştırma

Projeyi yerel makinenizde çalıştırmak için aşağıdaki adımları izleyin:

1.  **Repoyu Klonlayın:**
    ```sh
    git clone [https://github.com/SametDulger/JwtApp.git](https://github.com/SametDulger/JwtApp.git)
    ```

2.  **Proje Dizinine Gidin:**
    ```sh
    cd JwtApp
    ```

3.  **JWT Ayarlarını Yapılandırın:**
    `appsettings.json` dosyasını açın ve `Jwt` bölümündeki `Issuer`, `Audience` ve `SecurityKey` alanlarını kendi projenize uygun şekilde doldurun. **Güvenlik için `SecurityKey` değerini karmaşık bir dize yapmanız önemlidir.**

4.  **Veritabanı Bağlantısını Yapılandırın:**
    Yine `appsettings.json` dosyasında bulunan `ConnectionStrings` bölümünü kendi yerel veritabanınıza göre güncelleyin.

5.  **Veritabanını Oluşturun (Migrations):**
    Package Manager Console üzerinden veya `dotnet cli` kullanarak veritabanı migration'larını çalıştırın.
    ```sh
    dotnet ef database update
    ```

6.  **Uygulamayı Çalıştırın:**
    Projeyi Visual Studio üzerinden başlatın veya aşağıdaki komutu kullanın:
    ```sh
    dotnet run
    ```

### API'yi Test Etme

API'yi test etmek için Postman veya Swagger gibi bir araç kullanabilirsiniz:
1.  Önce `/api/auth/login` gibi bir endpoint'e POST isteği atarak bir token alın.
2.  Aldığınız token'ı kopyalayın.
3.  Korumalı bir endpoint'e (örn: GET `/api/products`) istek yaparken, `Authorization` header'ına `Bearer [kopyaladığınız_token]` değerini ekleyin.

