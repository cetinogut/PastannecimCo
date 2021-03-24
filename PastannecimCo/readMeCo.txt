
Twilio Sandbox for WhatsApp Messaging kullanarak program geliştirebiliriz.
Bunun için tunneling kullanmamız lazom. Yani localhost ta çalışan programımızın global bir adresi olması gerekiyor.
 Bunu da NGROK bize sağlıyor. NGROK ile 2-3 saatlik uygulama http adresleri üretip bunları client programlarda kullanabiliyoruz. 
 
 999_ProgrammingTools altında NGROK'un exesini çalıştırıp aşağıdaki konmutu yazınca bize http ve https adresleri üretecek.
 
 ngrok http 49086 -host-header="localhost:49086"
 
 Yukarıda 49086 port numarasıdır ve VS'de projemizin çalıştığı http port nodur. Launchsettings.jsondan bak.
   
  http://869a20020f4b.ngrok.io -> http://localhost:49086  
  https://869a20020f4b.ngrok.io -> http://localhost:49086
  
   
   veya 
  https://localhost:5001/ bu kestrel de çalışan localhostumuz. Buna NGrok'da tunnel üretmek için
  
  ngrok http https://localhost:5001 yazınca aşağıdaki adres iüretiyor. 
 
  http://5990de7027e4.ngrok.io // bu adress 2-3 saatte doluyor değiştirmek lazım

  
  bu adreslerde nhttp olanını TwilioStudio'da HttpRequest widgetindeki url adresine yazıyoruz.
  Whatsapp a müşterinin girdiği ürün tercih bilgileri jSON object olarak aşağıdaki formatta .net core da 
  geliştirdiğimiz projenni apisine gelecek.
 http://869a20020f4b.ngrok.io/api/order diye eklemeyi unutma


Twilio'dan gönderilen JSON mesajı


{
"number":"{{trigger.message.From}}",
"name":"{{widgets.AskNameCo.inbound.Body}}",
"email":"{{widgets.AskEmailCo.inbound.Body}}",
"cake": {
          "topping":"{{widgets.AskToppingCo.inbound.Body}}",
          "frosting":"{{widgets.AskCakeFrostingCo.inbound.Body}}",
           "flavour":"{{widgets.AskCakeFlavourCo.inbound.Body}}",
            "size":"{{widgets.WelcomeCo.inbound.Body}}",
            "price":"{{widgets.GetPriceCo.parsed.total}}"
         }
}

https://www.twilio.com/console/sms/whatsapp/sandbox
Sandbox Participants
Invite your friends to your Sandbox. Ask them to send a 
WhatsApp message to +1 415 523 8886 with code "join meat-thousand".

join meat-thousand twilionun bana özel ürettiği bir kod ve bu marketten 
alışveriş yapmak için bu kelime grubunu whatsapp dan ütye olmak istenilen numaraya gönermek gerekiyor.
