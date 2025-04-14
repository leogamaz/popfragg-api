﻿using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Reflection;
using System.Web;


namespace popfragg.Helper
{
    public static class HttpRequests
    {
        public static CookieOptions SetCookieOptions(int minutes)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true, // Protege contra acesso via JavaScript
                Secure = false, // Apenas envia em HTTPS
                IsEssential = true, // Garante envio do cookie mesmo sem consentimento do usuário
                Expires = DateTime.UtcNow.AddMinutes(minutes), // Expiração em minutos
            };
            return cookieOptions;
        }

        public static T CreatePayloadWithQueryParams<T>(IQueryCollection queryParams) where T : new()
        {
            T model = new();
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            

            foreach (var prop in properties)
            {
                // Procura uma chave na query que seja igual ao nome da propriedade (ignorando caixa)
                var key = queryParams.Keys.FirstOrDefault(x => CompararNomesChaves(x, prop.Name));
                if (!string.IsNullOrEmpty(key))
                {
                    var value = queryParams[key].ToString();

                    try
                    {
                        object convertedValue = value;
                        if (prop.PropertyType != typeof(string))
                        {
                            convertedValue = Convert.ChangeType(value, prop.PropertyType);
                        }
                        prop.SetValue(model, convertedValue);
                    }
                    catch
                    {
                        // Trate o erro de conversão, se necessário
                    }
                }
            }
            return model;
        }

        private static bool CompararNomesChaves(string chaveQueryParams, string nomePropriedade)
        {
            // Lógica de comparação personalizada
            return string.Equals(chaveQueryParams.Replace(".", "_"), nomePropriedade, StringComparison.OrdinalIgnoreCase);
        }

    }
}
