# ⚽ Modern Futbol Veri ve Yönetim Ekosistemi

Bu platform; veri tutarlılığı, kullanıcı deneyimi ve estetik tasarımın merkezde olduğu, uçtan uca geliştirilmiş profesyonel bir **Futbol Yönetim ve İstatistik Sistemidir.** Proje, Premier League konsepti üzerine inşa edilmiş olup, hem son kullanıcıya zengin bir içerik sunar hem de yöneticilere veriyi en ince ayrıntısına kadar işleme imkanı tanır.

## 🛠️ Bu Projede Kullanılan Teknolojiler

Sistemin kararlılığı ve hızı için modern teknoloji yığını (stack) şu şekilde kurgulanmıştır:

- **Framework & Dil:** .NET 8.0 ve C# kullanılarak en güncel standartlarda geliştirilmiştir.
- **Mimari:** N-Tier Architecture (Katmanlı Mimari) ile kodun sürdürülebilirliği sağlanmıştır. Veri erişiminde **Repository** ve **Unit of Work** tasarım desenleri uygulanmıştır.
- **Veri Transferi:** Sistem genelinde **DTO (Data Transfer Object)** yapısı kullanılarak veritabanı güvenliği sağlanmış ve sadece gerekli veriler optimize edilerek taşınmıştır.
- **Veritabanı:** İlişkisel veri yönetimi için **MS SQL Server** tercih edilmiş, **Entity Framework Core (Code First)** ile veritabanı modellemesi yapılmıştır.
- **Arayüz (UI/UX):** **ASP.NET Core MVC** yapısı üzerinde, futbolun ruhuna uygun dinamik bir yapı kurulmuştur. Tasarım süreçleri **Claude AI** ile optimize edilmiş olup, tamamen responsive (mobil uyumlu) bir deneyim sunulmaktadır.

---

## 📸 Proje Detayları ve Modüller

### 🏠 Dijital Stadyum Girişi (Ana Sayfa)
Sistemin vitrini olan bu ekran, kullanıcıları en güncel skorlar ve öne çıkan haftalık özetlerle karşılar. Modern "Dark Mode" estetiği, kullanıcının odak noktasını doğrudan maç sonuçlarına ve canlı verilere çeker.
<img src="https://github.com/user-attachments/assets/76f602fe-3a63-4eca-8841-12ba8067306a" width="100%" alt="Default1" />

### 🏆 Lig Puan Durumu ve Rekabet
Takımların performans verilerini anlık olarak yansıtan bu tabloda; galibiyet, beraberlik, mağlubiyet ve averaj gibi kritik veriler dinamik olarak hesaplanır. Her maç sonucu, lig tablosundaki hiyerarşiyi otomatik olarak günceller.
<img src="https://github.com/user-attachments/assets/4a7c6855-44d2-4207-b4e9-2d27c9da3213" width="100%" alt="Ligler1" />

### 📅 Haftalık Fikstür ve Geri Sayım
Ligin gelecek haftalarındaki büyük randevular burada listelenir. Yaklaşan maçlar için entegre edilen geri sayım sayaçları, kullanıcıya maçın başlama vaktine ne kadar kaldığını saniye bazlı gösteren dinamik bir yapı sunar.
<img src="https://github.com/user-attachments/assets/5a180252-59f6-45c9-93d8-2b1115350cc7" width="100%" alt="Fixture1" />

### 📊 Detaylı Maç Analiz Odası
Bir maçın skorundan çok daha fazlasını sunan bu analiz ekranı; şut sayıları, kornerler ve pas isabet oranları gibi teknik verileri görselleştirir. DTO mimarisi sayesinde bu veriler veritabanından en hızlı şekilde çekilerek kullanıcıya sunulur.
<img src="https://github.com/user-attachments/assets/152e4279-0c2e-41ae-a6e4-49757938dca0" width="100%" alt="Detail1" />

### 🚀 Stratejik Yönetim Paneli (Dashboard)
Yöneticiler için geliştirilen bu kontrol merkezi, sistemin genel sağlığını raporlar. Kayıtlı oyuncu sayısı, toplam stadyum kapasitesi ve haftalık gol istatistikleri gibi veriler, Dashboard üzerinden grafiksel olarak takip edilebilir.
<img src="https://github.com/user-attachments/assets/a2479382-8def-4a72-860d-36aaad27a7fb" width="100%" alt="Dashboard1" />

### ⚙️ Maç Veri Yönetim Sistemi
Admin tarafında maçların durumlarını, skorlarını ve sürelerini yönetmeye yarayan merkezi birimdir. Bu panel üzerinden yapılan her güncelleme, kullanıcı arayüzündeki canlı skor tablolarını anında tetikler.
<img src="https://github.com/user-attachments/assets/e92677d8-a3a0-4165-9eee-18b5fe0813a1" width="100%" alt="AdminMatch1" />

### ⚽ Teknik İstatistik Operasyonları
Maçın bitiş düdüğüyle birlikte sisteme işlenen detaylı performans verileridir. Topla oynama yüzdesi ve faul sayıları gibi ince detaylar, bu modül aracılığıyla SQL Server üzerine en güvenli veri yollarıyla kaydedilir.
<img src="https://github.com/user-attachments/assets/6f342300-9bb1-4eda-9136-2eec9111a5c4" width="100%" alt="AdminMatchStatistic1" />

### ⏱️ Canlı Olay Akışı (Match Events)
Goller, sarı ve kırmızı kartlar ile oyuncu değişikliklerinin dakikasıyla beraber işlendiği bölümdür. Bu veriler, kullanıcı tarafındaki "maç hikayesi" bölümünü besleyen ana kaynakları oluşturur.
<img src="https://github.com/user-attachments/assets/a6867f8f-ebff-48a6-80c5-531003e3074a" width="100%" alt="AdminMatchEvent1" />

### 🏢 Takım ve Kulüp Yönetimi
Kulüplerin logolarından kurumsal renklerine kadar tüm kimlik bilgilerinin yönetildiği ekrandır. Takımların veritabanındaki varlıkları, diğer tüm modüllerle ilişkisel bir şekilde (Foreign Key) bağlanmıştır.
<img src="https://github.com/user-attachments/assets/fdf3b4cc-1747-4ab9-a04e-f9077b90de9a" width="100%" alt="AdminTeam1" />

### 🏗️ Stadyum ve Altyapı Veritabanı
Ligdeki maçların oynandığı stadyumların kapasite, şehir ve görsel bilgilerinin tutulduğu modüldür. Bu yapı, her maçın hangi atmosferde gerçekleştiğini sisteme tanıtan kritik bir veri setidir.
<img src="https://github.com/user-attachments/assets/2e80cfe0-588b-4528-b056-dd3e77e5eb1e" width="100%" alt="AdminStadium1" />

### 👤 Sporcu ve Kadro Havuzu
Ligde görev alan tüm profesyonel oyuncuların detaylı bilgilerini içeren yönetim ekranıdır. Oyuncuların takımlarıyla olan ilişkileri ve performans geçmişleri bu panel üzerinden modüler olarak kontrol edilir.
<img src="https://github.com/user-attachments/assets/afebceeb-2ace-48d8-8f15-78b493c24ea0" width="100%" alt="AdminPlayer1" />

### 🚫 Kreatif Hata Yönetimi (404)
"Top Saha Dışına Çıktı!" Standart yaklaşımların ötesine geçilerek tasarlanan bu hata sayfası, projenin futbol ruhunu her köşesinde hissettirdiğinin en büyük kanıtıdır. Kullanıcıyı sistemde tutan yaratıcı bir deneyim sunar.
<img src="https://github.com/user-attachments/assets/6c486806-ab52-425e-a2e5-d3cdb4c9880a" width="100%" alt="Error" />

---
**🎨 Tasarım Notu:** Bu projenin tema tasarımı ve görsel konsept çalışmaları **Claude AI** desteğiyle optimize edilmiştir.
