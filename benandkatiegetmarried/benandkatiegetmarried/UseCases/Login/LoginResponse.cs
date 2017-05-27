using System;

namespace benandkatiegetmarried.UseCases.Login
{
    public class LoginResponse
    {
        public Guid Id { get; set; }
        public bool IsValid { get; set; }
    }
}