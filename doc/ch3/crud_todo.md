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

6. 增todo model

7. todo.infrastructure 新增IRepository與Repository

8. todo.infrastructure 使用orm設定資料庫，新增EntityTypeConfiguration設定todo 資料庫欄位

9. todo.infrastructure 新增context 與seed

10. 新增xunit專案 todo.unittests，測試context與repository的CRUD

11. todo.mvc 設定AddDbContext 與DI Repository
    1. 安裝Microsoft.EntityFrameworkCore.InMemory
    2. 安裝Microsoft.EntityFrameworkCore.SqlServer
    3. 在statup.cs新增AddDbContext 與 設定IServiceCollection
    4. 在program.cs使用ServiceProvider ，將context載入seed資料初始化
    5. todoController加入ITodoRepository ，建構子新增參數
    6. 測試是否可執行