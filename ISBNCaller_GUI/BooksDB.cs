using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISBNCaller.DBWorker
{
    public class BooksDB : DbContext
    {
        #region Variables
        public DbSet<Authors> Authors { get; set; }
        public DbSet<Bookauthor> Bookauthor { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Bookseries> Bookseries { get; set; }
        public DbSet<Lent> Lent { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<Settings_language> Settings_language { get; set; }
        private string mConnection { get; set; }
        #endregion
        #region Constructors

        internal BooksDB(string connection)
        {
            mConnection = connection;
        }


        #endregion
        #region Methods
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString: mConnection);
            base.OnConfiguring(optionsBuilder);
        }
        #endregion
    }
}

[Table("authors")]
public class Authors : DbContext
{
    [Column("deleted")]
    public bool Deleted { get; internal set; }

    [Column("prename")]
    public string? Prename { get; internal set; }

    [Column("name")]
    public string Name { get; internal set; }

    [System.ComponentModel.DataAnnotations.Key]
    [Column("authorid")]
    public int Authorid { get; internal set; }

}

[Table("bookauthor"), PrimaryKey(nameof(Bookid), nameof(Authorid))]
public class Bookauthor : DbContext
{
    [Column("bookid")]
    public int Bookid { get; internal set; }

    [Column("authorid")]
    public int Authorid { get; internal set; }

}

[Table("books")]
public class Books : DbContext
{
    [System.ComponentModel.DataAnnotations.Key]
    [Column("bookid")]
    public int Bookid { get; internal set; }

    [Column("publishingdate")]
    public string? Publishingdate { get; internal set; }

    [Column("deleted")]
    public bool Deleted { get; internal set; }

    [Column("isbn13")]
    public string? Isbn13 { get; internal set; }

    [Column("isbn10")]
    public string? Isbn10 { get; internal set; }

    [Column("title")]
    public string Title { get; internal set; }

    [Column("format")]
    public string? Format { get; internal set; }

    [Column("subtitle")]
    public string? Subtitle { get; internal set; }

    [Column("ispartofseries")]
    public bool Ispartofseries { get; internal set; }

}

[Table("bookseries"), PrimaryKey(nameof(Bookid), nameof(Seriesid))]
public class Bookseries : DbContext
{
    [Column("noinseries")]
    public int? Noinseries { get; internal set; }

    [Column("bookid")]
    public int Bookid { get; internal set; }

    [Column("seriesid")]
    public int Seriesid { get; internal set; }

}

[Table("lent")]
public class Lent : DbContext
{
    [Column("lentdate")]
    public DateTime Lentdate { get; internal set; }

    [Column("surname")]
    public string? Surname { get; internal set; }

    [System.ComponentModel.DataAnnotations.Key]
    [Column("lentid")]
    public int Lentid { get; internal set; }

    [Column("prename")]
    public string Prename { get; internal set; }

    [Column("active")]
    public bool? Active { get; internal set; }

    [Column("bookid")]
    public int Bookid { get; internal set; }

}

[Table("series")]
public class Series : DbContext
{
    [System.ComponentModel.DataAnnotations.Key]
    [Column("seriesid")]
    public Int64 Seriesid { get; internal set; }

    [Column("name")]
    public string Name { get; internal set; }

}

[Table("settings")]
public class Settings : DbContext
{
    [System.ComponentModel.DataAnnotations.Key]
    [Column("id")]
    public int Id { get; internal set; }

    [Column("inuse")]
    public bool? Inuse { get; internal set; }

    [Column("setting")]
    public string Setting { get; internal set; }

    [Column("topic")]
    public string Topic { get; internal set; }

}

[Table("settings_language"), PrimaryKey(nameof(Languageid), nameof(Controlname))]
public class Settings_language : DbContext
{
    [Column("controlname")]
    public string Controlname { get; internal set; }

    [Column("text")]
    public string? Text { get; internal set; }

    [Column("languageid")]
    public int Languageid { get; internal set; }

}

