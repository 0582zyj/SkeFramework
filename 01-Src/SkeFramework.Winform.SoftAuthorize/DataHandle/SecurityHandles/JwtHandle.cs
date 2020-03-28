using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using SkeFramework.Winform.SoftAuthorize.DataHandle.Securitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.SoftAuthorize.DataHandle.SecurityHandles
{
    /// <summary>
    /// JWT加密处理
    /// </summary>
    public class JwtHandle : ISecurityHandle
    {
        /// <summary>
        /// 签名密钥
        /// </summary>
        const string secret = "secret";//

      


        public string Encrypt(string encryptStr)
        {
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
             long seconds= Convert.ToInt64(ts.TotalSeconds);
            //载荷（payload）
            var payload = new Dictionary<string, object>()
            {
                { "iss","ut"},//发行人
                { "sub", "jwt" },
                { "jti", "2fb5bf13-6efb-4ccc-a7d0-62481d8da439" },
                { "iat", seconds },
                { "exp",  seconds },
                { "data" ,encryptStr}
            };
            return CreateJWT(payload);
        }


        public string Decrypt(string decryptStr)
        {
            //校验JWT
            Console.WriteLine("*******************校验JWT，获得载荷***************");
            string ResultMessage;//需要解析的消息
            Dictionary<string, object> Payload = new Dictionary<string, object>();//获取负载
            if (ValidateJWT(decryptStr, out Payload, out ResultMessage))
            {
                if (Payload.ContainsKey("data"))
                {
                    return Payload["data"].ToString();
                }
                return "";
            }
            return ResultMessage;
        }

        public bool Validate(string token)
        {
            throw new NotImplementedException();
        }



          public  string CreateJWT(Dictionary<string, object> payload)
        {
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();//Base64编解码
            IJsonSerializer serializer = new JsonNetSerializer();//序列化和反序列
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();//HMACSHA256加密
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            return encoder.Encode(payload, secret);
        }

        public  bool ValidateJWT(string token, out Dictionary<string, object> payload, out string message)
        {
            bool isValidted = false;
            payload = new Dictionary<string, object>();
            try
            {
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();//Base64编解码
                IDateTimeProvider provider = new UtcDateTimeProvider();//UTC时间获取
                IJsonSerializer serializer = new JsonNetSerializer();//序列化和反序列
                IJwtValidator validator = new JwtValidator(serializer, provider);//用于验证JWT的类
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);//用于解析JWT的类
                //token为之前生成的字符串
                payload = decoder.DecodeToObject<Dictionary<string, object>>(token, secret, verify: true);
                isValidted = true;
                message = "验证成功";
            }
            catch (TokenExpiredException)//当前时间大于负载过期时间（负荷中的exp），会引发Token过期异常
            {
                message = "过期了！";
            }
            catch (SignatureVerificationException)//如果签名不匹配，引发签名验证异常
            {
                message = "签名错误！";
            }
            return isValidted;
        }

    }
}
