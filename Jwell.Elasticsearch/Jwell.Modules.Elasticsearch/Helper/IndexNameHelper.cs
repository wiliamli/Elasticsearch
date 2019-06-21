using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Elasticsearch.Helper
{
    public class IndexNameHelper
    {
        public static string GetIndexMappingName(string serviceSign,string indexName)
        {
            return $"{serviceSign}-{indexName}".ToLower();
        }

        public static string GetIndexSizeName(string serviceSign, string indexName)
        {
            return $"{serviceSign}-{indexName}-size".ToLower();
        }

        //public static string GetIndexMappingName(string serviceNumber, string indexName, int expensionNum)
        //{
        //    return $"{serviceNumber}-{indexName}-{expensionNum}";
        //}

        //public static string GetIndexTemplateName(string serviceNumber, string indexName)
        //{
        //    return $"{serviceNumber}-{indexName}-template";
        //}

        //public static string GetIndexNamePattern(string serviceNumber, string indexName)
        //{
        //    return $"{serviceNumber}-{indexName}-*";
        //}

        
    }
}
