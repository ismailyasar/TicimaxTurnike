# TicimaxTurnike
Ticimax Turnike Projesi

Turnike giriş ve çıkışlarını sisteme ileten bir API barındırır. API içerisinde aşağıdaki endpointler bulunmaktadır. Projede postgresql veritabanı ve RabbitMQ kullanılmıştır. Kullanıcının o gün için her giriş ve çıkışı RabbitMQ ya gönderilerek ve ayrı bir arkaplan servisi ile veritabanında tutulur. Projede ORM olarak Entity framework kullanılmıştır. 

api/movements/Enter
Bir kullanıcı için giriş kaydı ekler. 

api/movements/Exit
Bir kullanıcı için çıkış kaydı ekler. 

api/movements?personId=1&startDate=2023-07-26&endDate=23-07-27
Belirtilen kullanıcı için veriler tarih aralıklarındaki hareketleri listeler. Parametreler zorunlu değildir. Eklendikçe işlenir.

api/movements/reports?personId=1&startDate=2023-07-26&endDate=23-07-27
Belirtilen kullanıcı için verilen tarih arasındaki giriş ve çıkışları getirir. Burada bir tarihteki kullanıcının ilk girişi ile son çıkışı yer alır. 



