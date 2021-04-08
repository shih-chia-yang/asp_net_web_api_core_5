# startup

the startup class gets called when CreateHostBuilder runs from main() in ASP.NET core.

1. `ConfigureServices`: this method get s celled by the runtime. use this method to add services to the container.

2. `Configure`: this method gets called by the runtime. use this method to configure the HTTP request pipeline.


- IHostBuilder interface run console application as web apps(either as MVC or Web API)

- Host class has CreateDefaultBuilder, reference the server handling request (Kestrel -a web server like IIS)
