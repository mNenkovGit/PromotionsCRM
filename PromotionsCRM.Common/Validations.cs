namespace PromotionsCRM.Common
{
    public static class Validations
    {
        public const int CountryNameMinLen = 2;
        public const int CountryNameMaxLen = 170;

        public const int ClientNameMinLen = 2;
        public const int ClientNameMaxLen = 80;

        public const int EmailMinLen = 8;
        public const int EmailMaxLen = 80;

        public const int AddressMinLen = 8;
        public const int AddressMaxLen = 80;

        public const int PhoneNumberLength = 15;
        public const string PhoneNumberRegEx = @"^\+\d{12}$";

        public const int WebsiteMaxLen = 2048;
        public const int WebsiteMinLen = 11;

        public const int FirstNameMinLen = 2;
        public const int FirstNameMaxLen = 50;

        public const int LastNameMinLen = 2;
        public const int LastNameMaxLen = 50;

        public const int PromotionNameMinLen = 5;
        public const int PromotionNameMaxLen = 80;

        public const int ProductNameMinLen = 2;
        public const int ProductNameMaxLen = 50;

        public const int UserNameMinLen = 2;
        public const int UsernameMaxLen = 50;

        public const int PasswordMinLen = 6;
        public const int PasswordMaxLen = 50;
    }
}
