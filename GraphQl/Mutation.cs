using System.Threading.Tasks;
using graphql_dotnet.Schemas;

namespace graphql_dotnet.GraphQl
{
    public class Mutation
    {
        public async Task<Book> AddBook(Book input)
        {
            await Task.Delay(1); // just for demo purposes to show the usage of async/await
            System.Console.WriteLine("book created!");
            System.Console.WriteLine(input.ToString());

            return input;

        }

        public async Task<Author> AddAuthor(Author input)
        {
            await Task.Delay(1); // just for demo purposes to show the usage of async/await
            System.Console.WriteLine("author created!");

            System.Console.WriteLine(input);
            Author author = input;
            return author;

        }

    }
}