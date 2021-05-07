using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace code.web.ViewComponents
{
    [ViewComponent(Name="IscedList")]
    public class IscedList:ViewComponent
    {
        public IscedList()
        {
            
        }

        public IViewComponentResult Invoke(string iscedId)
        {
            var viewmodel = new List<IscedViewModel>()
            {
                new IscedViewModel(){IscedId="0001",IscedName="教育學",
                DetailedList=new List<IscedViewModel>(){
                    new IscedViewModel(){IscedId="00011",IscedName="綜合教育"},
                    new IscedViewModel(){IscedId="00012",IscedName="成人教育"},
                    new IscedViewModel(){IscedId="00013",IscedName="殊特教育"},
                    new IscedViewModel(){IscedId="00014",IscedName="課程與教學"},
                    new IscedViewModel(){IscedId="00015",IscedName="教育科技"},
                    new IscedViewModel(){IscedId="00016",IscedName="教育測試評量"},
                    new IscedViewModel(){IscedId="00017",IscedName="技職教育"}
                }},
                new IscedViewModel(){IscedId="0002",IscedName="幼兒師資教育",
                DetailedList =new List<IscedViewModel>(){
                    new IscedViewModel(){IscedId="00021",IscedName="幼兒師資教育"}
                }},
                new IscedViewModel(){IscedId="0003",IscedName="普通科目師資教育",
                DetailedList = new List<IscedViewModel>(){
                    new IscedViewModel(){IscedId="00031",IscedName="普通科目師資教育"}
                }},
                new IscedViewModel(){IscedId="0004",IscedName="專業科目師資教育",
                DetailedList= new List<IscedViewModel>(){
                    new IscedViewModel(){IscedId="00041",IscedName="專業科目師資教育"}
                }},
                new IscedViewModel(){IscedId="0005",IscedName="其他教育",
                DetailedList=new List<IscedViewModel>(){
                    new IscedViewModel(){IscedId="00051",IscedName="其他教育"}
                }},
            };
            return View(viewmodel);
        }
    }
}