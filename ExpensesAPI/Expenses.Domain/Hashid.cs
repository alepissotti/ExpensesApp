using HashidsNet;

namespace Expenses.Domain
{
    public static class Hashid
    {
        private const string Salt = "EgZjaHJvbWUqCggBEAAYDxgWGB4y";
        
        public static Hashids GetHashid()
        {
            return new Hashids(Salt, 8);
        }

        public static string Encode(int id)
        {
            return GetHashid().Encode(id);
        }

        public static int Decode(string encodeId)
        {
            return GetHashid().Decode(encodeId).FirstOrDefault();
        }
    }
}
