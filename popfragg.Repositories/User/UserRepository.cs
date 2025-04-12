using fromshot_api.Domain.Entities;
using System;
using System.Threading.Tasks;
using fromshot_api.Domain.Interfaces.Repository;



namespace fromshot_api.Repositories.User
{
    public class UserRepository() : IUserRepository
    {
        //public UserModel ObterPorId(int id)
        //{

        //    return _usuarios.FirstOrDefault(u => u.Id == id);
        //}

        //public async  Task<string> SignUp()
        //{
        //    try
        //    {
        //        var user = new UserModel
        //        {
        //            Name = "Leo",
        //            Username = "Skill",
        //            Email = "teste@teste.com",
        //            Status = "Ativo",
                    
        //        };
               
        //        //logica para adicionar o supabase
        //        return "teste";
        //    }
        //    catch(Exception ex)
        //    {
        //        throw new Exception("Erro no KeyCloak " + ex);
        //    }
        //}
    }
}
