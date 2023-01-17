using eShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("Contacts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Email).IsRequired().IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.PhoneNumber).HasMaxLength(15).IsRequired();
            builder.Property(x => x.Message).IsRequired();
        }
    }
}
