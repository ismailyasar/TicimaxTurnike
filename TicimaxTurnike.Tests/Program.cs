// See https://aka.ms/new-console-template for more information

using System.Runtime.InteropServices.JavaScript;

var dateString = "2023-07-26";
var date = DateTime.Parse(dateString);

var d2 = new DateTimeOffset();


var timezoneInfo = TimeZoneInfo.ConvertTimeToUtc(date);

var newDAte = DateTime.SpecifyKind(date, DateTimeKind.Utc);
Console.WriteLine("Hello, World: " + timezoneInfo.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
//2023-07-27T08:57:24.577Z