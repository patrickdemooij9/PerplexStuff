namespace Perplex._2FA.Models.Business
{
    public class TwoFactorAuthInfo
    {
        public string Secret { get; set; }
        public string Email { get; set; }
        public string ApplicationName { get; set; }

        public string QrCodeSetupImageUrl { get; set; }
    }
}
