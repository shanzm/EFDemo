using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _008EFEntities.EnityConfig
{
    class NationConfig:EntityTypeConfiguration<Nation>
    {
        public NationConfig()
        {
            ToTable("T_Nations_008");
            Property(n => n.Name).HasMaxLength(30).IsRequired();

        }
    }
}