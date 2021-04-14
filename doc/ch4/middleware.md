# middleware

- chooses whether to pass the request to the next component in the pipeline.
- can perform work before and after the next component in the pipeline is invoked.

Request delegates are configure using `Run`、`Map` and `Use` extension methods.

1. the Run() method adds a middleware and ends the pipeline. doesn;t call next middleware
2. the Map() method adds middleware based on request path.
3. the Use() method adds a middleware , as a lambda or a dedicated class. the request delegate handler can be in-line as an anonymous method or a reusable class.