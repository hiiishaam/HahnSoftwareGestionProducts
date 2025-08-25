namespace JwtAuthDemo.Models
{
    /// <summary>
    /// login model for user login request payload 
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// prperty for user email 
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// property for user password  should be hashed in real application
        /// </summary>
        public string Password { get; set; }
    }
}
