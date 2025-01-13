using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.Helper
{
    /// <summary>
    /// 随机数帮助类
    /// </summary>
    public class RandomHelper
    {
        //验证验证码
        public static Dictionary<string, DateTime> DicCode = new Dictionary<string, DateTime>();
        public static string GetRequestCode()
        {
            string CodeSerial = "2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,j,k,m,n,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,J,K,M,N,P,Q,R,S,T,U,V,W,X,Y,Z";
            string[] arr = CodeSerial.Split(',');
            string code = "";
            int randValue = -1;
            Random random = new Random(unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < 4; i++)
            {
                randValue = random.Next(0, arr.Length - 1);
                code += arr[randValue];
            }
            DicCode.Add(code, DateTime.Now);
            return code;
        }
    }
}
