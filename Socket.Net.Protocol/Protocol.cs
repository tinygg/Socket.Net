using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Socket.Net.Protocol
{
    /// <summary>
    /// 消息协议
    /// </summary>
    public class Protocol
    {
        public const Int32 CMD_BLOCK_SIZE = 10;

        public static byte[] HELLO = new byte[] { 72, 69, 76, 76, 79, 0, 0, 0, 0, 0 };
        public static byte[] EXIT = new byte[] { 69, 88, 73, 84, 0, 0, 0, 0, 0, 0 };
        public static byte[] BEAT = new byte[] { 66, 69, 65, 84, 0, 0, 0, 0, 0, 0 };
        public static byte[] MSG = new byte[] { 77, 83, 71, 0, 0, 0, 0, 0, 0, 0 };
        public static byte[] QUERY = new byte[] { 81, 85, 69, 82, 89, 0, 0, 0, 0, 0 };
        public static byte[] REAUTH = new byte[] { 82, 69, 65, 85, 84, 72, 0, 0, 0, 0 };
        public static byte[] FORWARD = new byte[] { 70, 79, 82, 87, 65, 82, 68, 0, 0, 0 };
        public static byte[] CODE = new byte[] { 67, 79, 68, 69, 0, 0, 0, 0, 0, 0 };
        public static byte[] NULL = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        public const Int32 FROM_USER_BLOCK_SIZE = 150;
        public const Int32 TO_USER_BLOCK_SIZE = 150;

        public const Int32 MSG_SIZE_BLOCK_SIZE = 4;

        public const Int32 MSG_BLOCK_BEGIN = 314;

        /// <summary>
        /// bytes
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string BytesToStr(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// string 转 bytes
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] StrToBytes(string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        /// <summary>
        /// 按照协议解析消息
        /// </summary>
        /// <param name="bytes_in"></param>
        /// <param name="msg_type"></param>
        /// <param name="auth_code"></param>
        /// <param name="to_user"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool ParseProtocolMsg(BinaryReader reader, out CMD_TYPE msg_type, out string auth_code, out string to_user, out string msg)
        {
            bool rlt = false;
            //初始化输出
            msg_type = CMD_TYPE.NULL;
            auth_code = string.Empty;
            to_user = string.Empty;
            msg = string.Empty;

            try
            {
                byte[] msg_bytes = reader.ReadBytes(CMD_BLOCK_SIZE);
                var msg_str = BytesToStr(msg_bytes);
                if (msg_str == BytesToStr(Protocol.HELLO))
                {
                    msg_type = CMD_TYPE.HELLO;
                }
                else if (msg_str == BytesToStr(Protocol.EXIT))
                {
                    msg_type = CMD_TYPE.EXIT;
                }
                else if (msg_str == BytesToStr(Protocol.BEAT))
                {
                    msg_type = CMD_TYPE.BEAT;
                }
                else if (msg_str == BytesToStr(Protocol.MSG))
                {
                    msg_type = CMD_TYPE.MSG;
                }
                else if (msg_str == BytesToStr(Protocol.QUERY))
                {
                    msg_type = CMD_TYPE.QUERY;
                }
                else if (msg_str == BytesToStr(Protocol.CODE))
                {
                    msg_type = CMD_TYPE.CODE;
                }
                else if (msg_str == BytesToStr(Protocol.REAUTH))
                {
                    msg_type = CMD_TYPE.REAUTH;
                }
                else if (msg_str == BytesToStr(Protocol.FORWARD))
                {
                    msg_type = CMD_TYPE.FORWARD;
                }
                else if (msg_str == BytesToStr(Protocol.NULL))
                {
                    msg_type = CMD_TYPE.NULL;
                }

                auth_code = Encoding.UTF8.GetString(reader.ReadBytes(Protocol.FROM_USER_BLOCK_SIZE));

                to_user = Encoding.UTF8.GetString(reader.ReadBytes(Protocol.TO_USER_BLOCK_SIZE));

                //消息大小
                Int32 msg_size = reader.ReadInt32();
                byte[] msg_temp = reader.ReadBytes(msg_size);
                msg = Encoding.UTF8.GetString(msg_temp);
                rlt = true;
            }
            catch (IOException e)
            {
                rlt = true;
                msg_type = CMD_TYPE.EXIT;
                msg = e.Message;
            }
            catch (Exception e)
            {
                rlt = false;
            }
            return rlt;
        }

        /// <summary>
        /// 按照协议封装消息
        /// </summary>
        /// <param name="msg_type">指令类型CMD_TYPE</param>
        /// <param name="from_user">认证字符</param>
        /// <param name="to_user">发送目的地</param>
        /// <param name="msg">消息内容</param>
        /// <returns></returns>
        public static byte[] MakeProtocolMsg(CMD_TYPE msg_type, string from_user, string to_user, string msg)
        {
            byte[] part1 = new byte[Protocol.CMD_BLOCK_SIZE];
            byte[] part2 = new byte[Protocol.FROM_USER_BLOCK_SIZE];
            byte[] part3 = new byte[Protocol.TO_USER_BLOCK_SIZE];
            byte[] part4 = new byte[4];//msg size
            byte[] part5 = null;
            switch (msg_type)
            {
                case CMD_TYPE.HELLO:
                    part1 = Protocol.HELLO;
                    break;
                case CMD_TYPE.EXIT:
                    part1 = Protocol.EXIT;
                    break;
                case CMD_TYPE.BEAT:
                    part1 = Protocol.BEAT;
                    break;
                case CMD_TYPE.MSG:
                    part1 = Protocol.MSG;
                    break;
                case CMD_TYPE.QUERY:
                    part1 = Protocol.QUERY;
                    break;
                case CMD_TYPE.REAUTH:
                    part1 = Protocol.REAUTH;
                    break;
                case CMD_TYPE.FORWARD:
                    part1 = Protocol.FORWARD;
                    break;
                case CMD_TYPE.CODE:
                    part1 = Protocol.CODE;
                    break;
                default:
                    part1 = Protocol.NULL;
                    break;
            }

            byte[] auth_tmp = System.Text.Encoding.UTF8.GetBytes(from_user);
            if (auth_tmp.Length <= Protocol.FROM_USER_BLOCK_SIZE)
            {
                Array.Copy(auth_tmp, part2, auth_tmp.Length);
            }

            byte[] to_tmp = System.Text.Encoding.UTF8.GetBytes(to_user);
            if (to_tmp.Length <= Protocol.TO_USER_BLOCK_SIZE)
            {
                Array.Copy(to_tmp, part3, to_tmp.Length);
            }

            part5 = Encoding.UTF8.GetBytes(msg);
            part4 = BitConverter.GetBytes(part5.Length);

            List<byte> rlt = new List<byte>();
            rlt.AddRange(part1);
            rlt.AddRange(part2);
            rlt.AddRange(part3);
            rlt.AddRange(part4);
            rlt.AddRange(part5);

            return rlt.ToArray();
        }
    }

    /// <summary>
    /// 消息类型
    /// </summary>
    public enum CMD_TYPE
    {
        HELLO,
        EXIT,
        BEAT,
        MSG,
        QUERY,
        REAUTH,
        FORWARD,
        CODE,
        NULL
    }
}
