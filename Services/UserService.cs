using IOT.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using IOT.Helper;

namespace IOT.Services
{

public class UserService
{
    IOTContext _context;

    public UserService(IOTContext context)
    {
        _context=context;
    }

    public Users Authenticate(string username,string password)
    {
        return _context.Users.FirstOrDefault(x=>x.Username==username && x.Password==password);//#warning always encrypt your password!
    }

    public async Task<Users> SignUp(Users user)
    {
        user.RegisterDate=DateTime.Now;
        user.Type=(byte)MyEnums.UserTypes.ADMIN;
        user.Status=(byte)MyEnums.UserStatus.ACTIVE;
        user.Username=user.Username.ToLower();
        //user.Password = hash user.Password
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;

    }
    
}
}