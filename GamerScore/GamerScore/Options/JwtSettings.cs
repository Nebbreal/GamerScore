﻿using Gamerscore.Core.Interfaces;

namespace GamerScore.Options
{
    public class JwtSettings : IJwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
