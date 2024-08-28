using EasyWechatModels.Dto;
using EasyWechatModels.Other;
using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CommonManager.Helper
{
    public class CustomerJWTHelper
    {
        private readonly IJsonSerializer _jsonSerializer;

        private readonly IDateTimeProvider _dateTimeProvider;

        private readonly IJwtValidator _jwtValidator;

        private readonly IBase64UrlEncoder _base64UrlEncoder;

        private readonly IJwtAlgorithm _jwtAlgorithm;

        private readonly IJwtDecoder _jwtDecoder;

        private readonly IJwtEncoder _jwtEncoder;

        public CustomerJWTHelper()
        {
            _jsonSerializer = new JsonNetSerializer();
            _dateTimeProvider = new UtcDateTimeProvider();
            _jwtValidator = new JwtValidator(_jsonSerializer, _dateTimeProvider);
            _base64UrlEncoder = new JwtBase64UrlEncoder();
            _jwtAlgorithm = new HMACSHA256Algorithm();
            _jwtDecoder = new JwtDecoder(_jsonSerializer, _jwtValidator, _base64UrlEncoder, _jwtAlgorithm);
            _jwtEncoder = new JwtEncoder(_jwtAlgorithm, _jsonSerializer, _base64UrlEncoder);
        }
        private static JWTTokenOptions _JWTTokenOptions = AppSettingHelper.ReadAppSettings<JWTTokenOptions>("JWTTokenOptions");
        //public CustomerJWTHelper()
        //{
        //    var option1 = AppSettingHelper.ReadAppSettings<JWTTokenOptions>("JWTTokenOptions");
        //    _JWTTokenOptions = option1;
        //}
        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static string GetToken(BaseUsersRes user)
        {
            var claims = new[]
            {
                    new Claim("id",user.Id.ToString()),
                    new Claim("nickName",user.NickName),
                    new Claim("name",user.Name),
                    new Claim("userType",user.UserType.ToString()),
            };
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JWTTokenOptions.SecurityKey));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken
            (
                issuer: _JWTTokenOptions.Issuer,
                audience: _JWTTokenOptions.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(1),//十分钟有效期
                signingCredentials: credentials
                );
            string result = new JwtSecurityTokenHandler().WriteToken(token);
            return result;
        }
        public string Encode(IDictionary<string, object> payload, int expiredMinute = 30)
        {
            if (!payload.ContainsKey("exp"))
            {
                double secondsSince = UnixEpoch.GetSecondsSince(DateTime.Now.AddMinutes(expiredMinute));
                payload.Add("exp", secondsSince);
            }

            return _jwtEncoder.Encode(payload, _JWTTokenOptions.SecurityKey);
        }
        public string Encode(IDictionary<string, object> payload, string key, int expiredMinute = 30)
        {
            if (!payload.ContainsKey("exp"))
            {
                double secondsSince = UnixEpoch.GetSecondsSince(DateTime.Now.AddMinutes(expiredMinute));
                payload.Add("exp", secondsSince);
            }

            return _jwtEncoder.Encode(payload, key);
        }
        /// <summary>
        /// 校验解析token
        /// </summary>
        /// <returns></returns>
        public static string ValidateJwtToken(string token)
        {
            return ValidateJwtToken(token, _JWTTokenOptions.SecurityKey);
        }
        /// <summary>
        /// 校验解析token
        /// </summary>
        /// <returns></returns>
        public static string ValidateJwtToken(string token, string secret)
        {
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtAlgorithm alg = new HMACSHA256Algorithm();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, alg);
                var json = decoder.Decode(token, secret, true);
                //校验通过，返回解密后的字符串
                return json;
            }
            catch (TokenExpiredException)
            {
                //表示过期
                return "expired";
            }
            catch (SignatureVerificationException)
            {
                //表示验证不通过
                return "invalid";
            }
            catch (Exception)
            {
                return "error";
            }
        }

    }
}
