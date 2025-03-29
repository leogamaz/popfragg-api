using fromshot_api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fromshot_api.Domain.Interfaces.Repository;
using Supabase;
using Supabase.Interfaces;


namespace fromshot_api.Repositories.User
{
    public class UserRepository(Client supabaseClient, Client supabaseAdminClient) : IUserRepository
    {
        private readonly List<UserModel> _usuarios = [];
        private readonly Client _supabaseClient = supabaseClient;
        private readonly Client _supabaseAdminClient = supabaseAdminClient;
        //public UserModel ObterPorId(int id)
        //{

        //    return _usuarios.FirstOrDefault(u => u.Id == id);
        //}

        public async  Task<string> SignUp()
        {
            try
            {
                var user = new UserModel
                {
                    Nome = "Leo",
                    Username = "Skill",
                    Email = "teste@teste.com",
                    Status = "Ativo",
                    
                };

                var result = await _supabaseClient.From<UserModel>().Insert(user);
                var result2 = await _supabaseClient.From<UserModel>().Get();
                //logica para adicionar o supabase
                return "teste";
            }
            catch(Exception ex)
            {
                throw new Exception("Erro no KeyCloak " + ex);
            }
        }
    }
}
