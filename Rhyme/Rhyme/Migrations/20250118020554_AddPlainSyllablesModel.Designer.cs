﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Rhyme.Data;

#nullable disable

namespace Rhyme.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250118020554_AddPlainSyllablesModel")]
    partial class AddPlainSyllablesModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.1");

            modelBuilder.Entity("Rhyme.Models.WordWithPhonemes", b =>
                {
                    b.Property<int>("WordWithPhonemesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.PrimitiveCollection<string>("Phonemes")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Word")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("WordWithPhonemesId");

                    b.ToTable("WordsWithPhonemes");
                });

            modelBuilder.Entity("Rhyme.Models.WordWithPlainSyllables", b =>
                {
                    b.Property<int>("WordWithPlainSyllablesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.PrimitiveCollection<string>("Syllables")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Word")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("WordWithPlainSyllablesId");

                    b.ToTable("WordsWithPlainSyllables");
                });
#pragma warning restore 612, 618
        }
    }
}
