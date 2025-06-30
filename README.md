# MultiShop Mikroservis E-Ticaret Projesi

## Genel Bakış

MultiShop, modern mikroservis mimarisiyle geliştirilen bir e-ticaret platformudur. Proje, her işlevsel alanı bağımsız bir mikroservis olarak ele alır ve ölçeklenebilir, sürdürülebilir bir altyapı sunmayı hedefler.

## Mevcut Mikroservisler

### 1. Catalog Microservice
- **Amaç:** Ürün, kategori, ürün detayları ve ürün görsellerinin yönetimi.
- **Teknolojiler:** .NET 8, MongoDB, AutoMapper
- **Temel Özellikler:**
  - Kategori CRUD işlemleri
  - Ürün CRUD işlemleri
  - Ürün detayları CRUD işlemleri
  - Ürün görselleri CRUD işlemleri

### 2. Discount Microservice
- **Amaç:** İndirim kuponlarının yönetimi.
- **Teknolojiler:** .NET 8, Dapper, SQL Server
- **Temel Özellikler:**
  - Kupon CRUD işlemleri
  - RESTful API mimarisi
  - Swagger/OpenAPI desteği

## Planlanan Mikroservisler

- **Basket:** Kullanıcı sepeti yönetimi
- **Cargo:** Kargo ve teslimat süreçleri
- **Comment:** Ürün ve sipariş yorumları
- **Images:** Ortak medya/görsel yönetimi
- **Message:** Bildirim ve mesajlaşma altyapısı
- **Order:** Sipariş yönetimi
- **Payment:** Ödeme işlemleri

## Proje Kurulumu

1. Gerekli .NET ve veritabanı ortamlarını kurun.
2. Her mikroservisin kendi `appsettings.json` dosyasını ve bağlantı ayarlarını yapılandırın.
3. Her mikroservisi bağımsız olarak çalıştırabilirsiniz.
4. Swagger arayüzü ile API uç noktalarını test edebilirsiniz.

## Katkı ve Geliştirme

- Proje halen geliştirme aşamasındadır.
- Yeni mikroservisler ve özellikler eklendikçe README güncellenecektir.
- Katkıda bulunmak için lütfen bir pull request oluşturun.