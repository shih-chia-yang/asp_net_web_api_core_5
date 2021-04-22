# Authentication

## [Authorize] attribute

1. 在`Controller` or `Action` 加上`[Authorize]`可限制任何被驗證使用者存取

2. `[AllowAnonymous]` attribute to allow access by non-authenticated users to individual actions.

## Claims-Based Authorization

ASP.NET Core can be achieved by first assigning claims to the user,and then base on those claims defining policies to determine user permissions

**Claims**
a claims is a name value pair that represents what the subject is,not what the subject can do.

在驗證流程中將`Claims`分配給`ClaimsIdentity`，再由`ClaimsIdentity`分配給`ClaimsPrincipal`

Claim can contain information about the user like their name,email,address ,birth date etc

**Policy**

Policy is what the user is allowed to do,it is the permission rule.

Developers add policies when Configuring `Authorization service` in `Startup.cs`

**Adding claims checks**

Claim based authorization checks are declarative

針對controller,或是controller中的action去識別使用者那些claims為必要，那些為選擇性的

claims必須保留才能對資源進行訪問或存取

- 簡易的claim policy只尋找claim是否存在，而不檢查其值。

1. building claim and registering policies

```aspx-csharp
public void ConfigureServices(IServiceCollection services)
{
    ...
    ...
    services.AddAuthentication(options=>{
        options.AddPolicy("Manager",policy=>policy.RequireClaim("CanManaged"));
    });
}
```
Manager Policy會尋找current identity的claims中是否有`CanManaged`

`[Authorize]`中的Policy property可指定 特定policy name，而`[Authorize]`可以加在controller或是action

```aspx-csharp
[Authorize(Policy="Manager")]
public class ManagerController:Controller
{

}
```

以上的例子中，controller內都會受到`Authorize`所保護，如果有個別動作不要特別驗證，可以加上`[AllowAnonymous]`

```aspx-csharp
[Authorize(Policy="Manager")]
public class ManagerController:Controller
{

    [AllowAnonymous]
    public IActionResult List()
    {
    }
}
```

## implementing claims policy

1. add User Model
2. add IUserRepository & UserRepository
3. initialize UserRepository
4. register UserRepository to DI 
5. modify IsAuthentic ，改由UserRepository取得使用者資料
6. modify Login 當CanManaged=true時，增加claim `CanManaged`
7. HomeController add `[Authorize(Policy="Manager")]`
8. Privacy action add `[AllowAnonymous]` 