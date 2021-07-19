# dotnet-graphql-demo
A demo project illustrating the basics of graphql with HotChocolate

References:
- https://chillicream.com/docs/hotchocolate/get-started

## Follow the steps to run:

- clone the repo
- run ```dotnet watch run```
- go to the mock up url generated. Mine was at ```https://localhost:5001/graphql```

### Example queries:
```graphql
query GetBookById{
  book(id: "asdasd2313") {
    title
    author {
      name
    }
  }
}

query GetBooks{
  allBooks{
    title
    author{
      name
    }
  }
}
```
### Example mutations:
```graphql
mutation AddAuthor {
  addAuthor(input: { name: "Hashem", age: 22, booksWritten: 10 }) {
    name
    age
  }
}

mutation AddBook {
  addBook(
    input: {
      author: { name: "Hashem", age: 40, booksWritten: 5 }
      title: "Sex and the City"
    }
  ) {
    author{
      age
      name
      booksWritten
    }
    title
  }
}
```
