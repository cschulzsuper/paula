﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Super.Paula.Application.Communication;

namespace Super.Paula.Data.Mappings.Communication
{
    public class NotificationMapping : IEntityTypeConfiguration<Notification>
    {
        public string PartitionKey = nameof(PartitionKey);

        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder
                .Property<string>(PartitionKey)
                .HasValueGenerator<NotificationPartitionKeyValueGenerator>();

            builder
                .HasKey(PartitionKey,
                    nameof(Notification.Date),
                    nameof(Notification.Time));

            builder
                .ToContainer(nameof(Notification))
                .HasPartitionKey(PartitionKey)
                .HasNoDiscriminator();

            builder
                .Property(x => x.Target)
                .HasMaxLength(140)
                .IsRequired();

            builder
                .Property(x => x.Text)
                .HasMaxLength(140)
                .IsRequired();

            builder
                .Property(x => x.Date)
                .IsRequired();

            builder
                .Property(x => x.Time)
                .IsRequired();
        }
    }
}