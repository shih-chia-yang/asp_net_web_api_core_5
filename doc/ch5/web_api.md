# web_api

- controller,action,models
1. HttpGet - Retrieve - 200 Ok
2. HttpPost - Created - 201 Ok
3. HttpPut - Update No Comment- 204 Ok
4. HttpDelete- Delete No Comment -204 Ok
5. running API's on the swagger page


- 在類別層級定義route attribute，預設名稱為controller name，ex: HomeController route is `api/Home`，等同`[Route("api/Home")]`

```aspx-csharp
[Route("api/[controller]")]
```

- Controller與ControllerBase
1. ControllerBase：mvc controller基礎類別，不包含view相關的方法與屬性，api不需要view
2. Controller: 是ControllerBase加上view相關功能，用來專門處理web的類別

- 自訂custom model and custom Repository
1. add custom model
2. add custom Repository
3. startup.cs add di register IRepository
4. controller add ctor get DI parameter
5. add GetAll() action & Get(int id) action
6. add Add() Update() Delete() method and actions
7. use postman test web api


## http status code

[相關連結](https://developer.mozilla.org/zh-TW/docs/Web/HTTP/Status)