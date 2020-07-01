using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Clients.Base;
using WebStore.Domain;
using WebStore.Interfaces.Services.Identity;

namespace WebStore.Clients.Identity
{
    public class RolesClient : BaseClient, IRolesClient
    {
        public RolesClient(IConfiguration Configuration) : base(Configuration, WebApi.Identity.Roles) { }
        
        
    }
}
