using JWT;
using JWT.Serializers;
using Newtonsoft.Json;
using SkeFramework.Core.NetLog;
using SkeFramework.Core.Common.Enums;
using SkeFramework.Winform.LicenseAuth.DataEntities;
using SkeFramework.Winform.LicenseAuth.DataEntities.Constant;
using SkeFramework.Winform.LicenseAuth.DataHandle.Securitys;
using SkeFramework.Winform.LicenseAuth.DataHandle.StoreHandles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace SkeFramework.Winform.LicenseAuth.DataHandle.SecurityHandles
{
    /// <summary>
    /// JWT加密处理
    /// </summary>
    public class JwtHandle : ISecurityHandle
    {
        /// <summary>
        /// 密钥文件
        /// </summary>
        private ISaveHandles skHandle;


        public JwtHandle(ISaveHandles skSaveHandle)
        {
            skHandle = skSaveHandle;
        }


        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="encryptStr"></param>
        /// <returns></returns>
        public string Encrypt(string encryptStr)
        {
            return "";
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="decryptStr"></param>
        /// <returns></returns>
        public string Decrypt(string decryptStr)
        {
            Dictionary<string, object> Payload = new Dictionary<string, object>();//获取负载
            Debug.WriteLine("*******************校验JWT，获得载荷***************");
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();//Base64编解码
            IDateTimeProvider provider = new UtcDateTimeProvider();//UTC时间获取
            IJsonSerializer serializer = new JsonNetSerializer();//序列化和反序列
            IJwtValidator validator = new JwtValidator(serializer, provider);//用于验证JWT的类
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);//用于解析JWT的类
            skHandle.LoadByFile();
            string secretSk = skHandle.FinalCode;
            if (String.IsNullOrEmpty(secretSk))
                return String.Empty;
            //校验JWT
            Payload = decoder.DecodeToObject<Dictionary<string, object>>(decryptStr, secretSk, verify: true);
            return JsonConvert.SerializeObject(Payload);
        }

        public JsonResponse Validate(string token, string OriginalStr)
        {
            JsonResponse jsonResponse = JsonResponse.Failed.Clone() as JsonResponse;
            try
            {
                string json = Decrypt(token);
                if (!String.IsNullOrEmpty(json))
                {
                    Dictionary<string, object> Payload = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                    if (Payload.ContainsKey("jti") && OriginalStr == Payload["jti"].ToString())
                    {
                        jsonResponse.code = JsonResponse.SuccessCode;
                        jsonResponse.data = Payload;
                        jsonResponse.msg = "激活成功";
                    }
                    else
                    {
                        jsonResponse.code = (int)ErrorCodeEnums.MacCodeKeyChange;
                        jsonResponse.msg = ErrorCodeEnums.MacCodeKeyChange.GetEnumDescription();
                        LogAgent.Info(String.Format("tokenKey:{0};LocalKey:{1}", OriginalStr, Payload["jti"].ToString()));
                    }
                    return jsonResponse;
                }
                jsonResponse.msg = ErrorCodeEnums.SignatureKeyEmpty.GetEnumDescription(); 
                jsonResponse.code = (int)ErrorCodeEnums.SignatureKeyEmpty;
            }
            catch (TokenExpiredException ex)
            {
                jsonResponse.msg = ErrorCodeEnums.TokenExpired.GetEnumDescription();
                jsonResponse.code = (int)ErrorCodeEnums.TokenExpired;
                LogAgent.Info(String.Format("token:{0};ExpiredTime:{1}", token, ex.Expiration));
            }
            catch (SignatureVerificationException )
            {
                jsonResponse.msg = ErrorCodeEnums.SignatureError.GetEnumDescription();
                jsonResponse.code = (int)ErrorCodeEnums.SignatureError;
            }
            catch (Exception ex)
            {
                jsonResponse.msg = "该激活码不正确!";
                LogAgent.Error(ex.ToString());
            }
            LogAgent.Info("Validate:" + token + ";result:" + jsonResponse.msg);
            return jsonResponse;
        }
    }
}
