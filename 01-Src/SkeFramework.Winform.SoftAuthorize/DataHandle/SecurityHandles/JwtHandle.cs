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
    public class JwtHandle: ISecurityHandle
    {
        static IJwtAlgorithm algorithm = new HMACSHA256Algorithm();//HMACSHA256加密
        static IJsonSerializer serializer = new JsonNetSerializer();//序列化和反序列
        static IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();//Base64编解码
        static IDateTimeProvider provider = new UtcDateTimeProvider();//UTC时间获取
        const string secret = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQC4aKpVo2OHXPwb1R7duLgg";//服务端

     

        public static bool ValidateJWT(string token, out string payload, out string message)
        {
            bool isValidted = false;
            payload = "";
            try
            {
                IJwtValidator validator = new JwtValidator(serializer, provider);//用于验证JWT的类
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);//用于解析JWT的类
                payload = decoder.Decode(token, secret, verify: true);
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


        public string Encrypt(string encryptStr)
        {
            //载荷（payload）
            var payload = new Dictionary<string, object>
            {
                { "iss","ut"},//发行人
                { "exp", DateTimeOffset.UtcNow.AddSeconds(10).ToUniversalTime() },//到期时间
                { "sub", "jwt" }, //主题
                { "aud", "USER" }, //用户
                { "iat", DateTime.Now.ToString() }, //发布时间 
            };
            //生成JWT
            Console.WriteLine("******************生成JWT*******************");
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            return encoder.Encode(payload, secret);
        }

        public string Decrypt(string decryptStr)
        {
            throw new NotImplementedException();
        }


        public string Encode(IDictionary<string, object> extraHeaders, object payload, byte[] key)
        {
            if (payload is null)
                throw new ArgumentNullException(nameof(payload));

            var segments = new List<string>(3);

            var header = extraHeaders is null ? new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase) : new Dictionary<string, object>(extraHeaders, StringComparer.OrdinalIgnoreCase);
            header.Add("typ", "JWT");
            header.Add("alg", algorithm.Name);

            var headerBytes =Encoding.UTF8.GetBytes(serializer.Serialize(header));
            var payloadBytes = Encoding.UTF8.GetBytes(serializer.Serialize(payload));

            segments.Add(urlEncoder.Encode(headerBytes));
            segments.Add(urlEncoder.Encode(payloadBytes));

            var stringToSign = String.Join(".", segments.ToArray());
            var bytesToSign = Encoding.UTF8.GetBytes(stringToSign);

            var signature = algorithm.Sign(key, bytesToSign);
            segments.Add(urlEncoder.Encode(signature));

            return String.Join(".", segments.ToArray());
        }

       
    }
}
