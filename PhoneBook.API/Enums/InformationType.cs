using System.ComponentModel;

namespace PhoneBook.API.Enums
{
    public enum InformationType
    {
        [Description("Telefon Numarası")]
        PhoneNumber,
        [Description("E-Mail Adresi")]
        EmailAddress,
        [Description("Konum Bilgisi")]
        Location
    }
}
