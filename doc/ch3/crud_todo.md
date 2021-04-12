#todo

1. 建立新方案
```dotnetcli
dotnet new sln -o todo
```

- 此為書內範例，個人不打算使用引用範例database.dll與repository.dll
2. 新增console app project, named DatabaseTest
```dotnetcli
dotnet new console -o DatabaseTest
```

3. 自行建立datacontext與repository專案，提供todo.mvc使用,新增todo.infrastructure專案，加入方案，且todo.mvc將此專案加入參考
```dotnetcli
dotnet new classlib -o todo.infrastructure

dotnet sln add ./todo.infrastructure/todo.infrastructure.csproj

cd todo.mvc

dotnet add reference ../todo.infrastructure/todo.infrastructure.csproj
```

4. using nuget install Microsoft.EntityFrameworkCore、Microsoft.EntityFrameworkCore.Relational

5. add todo.domain project
```dotnetcli
dotnet new classlib -o todo.domain

dotnet sln add ./todo.domain/todo.domain.csproj

cd todo.mvc

dotnet add reference ../todo.domain/todo.domain.csproj
```