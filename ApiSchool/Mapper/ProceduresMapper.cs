using ApiSchool.Recourses;
using System.Resources;
using static ApiSchool.Models.Enum.SystemEnum;

namespace ApiSchool.Mapper
{
    public class ProceduresMapper
    {
        public static string Map(ResourceManager resource,string procedure, List<object?> parameters)
        {
            string? queryOrigin = resource.GetString(procedure);
            string? queryModified = string.Empty;

            for (int i = 0; i < parameters.Count(); i++)
            {
                if (i == 0)
                {
                    switch (Type.GetTypeCode(parameters[i].GetType()))
                    {
                        case TypeCode.String:
                            queryModified = queryOrigin.Insert(queryOrigin.Count() - 3, "\"" + parameters[i].ToString() + "\"");
                            break;
                        case TypeCode.Int32:
                        case TypeCode.Int64:
                        case TypeCode.UInt16:
                        case TypeCode.SByte:
                        case TypeCode.Byte:
                            queryModified = queryOrigin.Insert(queryOrigin.Count() - 3, parameters[i].ToString());
                            break;
                        case TypeCode.DateTime:
                            queryModified = queryOrigin.Insert(queryOrigin.Count() - 3, "\"" + parameters[i].ToString() + "\"");
                            break;
                    }
                }
                else
                {
                    switch (Type.GetTypeCode(parameters[i].GetType()))
                    {
                        case TypeCode.String:
                            queryModified = queryModified.Insert(queryModified.Count() - 3, ",\"" + parameters[i].ToString() + "\"");
                            break;
                        case TypeCode.Int32:
                        case TypeCode.Int64:
                        case TypeCode.UInt16:
                        case TypeCode.SByte:
                        case TypeCode.Byte:
                            queryModified = queryModified.Insert(queryModified.Count() - 3, "," + parameters[i].ToString());
                            break;
                        case TypeCode.DateTime:
                            queryModified = queryModified.Insert(queryModified.Count() - 3, ",\"" + DateTime.Parse(parameters[i].ToString()).ToString("yyyy-MM-dd") + "\"");
                            break;
                    }
                }
            }

            return queryModified;
        }
    }
}
