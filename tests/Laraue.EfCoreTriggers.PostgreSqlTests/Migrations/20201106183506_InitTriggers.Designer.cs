﻿// <auto-generated />
using System;
using Laraue.EfCoreTriggers.SqlTests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Laraue.EfCoreTriggers.PostgreSqlTests.Migrations
{
    [DbContext(typeof(NativeDbContext))]
    [Migration("20201106183506_InitTriggers")]
    partial class InitTriggers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Laraue.EfCoreTriggers.SqlTests.Entities.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsVeryfied")
                        .HasColumnName("is_veryfied")
                        .HasColumnType("boolean");

                    b.Property<Guid>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Value")
                        .HasColumnName("value")
                        .HasColumnType("numeric");

                    b.HasKey("Id")
                        .HasName("pk_transactions");

                    b.HasIndex("UserId")
                        .HasName("ix_transactions_user_id");

                    b.ToTable("transactions");

                    b.HasAnnotation("LC_TRIGGER_AFTER_DELETE_TRANSACTION", "CREATE FUNCTION LC_TRIGGER_AFTER_DELETE_TRANSACTION() RETURNS trigger as $LC_TRIGGER_AFTER_DELETE_TRANSACTION$ BEGIN IF OLD.is_veryfied is true THEN UPDATE balances SET balance = balances.balance - OLD.value WHERE balances.user_id = OLD.user_id;END IF;RETURN NEW; END;$LC_TRIGGER_AFTER_DELETE_TRANSACTION$ LANGUAGE plpgsql;CREATE TRIGGER LC_TRIGGER_AFTER_DELETE_TRANSACTION AFTER DELETE ON transactions FOR EACH ROW EXECUTE PROCEDURE LC_TRIGGER_AFTER_DELETE_TRANSACTION();");

                    b.HasAnnotation("LC_TRIGGER_AFTER_INSERT_TRANSACTION", "CREATE FUNCTION LC_TRIGGER_AFTER_INSERT_TRANSACTION() RETURNS trigger as $LC_TRIGGER_AFTER_INSERT_TRANSACTION$ BEGIN IF NEW.is_veryfied is true THEN INSERT INTO balances (user_id, balance) VALUES (NEW.user_id, NEW.value) ON CONFLICT (user_id) DO UPDATE SET balance = balances.balance + NEW.value;END IF;RETURN NEW; END;$LC_TRIGGER_AFTER_INSERT_TRANSACTION$ LANGUAGE plpgsql;CREATE TRIGGER LC_TRIGGER_AFTER_INSERT_TRANSACTION AFTER INSERT ON transactions FOR EACH ROW EXECUTE PROCEDURE LC_TRIGGER_AFTER_INSERT_TRANSACTION();");

                    b.HasAnnotation("LC_TRIGGER_AFTER_UPDATE_TRANSACTION", "CREATE FUNCTION LC_TRIGGER_AFTER_UPDATE_TRANSACTION() RETURNS trigger as $LC_TRIGGER_AFTER_UPDATE_TRANSACTION$ BEGIN IF OLD.is_veryfied is true && NEW.is_veryfied is true THEN UPDATE balances SET balance = balances.balance + NEW.value - OLD.value WHERE balances.user_id = OLD.user_id;END IF;RETURN NEW;IF !OLD.is_veryfied && NEW.is_veryfied THEN UPDATE balances SET balance = balances.balance + NEW.value WHERE balances.user_id = OLD.user_id;END IF;RETURN NEW;IF OLD.is_veryfied && !NEW.is_veryfied THEN UPDATE balances SET balance = balances.balance - OLD.value WHERE balances.user_id = OLD.user_id;END IF;RETURN NEW; END;$LC_TRIGGER_AFTER_UPDATE_TRANSACTION$ LANGUAGE plpgsql;CREATE TRIGGER LC_TRIGGER_AFTER_UPDATE_TRANSACTION AFTER UPDATE ON transactions FOR EACH ROW EXECUTE PROCEDURE LC_TRIGGER_AFTER_UPDATE_TRANSACTION();");
                });

            modelBuilder.Entity("Laraue.EfCoreTriggers.SqlTests.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("user_id")
                        .HasColumnType("uuid");

                    b.HasKey("UserId")
                        .HasName("pk_users");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Laraue.EfCoreTriggers.SqlTests.Entities.UserBalance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<decimal>("Balance")
                        .HasColumnName("balance")
                        .HasColumnType("numeric");

                    b.Property<Guid>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("uuid");

                    b.HasKey("Id")
                        .HasName("pk_balances");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasName("ix_balances_user_id");

                    b.ToTable("balances");
                });

            modelBuilder.Entity("Laraue.EfCoreTriggers.SqlTests.Entities.Transaction", b =>
                {
                    b.HasOne("Laraue.EfCoreTriggers.SqlTests.Entities.User", "User")
                        .WithMany("Transactions")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_transactions_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Laraue.EfCoreTriggers.SqlTests.Entities.UserBalance", b =>
                {
                    b.HasOne("Laraue.EfCoreTriggers.SqlTests.Entities.User", "User")
                        .WithOne("Balance")
                        .HasForeignKey("Laraue.EfCoreTriggers.SqlTests.Entities.UserBalance", "UserId")
                        .HasConstraintName("fk_balances_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
