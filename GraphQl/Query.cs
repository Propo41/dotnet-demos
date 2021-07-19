using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using graphql_dotnet.Schemas;
using System.Collections;
using HotChocolate;
namespace graphql_dotnet.GraphQl
{
    public class Query
    {

        public Book GetBook([GraphQLNonNullType] string id) =>
           new Book
           {
               Title = "C# in depth.",
               Author = new Author
               {
                   Name = "Jon Skeet"
               }
           };


        public List<Book> GetAllBooks()
        {
            // create a list of book objects
            var books = new List<Book>();
            books.Add(new Book { Title = "C# in depth", Author = new Author { Name = "Jon Skeet" } });
            books.Add(new Book { Title = "Intro to data structures", Author = new Author { Name = "Scarlet Johnson" } });
            books.Add(new Book { Title = "Python Masterclass", Author = new Author { Name = "Scarlet Johnson" } });

            return books;
        }

    }
}