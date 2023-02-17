using System.Globalization;

namespace ASPNET7LIVE.Services.ThaiDate
{
    public class ThaiDate : IThaiDate
    {
        public string ShowThaiDate()
        {
            return DateTime.Now.ToString("dd MMM yyyy", new CultureInfo("th-TH"));
        }
    }
}
