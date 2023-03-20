namespace ApiSchool.Utils
{
    public class MapProcedures
    {
        public static string MapGetUserByName(string name)
        {
            string sqlQuery = string.Format("CALL GetUserByName(\"{0}\");", name);
            return sqlQuery;
        }
    }
}
