namespace DentalClinicManagement.DomainLayer.Entities
{
    public class PasswordResetToken
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // معرف لكل طلب
        public string Email { get; set; } = string.Empty; // الإيميل المرتبط بالتوكن
        public string Token { get; set; } = string.Empty; // التوكن نفسه (كود عشوائي)
        public DateTime ExpirationTime { get; set; } // متى ينتهي التوكن
        public bool IsUsed { get; set; } = false; // هل تم استخدامه بالفعل؟
    }
}
