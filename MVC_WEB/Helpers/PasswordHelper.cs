using System;
using System.Security.Cryptography;
using System.Text;

namespace MVC_WEB.Helpers
{
    public static class PasswordHelper
    {
        // Hash mật khẩu
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                // Tạo byte từ mật khẩu và mã hóa
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var hashedBytes = sha256.ComputeHash(passwordBytes);
                // Chuyển đổi byte array thành chuỗi hex
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        // Kiểm tra mật khẩu so với hash đã lưu
        public static bool VerifyPassword(string hashedPassword, string password)
        {
            // Hash mật khẩu nhập vào và so sánh với hash đã lưu
            var hashOfInputPassword = HashPassword(password);
            return hashOfInputPassword.Equals(hashedPassword, StringComparison.OrdinalIgnoreCase);
        }
    }
}
