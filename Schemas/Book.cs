using HotChocolate;

namespace graphql_dotnet.Schemas
{
    public class Book
    {
        // means title can never return a null value
        [GraphQLNonNullType]
        public string Title { get; set; }
        public Author Author { get; set; }

    }
}