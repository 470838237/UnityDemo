using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDM
{
    public enum UserGender
    {
        Unknow = 0,
        Male,
        Female,
        Other
    }

    public class UserInfo : TBufferBase
    {
        public string UserID = "";
        public string NickName = "";
        public string Region = "";
        public int AccountType;
        public UserGender Gender = UserGender.Unknow;
        public int Age;
        public int Level;

        public override void WriteTo(TBufferWriter writer)
        {
            int gender = (int)Gender;
            writer.Write(UserID);
            writer.Write(NickName);
            writer.Write(Region);
            writer.Write(AccountType);
            writer.Write(gender);
            writer.Write(Age);
            writer.Write(Level);
        }

        public override void ReadFrom(TBufferReader reader)
        {
            int gender = 0;
            reader.Read(ref UserID);
            reader.Read(ref NickName);
            reader.Read(ref Region);
            reader.Read(ref AccountType);
            reader.Read(ref gender);
            reader.Read(ref Age);
            reader.Read(ref Level);
            Gender = (UserGender)gender;
        }
    }

    public class CustomInfo : TBufferBase
    {
        public Dictionary<string, string> KVInfo;

        public CustomInfo(Dictionary<string, string> info)
        {
            KVInfo = new Dictionary<string, string>(info);
        }

        public override void WriteTo(TBufferWriter writer)
        {
            writer.Write(KVInfo);
        }

        public override void ReadFrom(TBufferReader reader)
        {
            reader.Read(ref KVInfo);
        }
    };
}
