# 关于UEditorNetCore
ASP.NET Core下百度编辑器UEditor服务端。

# 安装
```
Install-Package UEditorNetCore
```

# 使用
## 1.在Startup.cs的ConfigureServices方法中添加UEditorNetCore服务
``` C#
public void ConfigureServices(IServiceCollection services)
{
    //第一个参数为配置文件路径，默认为项目目录下config.json
    //第二个参数为是否缓存配置文件，默认false
    services.AddUEditorService()
    services.AddMvc();
}
```

## 2.添加控制器用于服务端操作
``` C#
[Route("api/[controller]")] //配置路由
public class UEditorController : Controller
{
    private UEditorService ue;
    public UEditorController(UEditorService ue)
    {
        this.ue = ue;
    }

    public void Do()
    {
        ue.DoAction(HttpContext);
    }
}
```

## 3.修改前端配置文件ueditor.config.js
修改serverUrl为第2步Controller中配置的路由，使用例子中的路由按照以下配置：
``` json
serverUrl:"/api/UEditor"
```

## 4.修改服务端配置config.json
上传类的操作需要配置相应的PathFormat和Prefix，在示例中部署在web根目录，因此Prefix都设置为"/"。使用时要根据具体情况配置。
例如示例中图片上传的配置如下：
``` json
"imageUrlPrefix": "/", /* 图片访问路径前缀 */
"imagePathFormat": "upload/image/{yyyy}{mm}{dd}/{time}{rand:6}", 
```

## 5.添加javascript引用
``` html
<script type="text/javascript" charset="utf-8" src="~/lib/ueditor/ueditor.config.js"></script>
<script type="text/javascript" charset="utf-8" src="~/lib/ueditor/ueditor.all.min.js"> </script>
<script type="text/javascript" charset="utf-8" src="~/lib/ueditor/lang/zh-cn/zh-cn.js"></script>
```

# 扩展
如果需要扩展action，可以在Startup.cs的ConfigureServices方法中进行。
## 添加新的action
``` C#
public void ConfigureServices(IServiceCollection services)
{
    services.AddUEditorService()
        .Add("test", context =>
        {
            context.Response.WriteAsync("from test action");
        })
        .Add("test2", context =>
        {
            context.Response.WriteAsync("from test2 action");
        });
    services.AddMvc();
}
```
以上代码扩展了名字为test和test2两个action，作为示例仅仅返回了字符串。在扩展时可以读取Config配置，并使用已有的Handler。

## 覆盖现有action
上面的Add方法除了添加新action外还可以覆盖现有action。例如现有的action可能不符合你的要求，可以Add一个同名的action覆盖现有的。

## 移除action
如果要移除某个action，可以使用Remove方法。
``` C#
public void ConfigureServices(IServiceCollection services)
{
    services.AddUEditorService()
        .Remove("test");
    services.AddMvc();
}
```
以上代码移除了名为test的action。
