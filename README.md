# LINQ

### Query Syntax versus Method Syntax
One way to write a query in LINQ is to invoke extension methods. We can walk up to any collection, like an array or a list, and invoke. OrderBy,. When,. Count. We call this approach to querying the method syntax approach. Another way to write a query is to use the query syntax, which looks a lot like structure query language embedded in C#. 

### Deferred Execution of LINQ Query
Deferred execution means that the evaluation of an expression is delayed until its realized value is actually required. It greatly improves performance by avoiding unnecessary execution.

```csharp
      public static IEnumerable<T> Filter<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (var item in source)
            {
                if(predicate(item))
                {
                    yield return item;
                }
            }
        }
```

Many LINQ operators are implemented using the yield return syntax that we just used in our Filter method, and the operators that use this technique offer the behavior that we call deferred execution.

### Streaming Operators
A streaming operator needs to read through the source data, like the sequence of movies, up until the point where it produces a result. At that point, it will yield the result. 

When we're working with thousands of items where we only need to take 10 items because that's all that's going to fit on the screen. If we can produce those 10 items using a **Take** operator, and only using operators that are streaming that can be a very efficient query. It's only going to look through the source items until it finds the 10 items that it needs, but as soon as you introduce a non-streaming operators, something like OrderByDescending, now we're going to be looking at all the items in the collection.