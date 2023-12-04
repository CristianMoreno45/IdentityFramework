using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSoft.MsIdentity.Infrastructure.Repositories.Contexts
{
    public class MsIdentityDbContext: IdentityDbContext
    {
        public MsIdentityDbContext(DbContextOptions options): base(options)
        {

        }
    }
}
