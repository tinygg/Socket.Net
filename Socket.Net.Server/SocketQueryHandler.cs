using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Socket.Net.Server
{
    public class SocketQueryHandler
    {
        /// <summary>
        /// socket查询
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="query_type"></param>
        /// <param name="query_wd"></param>
        /// <returns></returns>
        public string Query(string msg, out string query_type, out string query_wd)
        {
            string query_rlt = string.Empty;
            query_type = string.Empty;
            query_wd = string.Empty;
            string[] query_arry = msg.Split(new string[] { "3@1@4@1@5@9" }, StringSplitOptions.None);
            if (query_arry.Length == 3)
            {
                query_type = query_arry[1];
                query_wd = query_arry[2];
            }
            else if (query_arry.Length == 2)
            {
                query_type = query_arry[0];
                query_wd = query_arry[1];
            }
            query_rlt = "this's result...";
            return query_rlt;
        }
    }
}
