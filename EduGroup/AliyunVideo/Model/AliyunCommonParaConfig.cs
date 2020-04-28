using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AliyunVideo
{
    public class AliyunCommonParaConfig
    {

        public const string ApiUrl = "vod.cn-shanghai.aliyuncs.com";
        public const string Format = "JSON";
        public const string Version = "2017-03-21";
        public const string AccessKeyId = "";
        public const string AccessKeySecret = "";

        public const string SignatureVersion = "1.0";

        public const string SignatureMethod = "HMAC-SHA1";

        // 注意在阿里云的使用过程中为了防止外链，使用了Refer防盗链的白名单。导致视频无法播放，需要上线后在测试。

    }
}


