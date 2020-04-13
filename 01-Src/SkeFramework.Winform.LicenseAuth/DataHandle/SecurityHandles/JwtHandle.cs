using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using SkeFramework.Winform.LicenseAuth.DataEntities;
using SkeFramework.Winform.LicenseAuth.DataHandle.Securitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.LicenseAuth.DataHandle.SecurityHandles
{
    /// <summary>
    /// JWT加密处理
    /// </summary>
    public class JwtHandle : ISecurityHandle
    {
        /// <summary>
        /// 签名密钥
        /// </summary>
        const string secret = "smarthome";//

        //private Dictionary<string, object> payLoad;

        //public JwtEntities jwtEntities
        //{
        //    get
        //    {
        //        JwtEntities jwt = new JwtEntities();
        //        foreach (var item in payLoad)
        //        {

        //        }
        //        return jwt;
        //    }
        //    set
        //    {

        //    }
        //}

        public JwtHandle()
        {
           
        }

        public string Encrypt(string encryptStr)
        {
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            long seconds = Convert.ToInt64(ts.TotalSeconds);
            //载荷（payload）
            var payload = new Dictionary<string, object>()
            {
                { "iss","ut"},//发行人
                { "sub", "ideuser" },
                { "jti", encryptStr},
                { "iat", seconds },
                { "exp",  seconds },
            };
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();//Base64编解码
            IJsonSerializer serializer = new JsonNetSerializer();//序列化和反序列
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();//HMACSHA256加密
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            return encoder.Encode(payload, secret);
        }


        public string Decrypt(string decryptStr)
        {
            //校验JWT
            Console.WriteLine("*******************校验JWT，获得载荷***************");
            Dictionary<string, object> Payload = new Dictionary<string, object>();//获取负载
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();//Base64编解码
            IDateTimeProvider provider = new UtcDateTimeProvider();//UTC时间获取
            IJsonSerializer serializer = new JsonNetSerializer();//序列化和反序列
            IJwtValidator validator = new JwtValidator(serializer, provider);//用于验证JWT的类
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);//用于解析JWT的类
            //token为之前生成的字符串
            Payload = decoder.DecodeToObject<Dictionary<string, object>>(decryptStr, secret, verify: true);
            if (Payload.ContainsKey("jti"))
            {
                return Payload["jti"].ToString();
            }
            return "";
        }

        public JsonResponse Validate(string token,string OriginalStr)
        {
            JsonResponse jsonResponse = JsonResponse.Failed;
            bool isValidted = false;
            try
            {
               isValidted =OriginalStr ==Decrypt(token);
                jsonResponse.code = JsonResponse.SuccessCode;
                jsonResponse.msg= "成功";
            }
            catch (TokenExpiredException)//当前时间大于负载过期时间（负荷中的exp），会引发Token过期异常
            {
                jsonResponse.msg = "过期了！";
            }
            catch (SignatureVerificationException)//如果签名不匹配，引发签名验证异常
            {
                jsonResponse.msg = "签名错误！";
            }
            catch(Exception)
            {
                jsonResponse.msg = "非法密钥";
            }
            return jsonResponse;
        }

      

    }
}
