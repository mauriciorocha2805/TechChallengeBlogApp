﻿namespace TechChallengeBlogWebApi.Interfaces
{
    public interface ITokenService
    {
        Task<string> GerarTokenAsync(string chave);
    }
}