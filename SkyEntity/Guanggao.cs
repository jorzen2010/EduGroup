using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SkyEntity
{
    public class Guanggao
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "类别")]
        [Required(ErrorMessage = "类别不能可为空")]
        public int Category { get; set; }

        [Display(Name = "文字标题")]
        [Required(ErrorMessage = "请输入文字标题")]
        public string Title { get; set; }

        [Display(Name = "图片标题")]
        public string Tupian { get; set; }

        [Display(Name = "广告介绍")]
        public string AdInfo { get; set; }

        [Display(Name = "链接地址")]
        public string HrefUrl { get; set; }

        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }
       
        [Display(Name = "排序")]
        public int Paixu { get; set; }

        [Display(Name = "是否上线")]
        public bool Shangxian { get; set; }

    }
}
