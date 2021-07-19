using HotChocolate;

namespace graphql_dotnet.Schemas
{
    [GraphQLName("BookAuthor")]
    public class Author
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int BooksWritten { get; set; }

        [GraphQLIgnore]
        public int Sex { get; set; }

        // note that address is lowercased. This is a convention.
        [GraphQLName("address")]
        public string Location { get; set; }

    }
}