using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Jwell.FullTextSearch.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// 
    /// </summary>
    public class EnumTypeModelDescription : ModelDescription
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public EnumTypeModelDescription()
        {
            Values = new Collection<EnumValueDescription>();
        }

        /// <summary>
        /// 
        /// </summary>
        public Collection<EnumValueDescription> Values { get; private set; }
    }
}