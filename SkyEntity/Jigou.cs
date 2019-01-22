using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SkyEntity
{
    public class Jigou
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "机构名称")]
        [Required(ErrorMessage = "请输入机构名称")]
        public string JigouName { get; set; }        
        [Required(ErrorMessage = "请输入介绍")]
        [Display(Name = "介绍")]
        public string JigouInfo { get; set; }
        [Display(Name = "内容")]
        public string Content { get; set; }
        [Display(Name = "标签")]
        public string Tags { get; set; }
        [Display(Name = "封面")]
        public string Cover { get; set; }
        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }
        [Display(Name = "地址")]
        public string JigouAddress { get; set; }
        [Display(Name = "联系人")]
        public string JigouLianxiren { get; set; }
        [Display(Name = "联系电话")]
        public string JigouLianxidianhua { get; set; }
        [Display(Name = "密码")]
        public string Password { get; set; }
        [Display(Name = "图片集")]
        public string Tupianji { get; set; }
        [Display(Name = "是否参与投票")]
        public bool Toupiao { get; set; }
        [Display(Name = "是否上线")]
        public bool Shangxian { get; set; }

    }

    public class JgVideo
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "机构编号")]
        [Required(ErrorMessage = "请输入机构编号")]
        public int JgBianhao { get; set; }
        [Required(ErrorMessage = "请输入介绍")]
        [Display(Name = "视频标题")]
        public string JgTitle { get; set; }
        [Display(Name = "视频介绍")]
        public string JgInfo { get; set; }
        [Display(Name = "阿里云地址")]
        public string Content { get; set; }
        [Display(Name = "标签")]
        public string Tags { get; set; }
        [Display(Name = "封面")]
        public string Cover { get; set; }
        [Display(Name = "上线时间")]
        public DateTime CreateTime { get; set; }
        [Display(Name = "是否上线")]
        public bool Shangxian { get; set; }

    }
}
